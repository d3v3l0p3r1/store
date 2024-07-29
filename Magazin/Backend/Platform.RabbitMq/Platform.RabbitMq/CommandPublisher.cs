using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace Platform.RabbitMq
{
    public class CommandPublisher<TCommand, TResult> : ICommandPublisher<TCommand, TResult>
        where TCommand : ICommand
    {
        private readonly RabbitMqOptions _options;
        private string _replyQueue;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<TResult>> callbackMapper = new();
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public CommandPublisher(RabbitMqOptions options)
        {
            _options = options;

            var factory = new ConnectionFactory
            {
                HostName = _options.Host
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            var command = Activator.CreateInstance(typeof(TCommand)) as ICommand;

            _replyQueue = _channel.QueueDeclare(command.Queue, exclusive: false).QueueName;

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                if (!callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                    return;
                var body = ea.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);

                var result = JsonSerializer.Deserialize<TResult>(response);
                if (result == null)
                {
                    throw new Exception("Invalid reply queue response");
                }

                tcs.TrySetResult(result);
            };

            _channel.BasicConsume(consumer: consumer,
                                 queue: _replyQueue,
                                 autoAck: true);
        }

        public Task<TResult> Send(TCommand request, CancellationToken cancellationToken = default)
        {
            var correlationId = Guid.NewGuid();

            var props = _channel.CreateBasicProperties();

            props.CorrelationId = correlationId.ToString();
            props.ReplyTo = _replyQueue;

            var tcs = new TaskCompletionSource<TResult>();
            callbackMapper.TryAdd(correlationId.ToString(), tcs);

            var json = JsonSerializer.Serialize(request);
            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: "", routingKey: request.Queue, body: body);

            cancellationToken.Register(() => callbackMapper.TryRemove(correlationId.ToString(), out _));

            return tcs.Task;
        }

        public void Dispose()
        {
            if (_connection?.IsOpen == true)
                _connection.Close();
        }
    }
}

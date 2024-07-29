using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace Platform.RabbitMq
{
    public class CommandConsumer<TCommand>
        where TCommand : ICommand
    {
        private readonly RabbitMqOptions options;

        public CommandConsumer(RabbitMqOptions options)
        {
            this.options = options;

            var factory = new ConnectionFactory { HostName = options.Host };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var command = Activator.CreateInstance(typeof(TCommand)) as ICommand;

            channel.QueueDeclare(queue: command.Queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: command.Queue,
                                 autoAck: false,
                                 consumer: consumer);

            consumer.Received += (model, ea) =>
            {
                string response = string.Empty;

                var body = ea.Body.ToArray();
                var props = ea.BasicProperties;
                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                try
                {
                    var message = Encoding.UTF8.GetString(body);
                    int n = int.Parse(message);
                    Console.WriteLine($" [.] Fib({message})");
                    response = Fib(n).ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine($" [.] {e.Message}");
                    response = string.Empty;
                }
                finally
                {
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish(exchange: string.Empty,
                                         routingKey: props.ReplyTo,
                                         basicProperties: replyProps,
                                         body: responseBytes);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            };
        }
    }
}

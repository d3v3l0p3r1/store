namespace Platform.RabbitMq
{
    public interface ICommandPublisher<TCommand, TResult> : IDisposable
        where TCommand : ICommand
    {
        Task<TResult> Send(TCommand request, CancellationToken cancellationToken = default);
    }
}

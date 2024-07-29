namespace Platform.RabbitMq
{
    public interface ICommand
    {
        string Queue { get; } = "default";
    }
}

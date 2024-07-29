using Microsoft.Extensions.DependencyInjection;

namespace Platform.RabbitMq
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddCommandPublisher<TPublisher, TCommand, TResult>(this IServiceCollection services) 
            where TPublisher: CommandPublisher<TCommand, TResult>
            where TCommand : ICommand
        {
            services.AddScoped<ICommandPublisher<TCommand, TResult>, TPublisher>();

            return services;
        }
    }
}

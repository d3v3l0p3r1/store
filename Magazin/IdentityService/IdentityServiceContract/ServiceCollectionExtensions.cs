using IdentityServiceContract.Commands;
using IdentityServiceContract.Dto;
using IdentityServiceContract.Publishers;
using Microsoft.Extensions.DependencyInjection;
using Platform.RabbitMq;

namespace IdentityServiceContract
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddCommandPublisher<LoginCommandPublisher, LoginCommand, LoginDto>();

            return services;
        }
    }
}

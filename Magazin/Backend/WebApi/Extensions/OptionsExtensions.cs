using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneAssIntegration.Options;
using WebApi.Options;

namespace WebApi.Extensions
{
    /// <summary>
    /// Расширения для настроек
    /// </summary>
    public static class OptionsExtensions
    {
        /// <summary>
        /// Добавить настройки
        /// </summary>
        public static IServiceCollection ConfigOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MainOptions>(configuration.GetSection(nameof(MainOptions)));
            services.Configure<OneAssOptions>(configuration.GetSection(nameof(OneAssOptions)));

            return services;
        }
    }
}

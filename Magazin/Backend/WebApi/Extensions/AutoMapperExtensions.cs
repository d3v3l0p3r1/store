using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Extensions
{
    /// <summary>
    /// Расширения для AutoMapper
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// Добавить автомаппер
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToArray();
            Action<IMapperConfigurationExpression> cfg = (c) =>
            {
                //var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes().Where(z => z.IsSubclassOf(typeof(Profile))));
                //foreach (var type in types)
                //{
                //    c.AddProfile(type);
                //}
            };

            services.AddAutoMapper(cfg, assemblies, ServiceLifetime.Scoped);

            return services;
        }
    }
}

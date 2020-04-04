using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("public", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Public api",
                    Version = "v1"
                });

                config.SwaggerDoc("admin", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Admin api",
                    Version = "v2"
                });

                config.SwaggerDoc("exchange", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Exchange api",
                    Version = "v1"
                });

                foreach (var dir in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
                {
                    var xmlFile = dir;
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    config.IncludeXmlComments(xmlPath);
                }

                
            });

            return services;
        }

        public static IApplicationBuilder UseSwagg(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/public/swagger.json", "Public API V1");
                c.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin API V1");
                c.SwaggerEndpoint("/swagger/exchange/swagger.json", "Exchange API V1");
                c.RoutePrefix = string.Empty;
            });


            return app;
        }
    }
}

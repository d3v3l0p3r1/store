using System.IO;
using BaseCore.Security.Entities;
using DataCore.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.SpaServices;
using WebUiAdmin.Models;
using System;
using WebApi.Extensions;
using AutoMapper;
using WebApi.Mappings.Documents;
using System.Reflection;
using Microsoft.AspNetCore.HttpOverrides;

namespace WebUiAdmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InitDbContext(services);

            Bindings.Bind(services);

            services.InitAuth(Configuration);

            services.AddControllers()
                .AddNewtonsoftJson(config =>
                {
                    config.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });


            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Magazin api",
                    Version = "v1"
                });

                config.SwaggerDoc("admin", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Admin api",
                    Version = "v2"
                });

                foreach (var dir in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
                {
                    var xmlFile = dir;
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    config.IncludeXmlComments(xmlPath);
                }
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().Build());

            app.UseStaticFiles(new StaticFileOptions()
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "image/png",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(config =>
            {
                config.MapControllers();
            });
        }

        private void InitDbContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString(nameof(DataContext));

            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(connectionString, builder =>
                {
                    builder.MigrationsAssembly("WebApi");
                });
            });
        }
    }
}

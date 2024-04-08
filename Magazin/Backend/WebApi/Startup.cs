﻿using System.IO;
using BaseCore.DAL.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebApi.Extensions;

namespace WebApi
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
            services.ConfigOptions(Configuration);

            InitDbContext(services);

            Bindings.Bind(services);

            services.InitAuth(Configuration);

            services.AddControllers()
                .AddNewtonsoftJson(config =>
                {
                    config.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    config.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                    //config.SerializerSettings.DateFormatString = "dd.MM.yyyy HH:mm:ss";
                });

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddSwagger();

            services.RegisterAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();
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
            app.UseSwagg();
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

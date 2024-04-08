using System;
using System.IO;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations;
using BaseCore.Security.Services.Concrete;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using WebApi.Migrations;
using WebApi.Options;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {

                logger.Information("Application start, WebUIAdmin");
                var host = BuildWebHost(args); 
                InitUsers(host).Wait();
                host.Run();
                logger.Information("Application end, WebUIAdmin");

            }
            catch (Exception e)
            {
                logger.Error(e, "Start application error.");
                throw;
            }
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:51145")
                .UseSerilog()
                .UseKestrel(config =>
                {
                    config.ListenAnyIP(51145);
                    config.Limits.MaxRequestBodySize = long.MaxValue;
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

        private static async Task InitUsers(IWebHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dataContext = services.GetService<DataContext>();
            var userManager = services.GetRequiredService<UserManager>();
            var roleManager = services.GetRequiredService<RoleManager>();

            dataContext.Database.Migrate();

            await DbMigrationsConfiguration.InitializeAsync(userManager, roleManager);

            var config = services.GetService<IWebHostEnvironment>();
            var options = services.GetRequiredService<IOptions<MainOptions>>();

            if (!config.IsProduction())
            {
                if (options.Value.UseOneAssIntegration)
                {
                    //await services.GetService<IProductFetcher>().LoadProducts();
                }
                else
                {
                    DbMigrationsConfiguration.Seed(dataContext);
                }
            }
        }
    }
}

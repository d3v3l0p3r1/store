using System.IO;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations;
using BaseCore.Security.Services.Concrete;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OneAssIntegration.Services.Abstractions;
using WebApi.Migrations;
using WebApi.Options;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            InitUsers(host).Wait();

            host.Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(config =>
                {
                    config.ListenAnyIP(51145);
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://localhost:51145")
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

            if (config.IsDevelopment())
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Security.Services.Concrete;
using DataCore.DAL;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebUiAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            InitUsers(host);

            host.Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()                
                .Build();

        private static async void InitUsers(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dataContext = services.GetService<DataContext>();
                var userManager = services.GetRequiredService<UserManager>();
                var roleManager = services.GetRequiredService<RoleManager>();

                await DbMigrationsConfiguration.InitializeAsync(userManager, roleManager);

                DbMigrationsConfiguration.Seed(dataContext);
            }

        }
    }
}

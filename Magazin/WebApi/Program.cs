﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Security.Services.Concrete;
using DataCore.DAL;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebUiAdmin
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
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://localhost:51145")
                .UseIISIntegration()
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

            if (!config.IsProduction())
            {
                DbMigrationsConfiguration.Seed(dataContext);
            }
        }
    }
}

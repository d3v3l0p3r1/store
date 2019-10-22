using BaseCore.Security.Entities;
using DataCore.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection InitAuth(this IServiceCollection services)
        {

            services.AddIdentity<User, Role>(opts =>
            {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                opts.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddConfigurationStore()
                .AddAspNetIdentity<User>()
                .AddApiAuthorization<User, DataContext>( config => { });

            services.AddAuthentication();
            services.AddAuthorization();

            return services;
        }
    }
}

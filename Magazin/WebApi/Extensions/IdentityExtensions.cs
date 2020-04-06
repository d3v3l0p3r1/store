using BaseCore.Security.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BaseCore.DAL.Implementations;
using WebApi.Models;

namespace WebApi.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection InitAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(DataContext));

            services.AddDefaultIdentity<User>(opts =>
            {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                opts.User.RequireUniqueEmail = true;
            })
                .AddRoles<Role>()
                .AddSignInManager()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddInMemoryClients(IdentityConfig.GetClients())
                .AddInMemoryApiResources(IdentityConfig.GetApis());


            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = AuthOptions.ISSUER;
                    options.Audience = AuthOptions.AUDIENCE;
                    options.SaveToken = true;

                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,

                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,

                        ValidateLifetime = true,

                        IssuerSigningKey = AuthOptions.GetKey(),
                        ValidateIssuerSigningKey = true,
                    };
                }).AddIdentityServerJwt();

            services.AddAuthorization(x =>
            {

            });

            return services;
        }
    }
}

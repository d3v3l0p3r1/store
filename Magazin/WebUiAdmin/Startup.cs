using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using BaseCore.Security.Entities;
using BaseCore.Security.Services.Abstract;
using BaseCore.Security.Services.Concrete;
using BaseCore.Services.Abstract;
using DataCore.DAL;
using DataCore.Services.Abstract;
using DataCore.Services.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using WebUiAdmin.Models;

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

            services.AddIdentity<User, Role>(opts =>
            {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре        
                opts.User.RequireUniqueEmail = true;                
            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

            services.AddMvc().AddJsonOptions(opts =>
            {
                opts.SerializerSettings.ContractResolver = new DefaultContractResolver();                
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer =  AuthOptions.ISSUER,

                        ValidateAudience =  true,
                        ValidAudience =  AuthOptions.AUDIENCE,

                        IssuerSigningKey = AuthOptions.GetKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddAuthorization(options =>
            {                
            });

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().Build());

            app.UseStaticFiles(new StaticFileOptions()
            {                 
                ServeUnknownFileTypes = true,
                DefaultContentType = "image/png",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),               
            });

            app.UseAuthentication();       

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            
        }

        private void InitDbContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString(nameof(DataContext));

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DataContext>(options => options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("WebUiAdmin")));
        }
    }
}

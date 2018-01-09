using System.IO;
using BaseCore.Security.Entities;
using BaseCore.Security.Services.Abstract;
using BaseCore.Security.Services.Concrete;
using BaseCore.Services.Abstract;
using DataCore.DAL;
using DataCore.Services.Abstract;
using DataCore.Services.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;

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
            }).AddEntityFrameworkStores<DataContext>();

            services.AddMvc().AddJsonOptions(opts =>
            {
                opts.SerializerSettings.ContractResolver = new DefaultContractResolver();
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

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<DataContext>(options => options.UseNpgsql(connectionString, a => a.MigrationsAssembly("DataCore")));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Entities;
using BaseCore.Security.Services.Abstract;
using BaseCore.Security.Services.Concrete;
using BaseCore.Services.Abstract;
using DataCore.DAL;
using DataCore.Entities;
using DataCore.Services.Abstract;
using DataCore.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using WebUiAdmin.Concrete;

namespace WebUiAdmin
{
    public static class Bindings
    {
        public static void Bind(IServiceCollection services)
        {
            services.AddTransient<RoleManager>();
            services.AddTransient<UserManager>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IKindService, KindService>();

            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<ProductCategory>, Repository<ProductCategory>>();
            services.AddScoped<IRepository<FileData>, Repository<FileData>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IRepository<News>, Repository<News>>();
            services.AddScoped<IRepository<ProductKind>, Repository<ProductKind>>();

            services.AddScoped<IFileService, FileService>();
        }
    }
}

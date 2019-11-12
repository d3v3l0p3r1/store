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
using DataCore.Entities.Documents;
using DataCore.Repositories.Abstract;
using DataCore.Repositories.Concrete;
using DataCore.Services.Abstract;
using DataCore.Services.Abstract.Documents;
using DataCore.Services.Concrete;
using DataCore.Services.Concrete.Documents;
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
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<IIncomingDocumentService, IncomingDocumentService>();
            services.AddScoped<IOutcomingDocumentService, OutcomingDocumentService>();

            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<ProductCategory>, Repository<ProductCategory>>();
            services.AddScoped<IRepository<FileData>, Repository<FileData>>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IRepository<News>, Repository<News>>();
            services.AddScoped<IRepository<ProductKind>, Repository<ProductKind>>();
            services.AddScoped<IBalanceRepository, BalanceRepository>();
            services.AddScoped<IRepository<IncomingDocument>, Repository<IncomingDocument>>();
            services.AddScoped<IRepository<ProductImage>, Repository<ProductImage>>();
            services.AddScoped<IRepository<IncomingDocumentEntry>, Repository<IncomingDocumentEntry>>();
            services.AddScoped<IRepository<OutComingDocument>, Repository<OutComingDocument>>();
            services.AddScoped<IRepository<OutComingDocumentEntry>, Repository<OutComingDocumentEntry>>();

            services.AddScoped<IFileService, FileService>();


        }
    }
}

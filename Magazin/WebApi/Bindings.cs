using BaseCore.Catalogues.Services.Abstract;
using BaseCore.Catalogues.Services.Concrete;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.DAL.Implementations.Entities.Documents;
using BaseCore.Documents.Abstractions.Services;
using BaseCore.Documents.Implementations.Repositories.Abstractions;
using BaseCore.Documents.Implementations.Repositories.Implementations;
using BaseCore.Documents.Implementations.Services;
using BaseCore.Documents.Implementations.Services.Abstractions;
using BaseCore.File;
using BaseCore.News.Services.Abstractions;
using BaseCore.News.Services.Implementations;
using BaseCore.Products.Abstractions.Services;
using BaseCore.Products.Implementations.Services;
using BaseCore.Security.Services.Abstract;
using BaseCore.Security.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using OneAssIntegration.Services.Abstractions;
using OneAssIntegration.Services.Implementations;
using WebApi.Concrete;

namespace WebApi
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
            services.AddScoped<IContractorService, ContractorService>();
            services.AddScoped<ICarouselService, CarouselService>();
            services.AddScoped<IBrandService, BrandService>();

            #region Repositories

            services.AddScoped<IRepository<Brand>, Repository<Brand>>();
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
            services.AddScoped<IRepository<Contractor>, Repository<Contractor>>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IRepository<Carousel>, Repository<Carousel>>();
            #endregion

            #region OneAss

            services.AddScoped<IRepository<OneAssSync>, Repository<OneAssSync>>();
            services.AddScoped<IProductFetcher, ProductFetcher>();

            #endregion



        }
    }
}

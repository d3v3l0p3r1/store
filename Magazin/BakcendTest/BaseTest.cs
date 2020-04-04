using BackendTest.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.DAL.Implementations.Entities.Documents;
using BaseCore.Documents.Implementations.Repositories.Implementations;
using BaseCore.Documents.Implementations.Services;
using BaseCore.Products.Implementations.Services;

namespace WebUiAdminTest
{
    public abstract class BaseTest
    {
        public IServiceCollection ServiceCollections;

        protected DataContext DataContext;
        protected BalanceService BalanceService;
        protected BalanceRepository BalanceRepository;

        protected Repository<ProductImage> ProductImageRepository;
        protected ProductService ProductService;
        protected Repository<Product> ProductRepository;

        protected Repository<ProductCategory> ProductCategoryRepository;
        protected ProductCategoryService ProductCategoryService;

        protected Repository<IncomingDocumentEntry> IncomingDocumentEntryRepository;
        protected Repository<IncomingDocument> IncomingDocumentRepository;
        protected IncomingDocumentService IncomingDocumentService;

        protected Repository<OutComingDocumentEntry> OutcomingDocumentEntryRepository;
        protected Repository<OutComingDocument> OutcomingDocumentRepository;
        protected OutcomingDocumentService OutcomingDocumentService;

        protected Repository<ProductKind> ProductKingRepository;
        protected KindService ProductKindService;

        protected DummyFileService DummyFileService;

        public BaseTest()
        {
            InitScope("default");
        }

        public void InitScope(string dbName)
        {
            ServiceCollections = new ServiceCollection();

            ServiceCollections.AddDbContext<DataContext>(config =>
            {
                config.UseInMemoryDatabase(dbName);
            });
            
            ServiceCollections.AddIdentityServer()
               .AddInMemoryClients(IdentityConfig.GetClients())
               .AddInMemoryApiResources(IdentityConfig.GetApis());

            ;

            var serviceProvider = ServiceCollections.BuildServiceProvider();
            DataContext = serviceProvider.GetService<DataContext>();

            BalanceRepository = new BalanceRepository(DataContext);
            ProductRepository = new Repository<Product>(DataContext);
            ProductImageRepository = new Repository<ProductImage>(DataContext);

            DummyFileService = new DummyFileService();
            BalanceService = new BalanceService(BalanceRepository);
            //BrandService = new BrandService();
           // ProductService = new ProductService(ProductRepository, BalanceService, DummyFileService, ProductImageRepository, new BrandService(), );

            ProductCategoryRepository = new Repository<ProductCategory>(DataContext);
            ProductCategoryService = new ProductCategoryService(ProductCategoryRepository);

            IncomingDocumentEntryRepository = new Repository<IncomingDocumentEntry>(DataContext);
            IncomingDocumentRepository = new Repository<IncomingDocument>(DataContext);
            IncomingDocumentService = new IncomingDocumentService(IncomingDocumentRepository, BalanceService, IncomingDocumentEntryRepository);

            OutcomingDocumentEntryRepository = new Repository<OutComingDocumentEntry>(DataContext);
            OutcomingDocumentRepository = new Repository<OutComingDocument>(DataContext);
            OutcomingDocumentService = new OutcomingDocumentService(OutcomingDocumentRepository, BalanceService, OutcomingDocumentEntryRepository);

            ProductKingRepository = new Repository<ProductKind>(DataContext);
            ProductKindService = new KindService(ProductKingRepository);
        }

        public static class IdentityConfig
        {

            public static IEnumerable<IdentityServer4.Models.ApiResource> GetApis()
            {
                return new List<IdentityServer4.Models.ApiResource>
            {
                new IdentityServer4.Models.ApiResource("API1", "Api1")
            };
            }

            public static IEnumerable<IdentityServer4.Models.Client> GetClients()
            {
                return new List<IdentityServer4.Models.Client>
            {
                new IdentityServer4.Models.Client
                {
                    ClientId = "js",
                    ClientName = "Js client",

                }
            };
            }

        }

        [SetUp]
        public async Task InitializeDb()
        {
            var category = new ProductCategory()
            {
                Title = "Test Category 1"
            };

            await ProductCategoryService.CreateAsync(category);

            var product = new Product()
            {
                Category = category,
                Title = "Test product 1",
            };

            await ProductService.CreateAsync(product);
        }

        protected async Task<IncomingDocument> CreateIncomingDocument(Product product, int count)
        {
            var incomingDocument = new IncomingDocument
            {
                Entries = new List<IncomingDocumentEntry>
                {
                    {
                        new IncomingDocumentEntry
                        {
                            Count = count,
                            Product = product
                        }
                    }
                }
            };

            await IncomingDocumentService.CreateAsync(incomingDocument);
            await IncomingDocumentService.Apply(incomingDocument.Id);

            return incomingDocument;
        }

        protected async Task<OutComingDocument> CreateOutcomingDocument(Product product, int count)
        {
            var outcomingDocument = new OutComingDocument()
            {
                Entries = new List<OutComingDocumentEntry>()
                {
                    {
                        new OutComingDocumentEntry()
                        {
                            Count = count,
                            Product = product
                        }
                    }
                }
            };

            await OutcomingDocumentService.CreateAsync(outcomingDocument);

            return outcomingDocument;
        }
    }
}

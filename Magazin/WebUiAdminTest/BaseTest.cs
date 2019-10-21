using DataCore.DAL;
using DataCore.Entities;
using DataCore.Entities.Documents;
using DataCore.Repositories.Concrete;
using DataCore.Services.Concrete;
using DataCore.Services.Concrete.Documents;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebUiAdminTest
{
    public abstract class BaseTest
    {
        protected DataContext DataContext;
        protected BalanceService BalanceService;
        protected BalanceRepository BalanceRepository;

        protected ProductService ProductService;
        protected Repository<Product> ProductRepository;

        protected Repository<ProductCategory> ProductCategoryRepository;
        protected ProductCategoryService ProductCategoryService;

        protected Repository<IncomingDocument> IncomingDocumentRepository;
        protected IncomingDocumentService IncomingDocumentService;

        protected Repository<OutComingDocument> OutcomingDocumentRepository;
        protected OutcomingDocumentService OutcomingDocumentService;

        protected Repository<ProductKind> ProductKingRepository;
        protected KindService ProductKindService;

        public BaseTest()
        {
            InitScope("default");
        }

        public void InitScope(string dbName)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();

            builder.UseInMemoryDatabase(dbName);

            DataContext = new DataContext(builder.Options);

            BalanceRepository = new BalanceRepository(DataContext);
            ProductRepository = new Repository<Product>(DataContext);

            BalanceService = new BalanceService(BalanceRepository);
            ProductService = new ProductService(ProductRepository, BalanceService);

            ProductCategoryRepository = new Repository<ProductCategory>(DataContext);
            ProductCategoryService = new ProductCategoryService(ProductCategoryRepository);

            IncomingDocumentRepository = new Repository<IncomingDocument>(DataContext);
            IncomingDocumentService = new IncomingDocumentService(IncomingDocumentRepository, BalanceService);

            OutcomingDocumentRepository = new Repository<OutComingDocument>(DataContext);
            OutcomingDocumentService = new OutcomingDocumentService(OutcomingDocumentRepository, BalanceService);

            ProductKingRepository = new Repository<ProductKind>(DataContext);
            ProductKindService = new KindService(ProductKingRepository);
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
                Price = 100,
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
            await IncomingDocumentService.Apply(incomingDocument);

            return incomingDocument;
        }

        protected async Task<OutComingDocument> CreateOutcomingDocument(Product product, int count)
        {
            var outcomingDocument = new OutComingDocument()
            {
                Entry = new List<OutComingDocumentEntry>()
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

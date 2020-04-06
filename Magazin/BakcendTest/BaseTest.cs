using System;
using BackendTest.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.DAL.Implementations.Entities.Documents;
using BaseCore.Documents.Abstractions.Services;
using BaseCore.Documents.Implementations.Repositories.Implementations;
using BaseCore.Documents.Implementations.Services;
using BaseCore.Documents.Implementations.Services.Abstractions;
using BaseCore.File;
using BaseCore.Products.Implementations.Services;
using WebApi;

namespace WebUiAdminTest
{
    public abstract class BaseTest
    {
        public IServiceCollection ServiceCollections;
        public IServiceProvider ServiceProvider;
        public IRepository Repository;

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

            Bindings.Bind(ServiceCollections);

            ServiceCollections.AddScoped<IFileService, DummyFileService>();

            ServiceProvider = ServiceCollections.BuildServiceProvider();

            Repository = ServiceProvider.GetService<IRepository>();
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
            
            await Repository.CreateAsync(category);

            var product = new Product()
            {
                Category = category,
                Title = "Test product 1",
            };

            await Repository.CreateAsync(product);
        }

        protected async Task<IncomingDocument> CreateIncomingDocument(Product product, int count)
        {
            var incomingDocumentService = ServiceProvider.GetService<IIncomingDocumentService>();
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

            await incomingDocumentService.CreateAsync(incomingDocument);
            await incomingDocumentService.Apply(incomingDocument.Id);

            return incomingDocument;
        }

        protected async Task<OutComingDocument> CreateOutcomingDocument(Product product, int count)
        {
            var outcomingDocumentService = ServiceProvider.GetService<IOutcomingDocumentService>();
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

            await outcomingDocumentService.CreateAsync(outcomingDocument);

            return outcomingDocument;
        }
    }
}

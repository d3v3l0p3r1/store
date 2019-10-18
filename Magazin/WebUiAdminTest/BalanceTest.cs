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
    [TestFixture]
    public class BalanceTest : BaseTest
    {

        private ProductCategory _productCategory;
        private Product _product;

        public BalanceTest()
        {

        }

        [SetUp]
        public async Task Setup()
        {
            _productCategory = new ProductCategory()
            {
                Title = "Balance test category"
            };

            await ProductCategoryService.CreateAsync(_productCategory);

            _product = new Product()
            {
                Category = _productCategory,
                Title = "Test balance product"
            };

            await ProductService.CreateAsync(_product);
        }

        [Test]
        public async Task AddToBalanceTest()
        {
            var incomingDocument = new IncomingDocument()
            {
                Date = DateTime.Now,
                Entries = new List<IncomingDocumentEntry>()
            };

            var entry = new IncomingDocumentEntry()
            {
                Count = 100,
                Product = _product
            };

            incomingDocument.Entries.Add(entry);

            await IncomingDocumentService.CreateAsync(incomingDocument);

            var count = await BalanceService.GetBalance(_product);

            Assert.AreEqual(100, count);
        }


        [Test]
        public async Task RemoveFromBalanceTest()
        {
            var product = new Product()
            {
                Category = _productCategory,
                Title = "Remove Title"
            };
            await ProductService.CreateAsync(product);

            var incomingDocument = new IncomingDocument
            {
                Entries = new List<IncomingDocumentEntry>
                {
                    { 
                        new IncomingDocumentEntry
                        {
                            Count = 100,
                            Product = product
                        }
                    }
                }
            };

            await IncomingDocumentService.CreateAsync(incomingDocument);

            var outcomingDocument = new OutComingDocument()
            {
                Entry = new List<OutComingDocumentEntry>()
                {
                    {
                        new OutComingDocumentEntry()
                        {
                            Count = 100,
                            Product = product
                        }
                    }
                }
            };

            await OutcomingDocumentService.CreateAsync(outcomingDocument);

            var count = await BalanceService.GetBalance(_product);
            Assert.AreEqual(0, count);
        }
    }
}

using DataCore.DAL;
using DataCore.Entities;
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
            await BalanceService.AddToBalance(_product, 100);
            var count = await BalanceService.GetBalance(_product);

            Assert.AreEqual(100, count);
        }
    }
}

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
    public abstract class BaseTest
    {
        protected readonly DataContext DataContext;
        protected BalanceService BalanceService;
        protected BalanceRepository BalanceRepository;

        protected ProductService ProductService;
        protected Repository<Product> ProductRepository;

        protected Repository<ProductCategory> ProductCategoryRepository;
        protected ProductCategoryService ProductCategoryService;

        public BaseTest()
        {
            var builder = new DbContextOptionsBuilder<DataContext>();

            builder.UseInMemoryDatabase("testBase1");

            DataContext = new DataContext(builder.Options);

            BalanceRepository = new BalanceRepository(DataContext);
            ProductRepository = new Repository<Product>(DataContext);

            BalanceService = new BalanceService(BalanceRepository);
            ProductService = new ProductService(ProductRepository);

            ProductCategoryRepository = new Repository<ProductCategory>(DataContext);
            ProductCategoryService = new ProductCategoryService(ProductCategoryRepository);
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
    }
}

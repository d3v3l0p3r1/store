using NUnit.Framework;
using System.Threading.Tasks;
using BaseCore.DAL.Abstractions.Repositories;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.DAL.Implementations.Exceptions.Balance;
using BaseCore.Products.Abstractions.Services;
using BaseCore.Products.Implementations.Services;
using Microsoft.Extensions.DependencyInjection;

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

            var repository = ServiceProvider.GetService<IRepository>();
            await repository.CreateAsync(_productCategory);

            _product = new Product()
            {
                Category = _productCategory,
                Title = "Test balance product"
            };

            await repository.CreateAsync(_product);
        }


        [Test(Description = "Документ расхода")]
        [Order(1)]
        public async Task RemoveFromBalanceTest()
        {
            var repository = ServiceProvider.GetService<IRepository>();
            var documentBalanceService = ServiceProvider.GetService<IBalanceService>();

            var product = new Product()
            {
                Category = _productCategory,
                Title = "Remove Title"
            };

            await repository.CreateAsync(product);

            await CreateIncomingDocument(product, 100);

            await CreateOutcomingDocument(product, 100);

            var count = await documentBalanceService.GetBalance(_product.Id);
            Assert.AreEqual(0, count);
        }

        [Test(Description = "Документ прихода")]
        [Order(2)]
        public async Task AddToBalanceTest()
        {
            var balanceService = ServiceProvider.GetService<IBalanceService>();
            await CreateIncomingDocument(_product, 100);

            var count = await balanceService.GetBalance(_product.Id);

            Assert.AreEqual(100, count);
        }

        [Test(Description = "Попытка списать больше чем есть на балансе")]
        [Order(3)]
        public async Task BelowZeroBalanceTest()
        {
            var repository = ServiceProvider.GetService<IRepository>();
            var product = new Product()
            {
                Category = _productCategory
            };

            await repository.CreateAsync(product);
            await CreateIncomingDocument(product, 10);

            Assert.Throws<BalanceBelowZeroException>(() =>
            {
                var task = CreateOutcomingDocument(product, 100);

                task.GetAwaiter().GetResult();
            });
        }

        [Test(Description = "Когда количетсво продукта становится равным 0 надо выставить ZeroDate и создать новый баланс, т.к. это удобнее для подсчета")]
        [Order(4)]
        public async Task CreateNewBalanceTest()
        {
            var repository = ServiceProvider.GetService<IRepository>();
            var balanceService = ServiceProvider.GetService<IBalanceService>();
            var product = new Product()
            {
                Category = _productCategory,
            };
            await repository.CreateAsync(product);

            await CreateIncomingDocument(product, 100);

            var balanceBefore = await balanceService.GetBalance(product.Id);

            await CreateOutcomingDocument(product, 100);
            await CreateIncomingDocument(product, 100);

            var lastBalance = await balanceService.GetBalance(product.Id);

            Assert.AreNotEqual(balanceBefore, lastBalance);

        }

      
    }
}

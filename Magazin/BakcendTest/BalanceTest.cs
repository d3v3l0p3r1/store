using NUnit.Framework;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.DAL.Implementations.Exceptions.Balance;

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


        [Test(Description = "Документ расхода")]
        [Order(1)]
        public async Task RemoveFromBalanceTest()
        {
            var product = new Product()
            {
                Category = _productCategory,
                Title = "Remove Title"
            };
            await ProductService.CreateAsync(product);

            await CreateIncomingDocument(product, 100);

            await CreateOutcomingDocument(product, 100);

            var count = await BalanceService.GetBalance(_product);
            Assert.AreEqual(0, count);
        }

        [Test(Description = "Документ прихода")]
        [Order(2)]
        public async Task AddToBalanceTest()
        {
            await CreateIncomingDocument(_product, 100);

            var count = await BalanceService.GetBalance(_product);

            Assert.AreEqual(100, count);
        }

        [Test(Description = "Попытка списать больше чем есть на балансе")]
        [Order(3)]
        public async Task BelowZeroBalanceTest()
        {
            var product = new Product()
            {
                Category = _productCategory
            };

            await ProductService.CreateAsync(product);

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
            var product = new Product()
            {
                Category = _productCategory,
            };
            await ProductService.CreateAsync(product);

            await CreateIncomingDocument(product, 100);

            var balanceBefore = await BalanceService.Get(product);

            await CreateOutcomingDocument(product, 100);
            await CreateIncomingDocument(product, 100);

            var lastBalance = await BalanceService.Get(product);

            Assert.AreNotEqual(balanceBefore.Id, lastBalance.Id);

        }

      
    }
}

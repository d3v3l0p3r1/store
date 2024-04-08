using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Services;
using BaseCore.Products.Implementations.Services;
using Microsoft.Extensions.DependencyInjection;

namespace WebUiAdminTest
{
    [TestFixture]
    public class ProductsTest : BaseTest
    {
        [Test(Description = "Получиение списка продуктов с учетом баланса")]
        public async Task GetProductsWithBalanceTest()
        {

            var kind = new ProductKind();
            await Repository.CreateAsync(kind);

            var cateogry = new ProductCategory();
            await Repository.CreateAsync(cateogry);

            var product = new Product()
            {
                CategoryId = cateogry.Id,
                KindId = kind.Id
            };

            await Repository.CreateAsync(product);

            await CreateIncomingDocument(product, 100);

            var product2 = new Product() 
            {
                CategoryId = cateogry.Id,
                KindId = kind.Id
            };
            await Repository.CreateAsync(product2);

            await CreateIncomingDocument(product2, 100);
            await CreateOutcomingDocument(product2, 100);

            var balanceService = ServiceProvider.GetService<IBalanceService>();
            var list = balanceService.GetWithBalance(cateogry.Id);

            Assert.IsTrue(list.Any());
            Assert.IsFalse(list.Any(x => x.Id == product2.Id));
        }
    }
}

using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;

namespace WebUiAdminTest
{
    [TestFixture]
    public class ProductsTest : BaseTest
    {
        [Test(Description = "Получиение списка продуктов с учетом баланса")]
        public async Task GetProductsWithBalanceTest()
        {
            var kind = new ProductKind();
            await ProductKindService.CreateAsync(kind);

            var cateogry = new ProductCategory();
            await ProductCategoryService.CreateAsync(cateogry);

            var product = new Product()
            {
                CategoryId = cateogry.Id,
                KindId = kind.Id
            };

            await ProductService.CreateAsync(product);

            await CreateIncomingDocument(product, 100);

            var product2 = new Product() 
            {
                CategoryId = cateogry.Id,
                KindId = kind.Id
            };
            await ProductService.CreateAsync(product2);

            await CreateIncomingDocument(product2, 100);
            await CreateOutcomingDocument(product2, 100);

            var list = BalanceService.GetProductBalance(cateogry.Id, "http", "localhost");

            Assert.IsTrue(list.Any());
            Assert.IsFalse(list.Any(x => x.Id == product2.Id));
        }
    }
}

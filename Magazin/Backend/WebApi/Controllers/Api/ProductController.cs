using System.Linq;
using System.Threading.Tasks;
using BaseCore.Products.Abstractions.Models;
using BaseCore.Products.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers.Api
{
    /// <summary>
    /// Продукты
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "public")]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="productService"></param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <param name="take"></param>
        /// <param name="page"></param>
        /// <param name="catID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ListRespone<ProductDto>), 200)]
        public async Task<IActionResult> GetAll(int take = 20, int page = 1, int? catID = null)
        {
            var skip = (page - 1) * take;
            var all = _productService.GetAllAsNotracking();

            if (catID != null)
            {
                all = all.Where(x => x.CategoryId == catID);
            }

            var total = await all.CountAsync();

            all = all.Skip(skip).Take(take);

            var res = all.Select(x => new ProductDto
            {
                Id = x.Id,
                Description = x.Description,
                Title = x.Title,
                Kind = x.Kind.Title,
                FileId = x.FileId,
                Price = x.Price
            });

            var listResponse = new ListRespone<ProductDto>()
            {
                Data = await res.ToListAsync(),
                Total = total
            };

            return Ok(listResponse);
        }


    }
}
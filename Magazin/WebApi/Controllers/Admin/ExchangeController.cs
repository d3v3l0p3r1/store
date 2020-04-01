using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Admin.Exchange;

namespace WebApi.Controllers.Admin
{
    /// <summary>
    /// Контроллер для обмена с 1С
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "exchange")]
    public class ExchangeController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        /// <summary>
        /// ctor
        /// </summary>
        public ExchangeController(IBrandService brandService, IProductCategoryService productCategoryService, IProductService productService)
        {
            _brandService = brandService;
            _productCategoryService = productCategoryService;
            _productService = productService;
        }

        /// <summary>
        /// Создать бренд
        /// </summary>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateBrand(CreateBrandModel model)
        {
            await _brandService.CreateAsync(new Brand()
            {
                Title = model.Title,
                ExternalId = model.Id,
                Description = model.Description
            });

            return Ok();
        }

        /// <summary>
        /// Создать категорию
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateCategory(CreateCategoryModel model)
        {
            await _productCategoryService.CreateAsync(model.Title, model.Id, model.ParentId);
            return Ok();
        }

        /// <summary>
        /// Создать продукт
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateProduct()
        {
            return Ok();
        }
    }
}

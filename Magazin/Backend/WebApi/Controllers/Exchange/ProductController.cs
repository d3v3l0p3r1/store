using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Models;
using BaseCore.Products.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Exchange
{
    /// <summary>
    /// Обмен продуктами
    /// </summary>
    [Route("api/exchange/[controller]")]
    [ApiController]
    [Authorize(Roles = "exchange")]
    [ApiExplorerSettings(GroupName = "exchange")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// ctor
        /// </summary>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        /// <summary>
        /// Создать продукт
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductRequestModel requestModel)
        {
            await _productService.CreateAsync(requestModel);
            return Ok();
        }

        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> UpdateProduct(ProductRequestModel requestModel)
        {
            await _productService.UpdateAsync(requestModel);

            return Ok();
        }

        /// <summary>
        /// Получить продукт
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Product), 200)]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _productService.GetByExternalId(id);
            if (product != null)
            {
                return Ok(product);

            }
            else
            {
                return NotFound();
            }
        }
    }
}
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Admin.Exchange;

namespace WebApi.Controllers.Exchange
{
    /// <summary>
    /// Обмен категориями
    /// </summary>
    [Route("api/exchange/[controller]")]
    [ApiController]
    [Authorize(Roles = "exchange")]
    [ApiExplorerSettings(GroupName = "exchange")]
    public class CategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        
        /// <summary>
        /// ctor
        /// </summary>
        public CategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        /// <summary>
        /// Создать категорию
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateCategory(CategoryRequestModel requestModel)
        {
            await _productCategoryService.CreateAsync(requestModel.Title, requestModel.Id, requestModel.ParentId);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCategory(CategoryRequestModel requestModel)
        {
            await _productCategoryService.UpdateAsync(requestModel.Title, requestModel.Id, requestModel.ParentId);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductCategory), 200)]
        public async Task<IActionResult> GetCategory(string id)
        {
            var cat = await _productCategoryService.GetByExternalIdAsync(id);
            if (cat != null)
            {
                return Ok(cat);

            }
            else
            {
                return NotFound();
            }
        }
    }
}
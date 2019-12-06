using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.DAL;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Категории продукта
    /// </summary>
    [ApiExplorerSettings(GroupName = "admin")]
    public class CategoryController : BaseController<ProductCategory>
    {
        private readonly IProductCategoryService _productCategoryService;

        public CategoryController(IProductCategoryService productCategoryService) : base(productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ListRespone<ProductCategory>), 200)]
        public async Task<IActionResult> GetAll(long? parentId = null)
        {
            var all = _productCategoryService.GetAllAsNotracking();

            if (parentId != null)
            {
                all = all.Where(x => x.ParentId == parentId.Value);
            }
            else
            {
                all = all.Where(x => x.ParentId == null);
            }

            var total = await all.CountAsync();
            var cats = await all.ToListAsync();

            var result = new ListRespone<ProductCategory>()
            {
                Data = cats,
                Total = total
            };

            return Ok(result);
        }
    }
}
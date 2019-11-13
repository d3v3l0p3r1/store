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
        public async Task<IActionResult> GetAll(int take = 10, int page = 1)
        {
            if (take < 1)
            {
                take = 10;
            }

            if (page < 1)
            {
                page = 1;
            }

            var skip = (page - 1) * take;

            var all = _productCategoryService.GetAllAsNotracking();

            var total = await all.CountAsync();
            var cats = await all.Skip(skip).Take(take).ToListAsync();

            var result = new ListRespone<ProductCategory>()
            {
                Data = cats,
                Total = total
            };

            return Ok(result);
        }
    }
}
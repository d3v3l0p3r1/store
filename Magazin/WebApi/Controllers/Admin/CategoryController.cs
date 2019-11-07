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
        [ProducesResponseType(typeof(ListRespone<Lookup>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var all = _productCategoryService.GetAllAsNotracking();

            var total = await all.CountAsync();
            var cats = await all.Select(x => new Lookup() { Id = x.Id, Title = x.Title }).ToListAsync();

            var result = new ListRespone<Lookup>()
            {
                Data = cats,
                Total = total
            };

            return Ok(result);
        }
    }
}
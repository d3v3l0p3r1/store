using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Api.Category;

namespace WebUiAdmin.Controllers.Api
{
    /// <summary>
    /// Категории продукта
    /// </summary>
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IProductCategoryService _categoryService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="categoryService"></param>
        public CategoryController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ListRespone<ProductCategory>), 200)]
        public async Task<IActionResult> GetAll(long? parentId = null)
        {
            var all = _categoryService.GetQuery()
                .OrderBy(x => x.SortOrder)
                .Include(x => x.Childs)
                .ThenInclude(x => x.Childs)
                .ThenInclude(x => x.Childs)
                .ThenInclude(x => x.Childs)
                .ThenInclude(x => x.Childs)
                .ThenInclude(x => x.Childs)
                .ThenInclude(x => x.Childs)
                .ThenInclude(x => x.Childs)
                .AsNoTracking();


            var list = await all.Where(x => x.ParentId == null).ToListAsync();

            var listResponse = new ListRespone<ProductCategory>
            {
                Data = list,
                Total = 0
            };

            return Ok(listResponse);
        }
    }
}
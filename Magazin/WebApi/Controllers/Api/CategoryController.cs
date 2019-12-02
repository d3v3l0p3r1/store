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
        [ProducesResponseType(typeof(ListRespone<CategoryDto>), 200)]
        public async Task<IActionResult> GetAll(long? parentId = null)
        {
            var all = _categoryService.GetAllAsNotracking();

            all = parentId != null
                ? all.Where(x => x.ParentId == parentId.Value)
                : all.Where(x => x.ParentId == null);

            var res = await all
                .OrderBy(x => x.SortOrder)
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    SortOrder = x.SortOrder
                }).ToListAsync();

            var listResponse = new ListRespone<CategoryDto>
            {
                Data = res,
                Total = res.Count
            };

            return Ok(listResponse);
        }
    }
}
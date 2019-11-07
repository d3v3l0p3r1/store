using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers.Api
{
    /// <summary>
    /// Категории продукта
    /// </summary>
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IProductCategoryService _categoryService;

        public CategoryController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var all = _categoryService.GetAllAsNotracking();

            var res = all.Select(x => new
            {
                x.Id,                
                x.Title,
                x.SortOrder
            }).OrderBy(x => x.SortOrder).ToList();

            return Ok(res);
        }
    }
}
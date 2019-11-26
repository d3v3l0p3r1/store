using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

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

        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ListRespone<Lookup>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var all = _categoryService.GetAllAsNotracking();

            var res = await all.Select(x => new Lookup()
            {
                Id = x.Id,
                Title = x.Title,
            }).ToListAsync();

            var listResponse = new ListRespone<Lookup>() 
            {
                Data = res,
                Total = res.Count
            };


            return Ok(listResponse);
        }
    }
}
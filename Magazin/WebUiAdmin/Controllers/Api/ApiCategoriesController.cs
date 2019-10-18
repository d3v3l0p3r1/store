using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class ApiCategoriesController : Controller
    {
        private readonly IProductCategoryService _categoryService;

        public ApiCategoriesController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

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
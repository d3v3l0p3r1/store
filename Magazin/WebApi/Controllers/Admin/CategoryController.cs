using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.DAL;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Категории продукта
    /// </summary>
    public class CategoryController : BaseController<ProductCategory>
    {
        private readonly IProductCategoryService _productCategoryService;

        public CategoryController(IProductCategoryService productCategoryService) : base(productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }


        public async Task<IActionResult> GetAll()
        {
            var all = _productCategoryService.GetAllAsNotracking();

            var total = await all.CountAsync();
            var cats = await all.Select(x => new
            {
                x.Id,
                x.Title,
            }).ToListAsync();

            return new JsonResult(new
            {
                Data = cats,
                Total = total
            });
        }
    }
}
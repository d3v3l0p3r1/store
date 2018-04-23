using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.DAL;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers
{
    public class CategoryController : BaseController<ProductCategory>
    {
        private readonly IProductCategoryService _productCategoryService;

        public CategoryController(IProductCategoryService productCategoryService) :base(productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var all = _productCategoryService.GetAll();

            var total = all.Count();
            var cats = all.Select(x => new
            {
                x.Id,
                x.Title,               
            });

            return new JsonResult(new
            {
                Data = cats,
                Total = total
            });


        }        
    }
}
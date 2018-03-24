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
    public class CategoryController : BaseController
    {
        private readonly IProductCategoryService _productCategoryService;

        public CategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("api/category/all")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var all = _productCategoryService.GetAll();

            var total = all.Count();
            var cats = all.Select(x => new
            {
                x.Id,
                x.Title,
                x.Description
            });

            return new JsonResult(new
            {
                Data = cats,
                Total = total
            });


        }

        public IActionResult Edit(int id)
        {
            var cat = _productCategoryService.Find(id);
            return View(cat);
        }

        [HttpPost]
        public IActionResult Edit(ProductCategory category)
        {
            try
            {
                _productCategoryService.Update(category);
                return View("Index");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }            
        }
    }
}
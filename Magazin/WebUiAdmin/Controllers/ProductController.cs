using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.DAL;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUiAdmin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        [Produces("application/json")]
        public IActionResult GetAll(int take, int skip, int? catID = null)
        {
            if (take == 0)
            {
                take = 100;
            }

            var all = _productService.GetAll();

            if (catID != null)
            {
                all = all.Where(x => x.CategoryID == catID.Value);
            }

            var products = all.OrderByDescending(x => x.Id).Skip(skip).Take(take);
            var total = all.Count();

            return new JsonResult(new
            {
                Data = products,
                Total = total
            });

        }

        public IActionResult Edit(int id)
        {
            var product = _productService.Find(id);

            ViewBag.CategoryID = _productCategoryService.GetAll()
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Title });
            ViewData["Product"] = product;

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _productService.Update(product);
            return View("Index");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
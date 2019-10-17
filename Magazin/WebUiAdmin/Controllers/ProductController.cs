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
using Microsoft.EntityFrameworkCore;

namespace WebUiAdmin.Controllers
{
    public class ProductController : BaseController<Product>
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IKindService _kindService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService, IKindService kindService) :base(productService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _kindService = kindService;
        }

        [Produces("application/json")]
        public async Task<IActionResult> GetAll(int take, int skip, int? catID = null)
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

            var products = await all.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync();

            var total = await all.CountAsync();

            return new JsonResult(new
            {
                Data = products,
                Total = total
            });
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryID = await _productCategoryService.GetAll()
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Title }).ToListAsync();

            ViewBag.KindID = await _kindService.GetAll()
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Title }).ToListAsync();

            return View("Edit", new Product());
        }

        public override async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.FindAsync(id);

            ViewBag.CategoryID = await _productCategoryService.GetAll()
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Title })
                .ToListAsync();

            ViewBag.KindID = await _kindService.GetAll()
                .Select(x => new SelectListItem() {Value = x.Id.ToString(), Text = x.Title})
                .ToListAsync();

            ViewData["Product"] = product;

            return View(product);
        }

        [HttpPost]
        public override async Task<IActionResult> Edit(Product product)
        {
            await _productService.UpdateAsync(product);
            return View("Index");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
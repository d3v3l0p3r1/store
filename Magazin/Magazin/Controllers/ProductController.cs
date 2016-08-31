using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Data.DAL;
using Data.Services;
using Magazin.Models;
using AutoMapper;

namespace Magazin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _categoryService;

        public ProductController(IProductService productService, IProductCategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var products = _productService.All();
            return View(products.ToList());            
        }

        public ActionResult LeftMenu()
        {
            var mdl = new LeftMenuModel();
            mdl.ProductCategories = _categoryService.GetAll().ProjectTo<ProductCategoryDTO>().ToList();

            return PartialView("ProductCategories", mdl);
        }
    }
}
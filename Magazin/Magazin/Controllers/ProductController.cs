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
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Magazin.Helpers;

namespace Magazin.Controllers
{
    public class ProductController : BaseController
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
            var mdl = new DialogViewModel();
            return View(mdl);
        }

        [HttpPost]
        public JsonNetResult GetProducts([DataSourceRequest] DataSourceRequest request)
        {
            using (var uow = CreateUnitOfWork())
            {
                var products = _productService.GetAll(uow).ProjectTo<ProductDTO>();

                var result = new JsonNetResult(products.ToDataSourceResult(request));                

                return result;
            }           
        }

        public JsonNetResult GetProductCategories([DataSourceRequest]DataSourceRequest request)
        {
            using (var uow = CreateUnitOfWork())
            {

            }
        }
    }
}
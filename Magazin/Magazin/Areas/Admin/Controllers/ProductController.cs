using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Data.Entities;
using Data.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Magazin.Controllers;
using Magazin.Models;

namespace Magazin.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {

        private readonly IProductCategoryService _categoryService;
        private readonly IProductService _productService;


        public ProductController(IProductCategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public ActionResult Products()
        {
            var mdl = new DialogViewModel();
            return PartialView("Products", mdl);
        }


        public JsonResult GetCategories([DataSourceRequest]DataSourceRequest request)
        {
            using (var uow = CreateUnitOfWork())
            {
                var cats = _categoryService.GetAll(uow);

                var result = new JsonResult()
                {
                    Data = cats.ToTreeDataSourceResult(request),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return result;
            }


        }

        public JsonResult GetProducts([DataSourceRequest] DataSourceRequest request, int? categoryId)
        {
            using (var uow = CreateUnitOfWork())
            {
                var products = _productService.GetAll(uow);

                if (categoryId.HasValue)
                {
                    products = products.Where(x => x.ProductCategoryID == categoryId.Value);
                }

                var result = new JsonResult
                {
                    Data = products.ToDataSourceResult(request),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return result;
            }

        }

        public ActionResult EditProduct()
        {
            return PartialView("ProductDetailView");
        }

    }
}
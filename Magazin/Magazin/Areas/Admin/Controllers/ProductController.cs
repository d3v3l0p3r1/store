using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var mdl = new DialogViewModel();
            return PartialView("Index", mdl);
        }

        public JsonResult GetProducts([DataSourceRequest] DataSourceRequest request, int? categoryId, string filter)
        {
            using (var uow = CreateUnitOfWork())
            {
                var products = _productService.GetAll(uow);

                if (categoryId.HasValue)
                {
                    products = products.Where(x => x.ProductCategoryId == categoryId.Value);
                }

                if (!string.IsNullOrEmpty(filter))
                {
                    products = products.Where(x => x.Title.ToUpper().StartsWith(filter.ToUpper()));
                }

                var result = new JsonResult
                {
                    Data = products.ToDataSourceResult(request),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return result;
            }

        }

        public ActionResult EditProduct(int? id, int categoryId)
        {
            using (var uow = CreateUnitOfWork())
            {
                Product product;

                product = id.HasValue ? _productService.Find(uow, id.Value) : new Product { ProductCategoryId = categoryId };

                return PartialView("ProductDetailView", product);
            }
        }

        [HttpPost]
        public JsonResult UpdateProduct(Product product)
        {
            try
            {
                using (var uow = CreateUnitOfWork())
                {

                    _productService.Update(uow, product);
                    var result = new JsonResult
                    {
                        Data = new { result = "ok" },
                    };
                    return result;
                }
            }
            catch (Exception error)
            {
                var result = new JsonResult
                {
                    Data = new { error = error.Message }
                };
                return result;
            }
        }

        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            using (var uow = CreateUnitOfWork())
            {
                try
                {
                    _productService.Delete(uow, id);
                    return Json(new { result = "ok" });
                }
                catch (ArgumentException)
                {
                    return Json(new { error = "Id is 0" });
                }
            }
        }

        public JsonResult FindProduct(string startWith)
        {
            using (var uow = CreateUnitOfWork())
            {
                var products = _productService.GetAll(uow);
                if (!string.IsNullOrEmpty(startWith))
                {
                    products = products.Where(x => x.Title.ToUpper().StartsWith(startWith.ToUpper()));
                }



                return new JsonResult
                {
                    Data = products.Take(20),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}
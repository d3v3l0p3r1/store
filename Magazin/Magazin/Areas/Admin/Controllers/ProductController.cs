using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Data.Entities;
using Data.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Magazin.Controllers;
using Magazin.Helpers;
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

        public IHttpActionResult GetProducts([DataSourceRequest] DataSourceRequest request, int? categoryId, string filter)
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

                var result = new JsonNetResult(products.ToDataSourceResult(request));                    
                
                return Ok(result);
            }

        }       

        public IHttpActionResult Get(int? id, int categoryId)
        {
            using (var uow = CreateUnitOfWork())
            {
                var product = id.HasValue ? _productService.Find(uow, id.Value) : new Product { ProductCategoryId = categoryId };
                return Ok(product);
            }
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult UpdateProduct(Product product)
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
                    return Ok(result);
                }
            }
            catch (Exception error)
            {                
                return  BadRequest(error.Message);
            }
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult DeleteProduct(int id)
        {
            using (var uow = CreateUnitOfWork())
            {
                try
                {
                    _productService.Delete(uow, id);
                    return Ok(new { result = "ok" });
                }
                catch (ArgumentException)
                {
                    return BadRequest("Id is 0");
                }
            }
        }

        public IHttpActionResult FindProduct(string startWith)
        {
            using (var uow = CreateUnitOfWork())
            {
                var products = _productService.GetAll(uow);
                if (!string.IsNullOrEmpty(startWith))
                {
                    products = products.Where(x => x.Title.ToUpper().StartsWith(startWith.ToUpper()));
                }

                
                return Ok( new
                {
                    Data = products.Take(20),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                });
            }
        }
    }
}
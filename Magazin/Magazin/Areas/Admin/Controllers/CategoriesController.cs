using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Data.Entities;
using Data.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Magazin.Controllers;
using Magazin.Helpers;
using Magazin.Models;

namespace Magazin.Areas.Admin.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly IProductCategoryService _categoryService;

        public CategoriesController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        public IHttpActionResult GetCategories([DataSourceRequest] DataSourceRequest request)
        {
            using (var uow = CreateUnitOfWork())
            {
                var cats = _categoryService.GetAll(uow).ToDataSourceResult(request);

                return Ok(cats);
            }
        }    

        public IHttpActionResult Get(int? id)
        {
            using (var uow = CreateUnitOfWork())
            {
                var cat = id.HasValue ? _categoryService.Find(uow, id.Value) : new ProductCategory();

                return Ok(cat);
            }
        }


        public IHttpActionResult Update(ProductCategory category)
        {
            try
            {
                if (category == null)
                {
                    throw new ArgumentNullException(nameof(category));
                }

                using (var uow = CreateUnitOfWork())
                {
                    _categoryService.Update(uow, category);
                    return Ok(new { result = "ok" });
                }

            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Delete(int id)
        {
            using (var uow = CreateUnitOfWork())
            {
                try
                {
                    _categoryService.Delete(uow, id);
                    return Ok(new { result = "ok" });
                }
                catch (ArgumentException)
                {
                    return BadRequest("Id is 0");
                }
            }
        }
    }
}
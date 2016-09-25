using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entities;
using Data.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Magazin.Controllers;
using Magazin.Helpers;

namespace Magazin.Areas.Admin.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly IProductCategoryService _categoryService;

        public CategoriesController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        public JsonNetResult GetCategories([DataSourceRequest] DataSourceRequest request, int? id)
        {
            using (var uow = CreateUnitOfWork())
            {
                var cats = _categoryService.GetAll(uow).ToDataSourceResult(request);

                return new JsonNetResult(cats);
            }
        }

        public ActionResult Edit(int? id)
        {
            using (var uow = CreateUnitOfWork())
            {
                var cat = id.HasValue ? _categoryService.Find(uow, id.Value) : new ProductCategory();

                return PartialView("CategoryDetailView", cat);
            }
        }

        
        public JsonResult Update(ProductCategory category)
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
                    return Json(new {result = "ok"});
                }
                
            }
            catch (Exception error)
            {
                return Json(new {error = error.Message});
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            using (var uow = CreateUnitOfWork())
            {
                try
                {
                    _categoryService.Delete(uow, id);
                    return Json(new { result = "ok" });
                }
                catch (ArgumentException)
                {
                    return Json(new { error = "Id is 0" });
                }
            }
        }
    }
}
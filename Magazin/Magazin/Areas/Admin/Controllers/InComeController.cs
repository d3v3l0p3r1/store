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
using Magazin.Models;

namespace Magazin.Areas.Admin.Controllers
{
    public class InComeController : BaseController
    {
        private readonly IInComeService _inComeService;

        public InComeController(IInComeService inComeService)
        {
            _inComeService = inComeService;
        }


        public ActionResult Index()
        {
            return View("Index", new DialogViewModel());
        }

        public JsonResult GetInComs([DataSourceRequest] DataSourceRequest request)
        {
            using (var uow = CreateUnitOfWork())
            {
                var incoms = _inComeService.GetAll(uow);

                var result = new JsonResult()
                {
                    Data = incoms.ToDataSourceResult(request),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return result;
            }
        }

        public ActionResult GetInCome(int? id)
        {
            using (var uow = CreateUnitOfWork())
            {
                InCome income = id.HasValue 
                    ?_inComeService.Find(uow, id.Value)
                    : new InCome();

                return PartialView("InComeDetailView", income);
            }
        }

        [HttpPost]
        public JsonResult AddInCome(InCome inCome)
        {
            using (var uow = CreateUnitOfWork())
            {
                var income = _inComeService.Update(uow, inCome);

                var result = new JsonResult()
                {
                    Data = income
                };

                return result;
            }
        }

        public JsonResult Delete(int id)
        {
            using (var uow = CreateUnitOfWork())
            {
                try
                {
                    _inComeService.Delete(uow, id);
                    return new JsonResult() { Data = new { result = "ok" } };
                }
                catch (Exception error)
                {
                    return new JsonResult() { Data = new { error = error.Message } };
                }
            }
        }

        public ActionResult GetIncomeEntity(int? id)
        {
            using (var uow = CreateUnitOfWork())
            {
                InComeEntity entity;

                if (id.HasValue)
                {
                    entity = uow.GetRepository<InComeEntity>().Find(id.Value);
                }

                else
                {
                    entity = new InComeEntity();
                }

                return PartialView("InComeEntityDetailView", entity);                
            }
        }

    }
}
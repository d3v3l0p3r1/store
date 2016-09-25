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

        public JsonNetResult GetInComs([DataSourceRequest] DataSourceRequest request)
        {
            using (var uow = CreateUnitOfWork())
            {
                var incoms = _inComeService.GetAll(uow);

                var result = new JsonNetResult(incoms.ToDataSourceResult(request));

                return result;
            }
        }

        public ActionResult GetInComeDetailView(int? id)
        {
            return PartialView("InComeDetailView", new DetailViewModel() { EntityId = id });
        }

        public JsonNetResult GetIncome(int? id)
        {
            using (var uow = CreateUnitOfWork())
            {
                var income = id.HasValue ? _inComeService.Find(uow, id.Value) : new InCome();

                return new JsonNetResult(income);
            }
        }

        [HttpPost]
        public JsonNetResult AddInCome(InCome inCome)
        {
            using (var uow = CreateUnitOfWork())
            {
                var income = _inComeService.Update(uow, inCome);

                var result = new JsonNetResult(income);

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


        public ActionResult GetIncomeEntityDetailView(int? id)
        {
            return PartialView("InComeEntityDetailView", new DetailViewModel() { EntityId = id });
        }

        public JsonNetResult GetIncomeEntity(int? id)
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

                return new JsonNetResult(entity);
            }
        }

        public JsonNetResult ProcessIncome(int id)
        {
            using (var uow = CreateUnitOfWork())
            {
                try
                {
                    _inComeService.ProcessIncome(uow, id);

                    return new JsonNetResult(new { result = "ok" });
                }
                catch (Exception error)
                {
                    return new JsonNetResult(error);
                }
            }
        }

    }
}
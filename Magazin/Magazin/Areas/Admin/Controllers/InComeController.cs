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
    public class InComeController : BaseController
    {
        private readonly IInComeService _inComeService;

        public InComeController(IInComeService inComeService)
        {
            _inComeService = inComeService;
        }

        public IHttpActionResult GetInComs([DataSourceRequest] DataSourceRequest request)
        {
            using (var uow = CreateUnitOfWork())
            {
                var incoms = _inComeService.GetAll(uow);

                var result = new JsonNetResult(incoms.ToDataSourceResult(request));

                return Ok(result);
            }
        }


        public IHttpActionResult GetIncome(int? id)
        {
            using (var uow = CreateUnitOfWork())
            {
                var income = id.HasValue ? _inComeService.Find(uow, id.Value) : new InCome();

                return Ok(income);
            }
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult AddInCome(InCome inCome)
        {
            using (var uow = CreateUnitOfWork())
            {
                var income = _inComeService.Update(uow, inCome);

                var result = new JsonNetResult(income);

                return Ok(result);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            using (var uow = CreateUnitOfWork())
            {
                try
                {
                    _inComeService.Delete(uow, id);
                    return Ok(new { result = "ok" });
                }
                catch (Exception error)
                {
                    return BadRequest(error.Message);
                }
            }
        }   

        public IHttpActionResult GetIncomeEntity(int? id)
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

                return Ok(entity);
            }
        }

        public IHttpActionResult ProcessIncome(int id)
        {
            using (var uow = CreateUnitOfWork())
            {
                try
                {
                    _inComeService.ProcessIncome(uow, id);

                    return Ok(new { result = "ok" });
                }
                catch (Exception error)
                {
                    return BadRequest(error.Message);
                }
            }
        }

    }
}
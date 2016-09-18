using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Base.Services;
using Data.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Magazin.Controllers;
using Magazin.Models;

namespace Magazin.Areas.Admin.Controllers
{
    public class BalanceController : BaseController
    {
        private readonly IBaseService<BalanceOfProduct> _balanceService;
        public BalanceController(IBaseService<BalanceOfProduct> balanceService)
        {
            _balanceService = balanceService;
        }

        public ActionResult Index()
        {
            return View("Index", new DialogViewModel());
        }

        public JsonResult GetBalances([DataSourceRequest] DataSourceRequest request)
        {
            using (var uow = CreateUnitOfWork())
            {
                var balances = _balanceService.GetAll(uow);

                var result = new JsonResult()
                {
                    Data = balances.ToDataSourceResult(request),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return result;
            }
        }
    }
}
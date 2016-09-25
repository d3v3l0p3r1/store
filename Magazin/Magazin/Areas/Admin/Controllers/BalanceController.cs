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
using Magazin.Helpers;
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

        public JsonNetResult GetBalances([DataSourceRequest] DataSourceRequest request)
        {
            using (var uow = CreateUnitOfWork())
            {
                var balances = _balanceService.GetAll(uow);

                var result = new JsonNetResult(balances.ToDataSourceResult(request));

                return result;
            }
        }

        public ActionResult GetBalanceDetailView(int? id)
        {
            return PartialView("BalanceDetailView", new DetailViewModel() {EntityId = id});
        }

        public JsonNetResult Get(int id)
        {
            using (var uow = CreateUnitOfWork())
            {
                var balance = _balanceService.Find(uow, id);
                return new JsonNetResult(balance);
            }            
        }

        [HttpPost]
        public JsonNetResult Update(BalanceOfProduct balance)
        {
            using (var uow = CreateUnitOfWork())
            {
                _balanceService.Update(uow, balance);

                return new JsonNetResult(balance);
            }
        }
    }
}
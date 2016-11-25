using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entities;
using Data.Services;
using Magazin.Helpers;

namespace Magazin.Controllers
{
    public class BascetController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IBascetService _bascetService;
        private readonly IOrderService _orderService;

        public BascetController(IProductService productService, IBascetService bascetService, IOrderService orderService)
        {
            _productService = productService;
            _bascetService = bascetService;
            _orderService = orderService;
        }


        [HttpPost]
        public JsonNetResult AddToBascet(int productId, int count = 1)
        {
            using (var uow = CreateUnitOfWork())
            {
                var product = _productService.GetAll(uow).FirstOrDefault(x => x.Id == productId);
                if (product != null)
                {
                    _bascetService.AddItem(GetBascet(), product, count);
                    return new JsonNetResult(new { ok = "ok" });
                }
                else
                {
                    return new JsonNetResult(new { error = "Продукт не найден" });
                }
            }
        }

        [HttpPost]
        public JsonNetResult RemoveFromBascet(int productId)
        {
            _bascetService.RemoveItem(GetBascet(), productId);
            return new JsonNetResult(new { ok = "ok" });
        }

        public JsonNetResult GetBascetCount()
        {
            var bascet = GetBascet();

            return new JsonNetResult(bascet.Products.Count());
        }



        private Bascet GetBascet()
        {
            var bascet = (Bascet)Session["Bascet"];

            if (bascet == null)
            {
                bascet = new Bascet();
                Session["Bascet"] = bascet;
            }

            return bascet;
        }

        public ActionResult Index()
        {
            return View(GetBascet());
        }

        [HttpPost]
        public JsonNetResult CheckOut(Bascet bascet, string phone, string address)
        {
            using (var uow = CreateUnitOfWork())
            {
                try
                {
                    var order = _orderService.CreateOrder(uow, bascet, phone, address);
                    return new JsonNetResult(order);
                }
                catch (Exception error)
                {
                    return new JsonNetResult(new { error = error.Message });
                }

            }
        }
    }
}
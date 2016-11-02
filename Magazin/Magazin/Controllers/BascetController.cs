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

        public BascetController(IProductService productService, IBascetService bascetService)
        {
            _productService = productService;
            _bascetService = bascetService;
        }

        public JsonNetResult AddToBascet(int productId, int count)
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

        public JsonNetResult RemoveFromBascet(int productId)
        {
            _bascetService.RemoveItem(GetBascet(), productId);
            return new JsonNetResult(new { ok = "ok" });
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
    }
}
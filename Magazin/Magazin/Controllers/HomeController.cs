using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Services;

namespace Magazin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }


        // GET: Home
        public ActionResult Index()
        {
            var produts = _productService.GetAll();
            return View("Index", produts);
        }
    }
}
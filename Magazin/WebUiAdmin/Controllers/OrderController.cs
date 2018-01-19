using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll(int take, int skip)
        {
            var all = _orderService.GetAll();

            var orders = all.OrderByDescending(x => x.Id).Skip(skip).Take(take);
            var total = all.Count();

            return new JsonResult(new
            {
                Data = orders,
                Total = total
            });
        }


    }
}
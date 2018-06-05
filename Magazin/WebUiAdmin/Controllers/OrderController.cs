using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using WebUiAdmin.Models.Api.Order;

namespace WebUiAdmin.Controllers
{
    public class OrderController : BaseController<Order>
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) : base(orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll(int take, int skip, OrderState? state = null)
        {
            var all = _orderService.GetAll();

            if (state != null)
            {
                all = all.Where(x => x.State == state.Value);
            }
            var orders = all.OrderByDescending(x => x.Id).Skip(skip).Take(take);
            
            var total = all.Count();

            return new JsonResult(new
            {
                Data = orders,
                Total = total
            });
        }

        public override IActionResult Edit(int id)
        {
            var order = _orderService.Find(id);

            return View(order);
        }

        public IActionResult Update([FromBody]Order order)
        {
            if (ModelState.IsValid)
            {
                _orderService.Update(order);
                return Ok();
            }

            return BadRequest("Model invalid");
        }

        public IActionResult OrderItemEdit(string parent)
        {
            return View("OrderItemEdit", parent);
        }


        public IActionResult GetOrderProducts(int orderID)
        {
            var products = _orderService.GetOrderProducts(orderID);

            return Ok(products);
        }

        [HttpPost]
        public IActionResult ToDelivery(int id)
        {
            var order = _orderService.Find(id);
            if (order != null)
            {
                order.State = OrderState.InProgress;
                _orderService.Update(order);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult ToComplete(int id)
        {
            var order = _orderService.Find(id);
            if (order != null)
            {
                order.State = OrderState.Complete;
                _orderService.Update(order);
                return Ok();
            }

            return BadRequest();
        }



    }
}
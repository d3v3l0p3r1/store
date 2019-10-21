using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> GetAll(int take = 20, int skip = 0, OrderState? state = null)
        {
            var all = _orderService.GetAllAsNotracking();

            if (state != null)
            {
                all = all.Where(x => x.State == state.Value);
            }

            var total = await all.CountAsync();
            var orders = await all.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync();
            
            return new JsonResult(new
            {
                Data = orders,
                Total = total
            });
        }

        public override async Task<IActionResult> Edit(int id)
        {
            var order = await _orderService.FindAsync(id);

            return View(order);
        }

        public async Task<IActionResult> Update([FromBody]Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.UpdateAsync(order);
                return Ok();
            }

            return BadRequest("Model invalid");
        }

        public IActionResult OrderItemEdit(string parent)
        {
            return View("OrderItemEdit", parent);
        }


        public async Task<IActionResult> GetOrderProducts(int orderID)
        {
            var products = await _orderService.GetOrderProducts(orderID);

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> ToDelivery(int id)
        {
            var order = await _orderService.FindAsync(id);
            if (order != null)
            {
                order.State = OrderState.InProgress;
                await _orderService.UpdateAsync(order);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> ToComplete(int id)
        {
            var order = await _orderService.FindAsync(id);
            if (order != null)
            {
                order.State = OrderState.Complete;
                await _orderService.UpdateAsync(order);
                return Ok();
            }

            return BadRequest();
        }



    }
}
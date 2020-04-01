using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Documents.Implementations.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers.Admin
{
    /// <summary>
    /// Заказы
    /// </summary>
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("[action]")]
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


        /// <summary>
        /// Получить список продуктов по заказу
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetOrderProducts(long orderID)
        {
            var products = await _orderService.GetOrderProducts(orderID);

            return Ok(products);
        }

        /// <summary>
        /// Отпраить заказ на доставку
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ToDelivery(long id)
        {
            var order = await _orderService.GetAsync(id);
            if (order != null)
            {
                order.State = OrderState.InProgress;
                await _orderService.UpdateAsync(order);
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Закрыть заказ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ToComplete(long id)
        {
            var order = await _orderService.GetAsync(id);
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
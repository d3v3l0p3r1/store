using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUiAdmin.Models.Api.Order;

namespace WebUiAdmin.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Order")]
    public class ApiOrderController : Controller
    {
        private readonly IOrderService _orderService;

        public ApiOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Order([FromBody]OrderModel model)
        {
            try
            {
                var t = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int userID = 13244;

                var orderProducts = model.Products.Select(x => new OrderProduct
                {
                    ProductID = x.ProductID,
                    Count = x.Count,
                }).ToList();

                var res = await _orderService.CreateAsync(userID, orderProducts);

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }


    }
}
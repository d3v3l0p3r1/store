using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Models;
using BaseCore.Documents.Implementations.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Api
{
    /// <summary>
    /// Заказы
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "public")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Order([FromBody]OrderModel model)
        {
            try
            {
                int? userID = null;
                string nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!String.IsNullOrWhiteSpace(nameIdentifier))
                {
                    int.TryParse(nameIdentifier, out int id);
                    if (id != 0)
                    {
                        userID = id;
                    }
                }
                
                var res = await _orderService.CreateAsync(userID, model);

                return Ok(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }


    }
}
﻿using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Models;
using BaseCore.Products.Abstractions.Models;
using BaseCore.Products.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers.Api
{
    /// <summary>
    /// Остатки
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "public")]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService _balanceService;

        /// <summary>
        /// ctor
        /// </summary>
        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        /// <summary>
        /// Balance
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <param name="cat"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ListRespone<BalancedProductModel>), 200)]
        public async Task<IActionResult> GetProductBalance(int page = 1, int take = 10, long? cat = null)
        {
            if (page < 1)
            {
                page = 1;
            }
            if (take < 1)
            {
                take = 10;
            }
            var skip = (page - 1) * take;

            var all = _balanceService.GetWithBalance(cat);
            var total = await all.CountAsync();

            all = all.Skip(skip).Take(take);

            var result = new ListRespone<BalancedProductModel>()
            {
                Total = total,
                Data = await all.ToListAsync()
            };

            return Ok(result);
        }
    }
}
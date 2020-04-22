﻿using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.DAL.Implementations.Models;
using BaseCore.Products.Abstractions.Models;
using BaseCore.Products.Abstractions.Services;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers.Admin
{
    /// <summary>
    /// Balance controller
    /// </summary>
    [ApiController]
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "admin")]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService _balanceService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="balanceService"></param>
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
        [Route("[action]")]
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

        /// <summary>
        /// Получить движения
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ListRespone<Balance>), 200)]
        public async Task<IActionResult> GetAll(int page = 1, int take = 10, long? cat = null)
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

            var all = _balanceService.GetAllAsNoTracking();

            if (cat != null)
            {
                //all = all.Where(x => x.Product.CategoryId == cat.Value);
            }

            var total = await all.CountAsync();

            all = all.Skip(skip).Take(take);

            var result = new ListRespone<BalanceDto>()
            {
                Total = total,
                Data = await all.ToListAsync()
            };

            return Ok(result);
        }
    }
}
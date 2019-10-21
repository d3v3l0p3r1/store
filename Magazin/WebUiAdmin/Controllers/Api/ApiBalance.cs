using DataCore.Models;
using DataCore.Services.Abstract.Documents;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUiAdmin.Controllers.Api
{    
    [Route("api/[controller]")]
    public class BalanceController : Controller
    {
        private readonly IBalanceService _balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [Route("[action]")]
        [ProducesResponseType(typeof(List<BalancedProductModel>), 200)]
        public async Task<IActionResult> GetProductBalance(long cat = 1)
        {
            var all = await _balanceService.GetProductBalance(cat, Request.Scheme, Request.Host.ToString());

            return Ok(all);
        }
    }
}

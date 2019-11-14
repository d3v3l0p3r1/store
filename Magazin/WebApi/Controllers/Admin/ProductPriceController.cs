using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebUiAdmin.Controllers;

namespace WebApi.Controllers.Admin
{
    /// <summary>
    /// 
    /// </summary>    
    public class ProductPriceController : BaseController<ProductPrice>
    {
        private readonly IProductPriceService _service;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="service"></param>
        public ProductPriceController(IProductPriceService service) : base(service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ListRespone<ProductPrice>), 200)]
        public async Task<IActionResult> GetAll(int take = 10, int page = 1)
        {
            if (take < 1)
            {
                take = 10;
            }

            if (page < 1)
            {
                page = 1;
            }

            var skip = (page - 1) * take;

            var all = _service.GetAllAsNotracking();

            var total = await all.CountAsync();
            var cats = await all.Skip(skip).Take(take).ToListAsync();

            var result = new ListRespone<ProductPrice>()
            {
                Data = cats,
                Total = total
            };

            return Ok(result);
        }
    }
}

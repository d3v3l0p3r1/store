using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Admin.ProductPrice;
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
        [ProducesResponseType(typeof(ListRespone<ProductPriceModel>), 200)]
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

            var all = _service.GetQuery().Include(x => x.Product);

            var total = await all.CountAsync();

            var products = await all.OrderBy(x => x.ProductId).Select(x => x.ProductId).Distinct().Skip(skip).Take(take).ToListAsync();

            var priceList = await all.Where(x => products.Contains(x.ProductId)).ToListAsync();
            
            var list = priceList.GroupBy(x => x.ProductId)
                .Select(x => new ProductPriceModel
                {
                    ProductId = x.Key,
                    Prices = x.Select(z => new ProductPriceModelItem
                    {
                        Id = z.Id,
                        Date = z.Date,
                        Price = z.Price
                    })
                }).ToList();

            var result = new ListRespone<ProductPriceModel>()
            {
                Data = list,
                Total = total
            };

            return Ok(result);
        }
    }
}

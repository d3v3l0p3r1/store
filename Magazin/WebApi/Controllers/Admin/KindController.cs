using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Вид продукта
    /// </summary>
    public class KindController : BaseController<ProductKind>
    {
        private readonly IKindService _kindService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="kindService"></param>
        public KindController(IKindService kindService) : base(kindService)
        {
            _kindService = kindService;
        }

        /// <summary>
        /// Полусить список
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ListRespone<ProductKind>), 200)]
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

            var all = _kindService.GetAllAsNotracking();

            var total = await all.CountAsync();
            var cats = await all.Skip(skip).Take(take).ToListAsync();

            var res = new ListRespone<ProductKind>()
            {
                Total = total,
                Data = cats
            };

            return Ok(res);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApi.Models;
using WebApi.Models.Admin.Carousel;
using WebUiAdmin.Controllers;

namespace WebApi.Controllers.Admin
{
    [Route("[controller]")]
    [ApiController]
    public class CarouselController : BaseController<Carousel>
    {
        private readonly ICarouselService _carouselService;

        /// <summary>
        /// ctor
        /// </summary>
        public CarouselController(ICarouselService carouselService) : base(carouselService)
        {
            _carouselService = carouselService;
        }

        /// <summary>
        /// Создать карусель
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public async Task<ActionResult<Carousel>> Create(Carousel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await base.Create(model);

            return Ok(model);
        }

        /// <summary>
        /// Полусить список
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ListRespone<Carousel>), 200)]
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

            var all = _carouselService.GetAllAsNotracking();

            var total = await all.CountAsync();
            var cats = await all.Skip(skip).Take(take).ToListAsync();

            var res = new ListRespone<Carousel>()
            {
                Total = total,
                Data = cats
            };

            return Ok(res);
        }
    }
}
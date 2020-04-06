using System;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.News.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers.Admin
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    public class CarouselController : ControllerBase
    {
        private readonly ICarouselService _carouselService;

        /// <summary>
        /// ctor
        /// </summary>
        public CarouselController(ICarouselService carouselService)
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

            await _carouselService.Create(model);

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

        /// <summary>
        /// Получить сущность
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult<Carousel>> Get(long id)
        {
            try
            {
                var entity = await _carouselService.GetAsync(id);
                return Ok(entity);
            }
            catch (Exception error)
            {
                return BadRequest(error.GetBaseException().Message);
            }
        }


        /// <summary>
        /// Обновить сущность
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPatch]
        public virtual async Task<ActionResult<Carousel>> Update(Carousel entity)
        {
            try
            {
                await _carouselService.UpdateAsync(entity);
                return Ok(entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public virtual async Task<IActionResult> Delete(int id)
        {
            await _carouselService.DeleteAsync(id);
            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers.Api
{
    /// <summary>
    /// Для главной страницы  
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly ICarouselService _carouselService;

        /// <summary>
        /// ctor
        /// </summary>
        public PublicController(ICarouselService carouselService)
        {
            _carouselService = carouselService;
        }


        /// <summary>
        /// Данные для карусели
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetCarousel()
        {
            //var carousel = await _carouselService.GetCarousel();

            return Ok();
        }


        
    }
}

using System.Threading.Tasks;
using BaseCore.News.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Api
{
    /// <summary>
    /// Для главной страницы  
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "public")]
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
            var carousel = await _carouselService.GetCarousel();

            return Ok(carousel);
        }



    }
}

using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.News.Services.Abstractions;
using BaseCore.Products.Abstractions.Services;
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
        private readonly IBalanceService _balanceService;
        private readonly IBrandService _brandService;

        /// <summary>
        /// ctor
        /// </summary>
        public PublicController(ICarouselService carouselService, 
            IBalanceService balanceService, 
            IBrandService brandService)
        {
            _carouselService = carouselService;
            _balanceService = balanceService;
            _brandService = brandService;
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

        /// <summary>
        /// Список новых продуктов
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        
        public async Task<IActionResult> GetLastProducts(int count)
        {
            var products = await _balanceService.GetLastProducts(count);

            return Ok(products);
        }

        /// <summary>
        /// Список всех брендов с картинкой
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _brandService.GetAllBrands();

            return Ok(brands);
        }
    }
}

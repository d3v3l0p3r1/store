using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneAssIntegration.Services.Abstractions;

namespace WebApi.Controllers.Exchange
{
    /// <summary>
    /// Импорт из файлов
    /// </summary>
    [Route("api/exchange/[controller]")]
    [ApiController]
    [Authorize(Roles = "exchange,admin")]
    [ApiExplorerSettings(GroupName = "exchange")]
    [DisableRequestSizeLimit]
    public class FileInputController : ControllerBase
    {
        private readonly IProductFetcher _productFetcher;

        /// <summary>
        /// ctor
        /// </summary>
        public FileInputController(IProductFetcher productFetcher)
        {
            _productFetcher = productFetcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            using (var ms = file.OpenReadStream())
            {
                var result = await _productFetcher.ImportFile(ms);

                return Ok(result);
            }
        }

        /// <summary>
        /// Обновить картинки
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdatePictures()
        {
            var result = await _productFetcher.UpdateProductPictures();
            return Ok(result);
        }

        /// <summary>
        /// Обновить цены
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdatePrices(IFormFile file)
        {
            using (var ms = file.OpenReadStream())
            {
                var result = await _productFetcher.UpdateProductPrices(ms);
                return Ok(result);
            }
            
        }
    }
}
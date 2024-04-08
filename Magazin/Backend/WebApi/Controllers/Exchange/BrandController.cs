using System.Threading.Tasks;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Admin.Exchange;

namespace WebApi.Controllers.Exchange
{
    /// <summary>
    /// Контроллер для обмена брендов
    /// </summary>
    [Route("api/exchange/[controller]")]
    [ApiController]
    [Authorize(Roles = "exchange")]
    [ApiExplorerSettings(GroupName = "exchange")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        /// <summary>
        /// ctor
        /// </summary>
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        /// <summary>
        /// Создать бренд
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateBrand(BrandRequestModel model)
        {
            await _brandService.CreateAsync(new Brand()
            {
                Title = model.Title,
                ExternalId = model.Id,
                Description = model.Description
            });

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateBrand(BrandRequestModel model)
        {
            await _brandService.UpdateAsync(new Brand()
            {
                Title = model.Title,
                ExternalId = model.Id,
                Description = model.Description
            });

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(Brand), 200)]
        public async Task<IActionResult> GetBrand(string id)
        {
            var brand = await _brandService.GetByExternalIdAsync(id);
            if (brand != null)
            {
                return Ok(brand);
                
            }
            else
            {
                return NotFound();
            }
        }
    }
}
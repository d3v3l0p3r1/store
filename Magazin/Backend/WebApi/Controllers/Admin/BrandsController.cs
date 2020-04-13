using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Products.Abstractions.Models;
using BaseCore.Products.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin
{
    /// <summary>
    /// Контролер брендов
    /// </summary>
    [ApiController]
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "admin")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        /// <summary>
        /// ctor
        /// </summary>
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        /// <summary>
        /// Список брендов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _brandService.GetAllBrands();
            return Ok(brands);
        }

        /// <summary>
        /// Обновить бренд
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Update(BrandDetailDto model)
        {
            await _brandService.UpdateAsync(model);
            return Ok();
        }

        public async Task<IActionResult> Get(long id)
        {
            var result  = await _brandService.Get(id);
            return Ok(result);
        }
    }
}

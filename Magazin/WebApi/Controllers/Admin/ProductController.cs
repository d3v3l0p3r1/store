using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using DataCore.DAL;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApi.Models;
using WebApi.Models.Admin;
using WebApi.Models.Api.Product;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Продукты
    /// </summary>
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "admin")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IKindService _kindService;
        private readonly IFileService _fileService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService, IKindService kindService, IFileService fileService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _kindService = kindService;
            _fileService = fileService;
        }

        /// <summary>
        /// Получить список продуктов
        /// </summary>
        /// <param name="take"></param>
        /// <param name="page"></param>
        /// <param name="catID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ListRespone<ProductDto>), 200)]
        public async Task<IActionResult> GetAll(int take = 20, int page = 1, int? catID = null)
        {
            if (take == 0)
            {
                take = 20;
            }
            if (page <= 0)
            {
                page = 1;
            }

            var skip = (page - 1) * take;

            var all = _productService.GetQuery().Include(x => x.Kind).AsNoTracking();

            if (catID != null)
            {
                all = all.Where(x => x.CategoryId == catID.Value);
            }

            var url = $"{Request.Scheme}://{Request.Host}/File/GetFile?id=";

            var products = await all.OrderByDescending(x => x.Id).Skip(skip).Take(take).Select(x => new ProductDto
            {
                Id = x.Id,
                Price = x.Price,
                Description = x.Description,
                Title = x.Title,
                Kind = x.Kind.Title,
                FileUrl = x.FileID != null ? url + x.FileID : url + 1,
            }).ToListAsync();

            var total = await all.CountAsync();

            var result = new ListRespone<ProductDto>()
            {
                Data = products,
                Total = total
            };

            return Ok(result);
        }

        /// <summary>
        /// Получить продукт для изменения
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Product), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var product = await _productService.GetAsync(id);

            return Ok(product);
        }

        /// <summary>
        /// Создание продукта, принмает multipart
        /// </summary>
        /// <param name="body">body</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromForm]ProductPostModel body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = JsonConvert.DeserializeObject<Product>(body.Model);

            await _productService.CreateAsync(product, body.MainImage, body.Images);

            return Ok();
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="body">body</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromForm]ProductPostModel body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = JsonConvert.DeserializeObject<Product>(body.Model);

            await _productService.UpdateAsync(product, body.MainImage, body.Images);

            return Ok();
        }
    }
}
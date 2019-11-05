using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.DAL;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Api.Product;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Продукты
    /// </summary>
    public class ProductController : BaseController<Product>
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IKindService _kindService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService, IKindService kindService) : base(productService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _kindService = kindService;
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
    }
}
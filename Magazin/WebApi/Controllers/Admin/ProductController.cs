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

        public ProductController(IProductService productService, IProductCategoryService productCategoryService, IKindService kindService) :base(productService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _kindService = kindService;
        }

        /// <summary>
        /// Получить список продуктов
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="catID"></param>
        /// <returns></returns>
        [Produces("application/json")]
        public async Task<IActionResult> GetAll(int take, int skip, int? catID = null)
        {
            if (take == 0)
            {
                take = 100;
            }

            var all = _productService.GetAllAsNotracking();

            if (catID != null)
            {
                all = all.Where(x => x.CategoryId == catID.Value);
            }

            var products = await all.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync();

            var total = await all.CountAsync();

            return new JsonResult(new
            {
                Data = products,
                Total = total
            });
        }
    }
}
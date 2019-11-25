using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers.Api
{
    /// <summary>
    /// Продукты
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll(int take = 20, int page = 1, int? catID = null)
        {
            var skip = (page - 1) * take;
            var all = _productService.GetAllAsNotracking().Skip(skip).Take(take); 

            if(catID != null)
            {
                all = all.Where(x => x.CategoryId == catID);
            }

            var url = $"{Request.Scheme}://{Request.Host}/File/GetFile/";

            var res = all.Select(x => new
            {
                x.Id,
                Price = -100,
                x.Description,
                x.Title,
                x.KindId,
                x.CategoryId,
                KindTitle = x.Kind.Title,
                File = x.FileID != null ? url + x.FileID : url + 1
            });

            return Ok(res);
        }


    }
}
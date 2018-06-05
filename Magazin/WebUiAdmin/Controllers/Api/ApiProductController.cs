using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ApiProductController : Controller
    {

        private readonly IProductService _productService;

        public ApiProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("GetAll")]
        public IActionResult GetAll(int cat = 1)
        {
            var all = _productService.GetAll().Where(x => x.CategoryID == cat);

            var url = $"{Request.Scheme}://{Request.Host}/File/GetFile/";

            var res = all.Select(x => new
            {
                x.Id,
                x.Price,
                x.Description,
                x.Title,
                x.KindID,
                x.CategoryID,
                KindTitle = x.Kind.Title,
                File = x.FileID != null ? url + x.FileID : url + 1
            });

            return Ok(res);
        }


    }
}
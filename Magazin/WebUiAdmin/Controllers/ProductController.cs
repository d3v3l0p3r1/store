using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.DAL;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers
{    
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(DataContext dataContext, IProductService productService) : base(dataContext)
        {
            _productService = productService;
        }

        
        [Route("api/product/all")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            using (var uow = CreateUnitOfWork())
            {                
                var all = _productService.GetAll(uow);
                var products = all.Skip(skip).Take(take);
                var total = all.Count();

                return new JsonResult(new
                {
                    Data = products,
                    Total = total
                });
            }
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
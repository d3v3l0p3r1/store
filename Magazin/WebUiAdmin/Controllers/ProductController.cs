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
        
        [Produces("application/json")]
        public IActionResult GetAll(int take, int skip, int? catID = null)
        {
            using (var uow = CreateUnitOfWork())
            {
                var all = _productService.GetAll(uow);

                if (catID != null)
                {
                    all = all.Where(x => x.CategoryID == catID.Value);
                }

                var products = all.Skip(skip).Take(take);
                var total = all.Count();

                return new JsonResult(new
                {
                    Data = products,
                    Total = total
                });
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            using (var uow = CreateUnitOfWork())
            {
                var product = await _productService.FindAsync(uow, id);

                ViewData["Product"] = product;

                return PartialView(product);
            }            
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
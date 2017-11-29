using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : BaseController
    {
        public ProductController(DataContext dataContext) : base(dataContext)
        {
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
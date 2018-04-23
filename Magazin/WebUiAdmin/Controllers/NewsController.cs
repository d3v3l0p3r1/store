using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUiAdmin.Controllers
{
    public class NewsController : BaseController<News>
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService) : base(newsService)
        {
            _newsService = newsService;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        [Produces("application/json")]
        public IActionResult GetAll(int take, int skip)
        {
            if (take == 0)
            {
                take = 100;
            }

            var all = _newsService.GetAll();            

            var products = all.OrderByDescending(x => x.Id).Skip(skip).Take(take);

            var total = all.Count();

            return new JsonResult(new
            {
                Data = products,
                Total = total
            });
        }

        public override IActionResult Create()
        {
            var news = new News
            {
                Date = DateTime.Now
            };

            return View("Edit", news);
        }
        
    }
}

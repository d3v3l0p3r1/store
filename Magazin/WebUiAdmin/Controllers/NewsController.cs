using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUiAdmin.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
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

        public IActionResult Edit(int id)
        {
            var n = _newsService.Find(id);
            
            return View(n);
        }

        [HttpPost]
        public IActionResult Edit(News product)
        {
            _newsService.Update(product);
            return View("Index");
        }

        public IActionResult Create()
        {
            var news = new News
            {
                Date = DateTime.Now
            };

            return View("Edit", news);
        }

        [HttpPost]
        public IActionResult Create(News product)
        {
            _newsService.Update(product);
            return View("Index");
        }


    }
}

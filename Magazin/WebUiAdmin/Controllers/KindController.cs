using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers
{

    public class KindController : BaseController<ProductKind>
    {
        private readonly IKindService _kindService;

        public KindController(IKindService kindService) : base(kindService)
        {
            _kindService = kindService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var all = _kindService.GetAll();

            var total = all.Count();
            var cats = all.Select(x => new
            {
                x.Id,
                x.Title,
            });

            return new JsonResult(new
            {
                Data = cats,
                Total = total
            });


        }

        //public IActionResult Edit(int id)
        //{
        //    var cat = _kindService.Find(id);
        //    return View(cat);
        //}

        //[HttpPost]
        //public IActionResult Edit(ProductKind kind)
        //{
        //    try
        //    {
        //        _kindService.Update(kind);
        //        return View("Index");
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        //public IActionResult Create()
        //{
        //    var kind = new ProductKind();

        //    return View("Edit", kind);
        //}

        //[HttpPost]
        //public IActionResult Create(ProductKind kind)
        //{
        //    _kindService.Update(kind);
        //    return View("Index");
        //}

    }
}
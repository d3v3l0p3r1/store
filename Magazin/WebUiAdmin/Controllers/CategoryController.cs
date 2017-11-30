using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.DAL;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(DataContext dataContext, ICategoryService categoryService) : base(dataContext)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("api/category/all")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            using (var uow = CreateUnitOfWork())
            {
                var all = _categoryService.GetAll(uow);

                var total = all.Count();
                var cats = all.Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Description
                });

                return new JsonResult(new
                {
                    Data = cats,
                    Total = total
                });

            }
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUiAdmin.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/News")]
    public class ApiNewsController : Controller
    {
        private readonly INewsService _newsService;

        public ApiNewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public IActionResult GetAll(int cat = 1)
        {
            var all = _newsService.GetAll().OrderBy(x => x.Date);

            var res = all.Select(x => new
            {
                x.Id,
                x.Description,
                x.Title,
                Image = $"{Request.Scheme}://{Request.Host}/File/GetFile/{x.ImageID}"
            });

            return Ok(res);
        }
    }
}

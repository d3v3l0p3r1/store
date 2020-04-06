using System.Linq;
using BaseCore.News.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Api
{
    /// <summary>
    /// Новости
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "public")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public IActionResult GetAll(int cat = 1)
        {
            var all = _newsService.GetAllAsNotracking().OrderBy(x => x.Date);

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

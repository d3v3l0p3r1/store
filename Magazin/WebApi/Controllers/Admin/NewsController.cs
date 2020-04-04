using System.Linq;
using System.Threading.Tasks;
using BaseCore.News.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Admin
{
    /// <summary>
    /// Новости
    /// </summary>
    [ApiController]
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "admin")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService) 
        {
            _newsService = newsService;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            if (take == 0)
            {
                take = 100;
            }

            var all = _newsService.GetAllAsNotracking();

            var products = await all.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync();

            var total = await all.CountAsync();

            return new JsonResult(new
            {
                Data = products,
                Total = total
            });
        }
    }
}

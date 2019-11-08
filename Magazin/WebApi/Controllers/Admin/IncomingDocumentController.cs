using System;
using System.Linq;
using System.Threading.Tasks;
using DataCore.Entities.Documents;
using DataCore.Services.Abstract.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Документы прихода
    /// </summary>
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "admin")]
    public class IncomingDocumentController : Controller
    {
        private readonly IIncomingDocumentService incomingDocumentService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="incomingDocumentService"></param>
        public IncomingDocumentController(IIncomingDocumentService incomingDocumentService)
        {
            this.incomingDocumentService = incomingDocumentService;
        }

        /// <summary>
        /// Получить документ прихода
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IncomingDocument), 200)]
        public async Task<IActionResult> Get(long id)
        {
            var entity = await incomingDocumentService.GetAsync(id);
            return Ok(entity);
        }

        /// <summary>
        /// Получить список
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(int take = 20, int skip = 0, DocumentStatus? state = null)
        {
            var all = incomingDocumentService.GetAllAsNotracking();

            if (state != null)
            {
                all = all.Where(x => x.DocumentStatus == state.Value);
            }

            var total = await all.CountAsync();
            var orders = await all.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync();

            return new JsonResult(new
            {
                Data = orders,
                Total = total
            });
        }

        /// <summary>
        /// Создать документ
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(IncomingDocument document)
        {
            try
            {
                await incomingDocumentService.CreateAsync(document);
                return Ok();
            }
            catch(Exception error)
            {
                return BadRequest(error.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Провести документ
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Apply([FromBody]IncomingDocument document)
        {
            try
            {
                await incomingDocumentService.Apply(document);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return BadRequest(e.GetBaseException().Message);
            }
        }
    }
}
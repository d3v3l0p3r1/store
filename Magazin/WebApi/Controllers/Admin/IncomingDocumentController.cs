using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using DataCore.Entities.Documents;
using DataCore.Services.Abstract.Documents;
using DataCore.Services.Concrete.Documents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Документы прихода
    /// </summary>
    public class IncomingDocumentController : BaseController<IncomingDocument>
    {
        private readonly IIncomingDocumentService incomingDocumentService;

        public IncomingDocumentController(IIncomingDocumentService incomingDocumentService) : base(incomingDocumentService)
        {
            this.incomingDocumentService = incomingDocumentService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult IncomingDocumentEntryEdit(string parent)
        {
            return View("IncomingDocumentEntryEdit", parent);
        }

        [HttpGet]
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
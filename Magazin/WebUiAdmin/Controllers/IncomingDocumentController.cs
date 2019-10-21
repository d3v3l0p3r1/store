using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Services.Abstract;
using DataCore.Entities.Documents;
using DataCore.Services.Abstract.Documents;
using DataCore.Services.Concrete.Documents;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers
{
    public class IncomingDocumentController : BaseController<IncomingDocument>
    {
        private readonly IIncomingDocumentService incomingDocumentService;

        public IncomingDocumentController(IIncomingDocumentService incomingDocumentService) :base(incomingDocumentService)
        {

        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IncomingDocumentEntryEdit(string parent)
        {
            return View("IncomingDocumentEntryEdit", parent);
        }        
    }
}
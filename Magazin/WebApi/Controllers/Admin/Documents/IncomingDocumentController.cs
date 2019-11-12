using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseCore.Security.Services.Concrete;
using DataCore.Entities.Documents;
using DataCore.Services.Abstract.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers.Admin.Documents;
using WebApi.Models;
using WebApi.Models.Admin.Documents;
using WebApi.Models.Admin.Documents.IncomingDocuments;

namespace WebUiAdmin.Controllers
{
    /// <summary>
    /// Документы прихода
    /// </summary>
    [Route("[controller]")]
    public class IncomingDocumentController : BaseDocumentController<IIncomingDocumentService, IncomingDocument, IncomingDocumentEntry, IncomingDocumentDetailDto, IncomingDocumentListDto>
    {
        private readonly IIncomingDocumentService incomingDocumentService;
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="incomingDocumentService"></param>
        /// <param name="mapper"></param>
        public IncomingDocumentController(IIncomingDocumentService incomingDocumentService, IMapper mapper, UserManager manager) : base(incomingDocumentService, mapper, manager)
        {
            this.incomingDocumentService = incomingDocumentService;
            _mapper = mapper;
        }


    }
}
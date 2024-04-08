using AutoMapper;
using BaseCore.DAL.Implementations.Entities.Documents;
using BaseCore.Documents.Implementations.Services.Abstractions;
using BaseCore.Security.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Admin.Documents.IncomingDocuments;

namespace WebApi.Controllers.Admin.Documents
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
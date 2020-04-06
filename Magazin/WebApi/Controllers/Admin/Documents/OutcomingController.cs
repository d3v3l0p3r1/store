using AutoMapper;
using BaseCore.DAL.Implementations.Entities.Documents;
using BaseCore.Documents.Abstractions.Services;
using BaseCore.Security.Services.Concrete;
using WebApi.Models.Admin.Documents.OutcomingDocuments;

namespace WebApi.Controllers.Admin.Documents
{
    /// <summary>
    /// Для документов расхода
    /// </summary>
    public class OutcomingDocumentController : BaseDocumentController<IOutcomingDocumentService, OutComingDocument, OutComingDocumentEntry, OutcomingDocumentDetailDto, OutcomingDocumentListDto>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="outcomingDocumentService"></param>
        /// <param name="mapper"></param>
        public OutcomingDocumentController(IOutcomingDocumentService outcomingDocumentService, IMapper mapper, UserManager manager) : base(outcomingDocumentService, mapper, manager)
        {
        }


    }
}

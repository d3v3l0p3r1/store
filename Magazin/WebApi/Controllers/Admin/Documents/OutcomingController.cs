using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseCore.Security.Services.Concrete;
using DataCore.Entities.Documents;
using DataCore.Services.Abstract.Documents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Admin.Documents;
using WebApi.Models.Admin.Documents.OutcomingDocuments;

namespace WebApi.Controllers.Admin
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

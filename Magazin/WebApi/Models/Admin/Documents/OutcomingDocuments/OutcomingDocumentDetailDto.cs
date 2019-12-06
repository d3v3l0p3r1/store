using BaseCore.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Admin.Documents.OutcomingDocuments
{
    /// <summary>
    /// Детальное представление документа расхода
    /// </summary>
    public class OutcomingDocumentDetailDto : BaseDocumentDto
    {
        /// <summary>
        /// Табличная часть
        /// </summary>
        public List<DocumentEntryDto> Entries { get; set; }
    }
}

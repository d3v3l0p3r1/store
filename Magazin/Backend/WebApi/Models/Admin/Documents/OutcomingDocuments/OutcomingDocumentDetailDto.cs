using System.Collections.Generic;

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

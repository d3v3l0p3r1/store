using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Admin.Documents.IncomingDocuments
{
    /// <summary>
    /// Incoming document DTO
    /// </summary>
    public class IncomingDocumentListDto : BaseDocumentDto
    {
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Total { get; set; }
    }
}

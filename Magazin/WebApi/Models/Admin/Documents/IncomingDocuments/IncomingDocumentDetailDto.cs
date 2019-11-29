using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Admin.Documents.IncomingDocuments
{
    /// <summary>
    /// Модель для входящих документов
    /// </summary>
    public class IncomingDocumentDetailDto : BaseDocumentDto
    {

        /// <summary>
        /// Табличная часть
        /// </summary>
        public List<DocumentEntryDto> Entries { get; set; }

        /// <summary>
        /// Идентификатор поставщика
        /// </summary>
        public long ShipperId { get; set; }

        /// <summary>
        /// Поставщик
        /// </summary>
        public Contractor Shipper { get; set; }
    }
}

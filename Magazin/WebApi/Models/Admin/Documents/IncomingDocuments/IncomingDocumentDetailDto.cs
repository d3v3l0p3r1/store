using System.Collections.Generic;
using BaseCore.DAL.Implementations.Entities;

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

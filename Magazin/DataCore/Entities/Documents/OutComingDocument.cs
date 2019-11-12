using BaseCore.Security.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities.Documents
{
    /// <summary>
    /// Документ расхода
    /// </summary>
    public class OutComingDocument : BaseDocument<OutComingDocumentEntry>
    {
        /// <summary>
        /// Идентификатор получателя
        /// </summary>
        public long RecipientId { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public User Recipient { get; set; }
    }

    public class OutComingDocumentEntry: BaseDocumentEntry
    {
        public OutComingDocument Document { get; set; }
    }
}

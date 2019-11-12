using BaseCore.Security.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities.Documents
{
    public class IncomingDocument : BaseDocument<IncomingDocumentEntry>
    {
        /// <summary>
        /// Отправитель
        /// </summary>
        public long? SenderId { get; set; }

        /// <summary>
        /// От кого полученно
        /// </summary>
        public User Sender { get; set; }
    }



    public class IncomingDocumentEntry: BaseDocumentEntry
    {
        public IncomingDocument Document { get; set; }
    }
}

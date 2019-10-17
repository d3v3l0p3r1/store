using BaseCore.Entities;
using DataCore.Entities.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities
{
    public class Balance: BaseEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public int Count { get; set; }

        public long? IncomingDocumentId { get; set; }
        public IncomingDocument IncomingDocument { get; set; }

        public long? OutcomingDocumentId { get; set; }
        public OutComingDocument GetOutComingDocument { get; set; }

        public DateTime? ZeroDate { get; set; }
    }
}

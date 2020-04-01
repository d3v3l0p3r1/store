using System;
using System.Collections.Generic;
using BaseCore.DAL.Abstractions;
using BaseCore.DAL.Implementations.Entities.Documents;

namespace BaseCore.DAL.Implementations.Entities
{
    public class Balance : BaseEntity
    {
        public long ProductId { get; set; }

        public Product Product { get; set; }

        public DateTime? ZeroDate { get; set; }

        public List<BalanceEntry> BalanceEntries { get; set; } = new List<BalanceEntry>();
    }

    public class BalanceEntry : BaseEntity
    {
        public long BalanceId { get; set; }
        public Balance Balance { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public long? IncomingDocumentId { get; set; }
        public IncomingDocument IncomingDocument { get; set; }

        public long? OutcomingDocumentId { get; set; }
        public OutComingDocument OutComingDocument { get; set; }
    }
}

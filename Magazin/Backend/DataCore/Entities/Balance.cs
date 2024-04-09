using BaseCore.DAL.Abstractions;
using System;
using System.Collections.Generic;

namespace BaseCore.DAL.Implementations.Entities
{
    public class Balance : BaseEntity<long>
    {
        public long ProductId { get; set; }

        public Product Product { get; set; }

        public DateTime? ZeroDate { get; set; }

        public List<BalanceEntry> BalanceEntries { get; set; } = new List<BalanceEntry>();
    }

    public class BalanceEntry : BaseEntity<long>
    {
        public long BalanceId { get; set; }
        public Balance Balance { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }
    }
}

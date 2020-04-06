using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Balance.Abstractions.DTO
{
    public class BalanceDto
    {
        public long ProductId { get; set; }
        public DateTime? ZeroDate { get; set; }
        public IEnumerable<BalanceEntryDto> BalanceEntries { get; set; }
    }

    public class BalanceEntryDto
    {
        public int Count { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Models
{
    public class BalancedProductModel
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public long KindId { get; set; }
        public string KingTitle { get; set; }
        public long CateogryId { get; set; }
        public int Count { get; set; }
        public string File { get; set; }
    }
}

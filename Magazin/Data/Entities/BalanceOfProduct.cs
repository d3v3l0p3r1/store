using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Entities;

namespace Data.Entities
{
    /// <summary>
    /// Остаток товара
    /// </summary>
    public class BalanceOfProduct : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Count { get; set; }
    }
}

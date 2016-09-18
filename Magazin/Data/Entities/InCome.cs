using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Entities;

namespace Data.Entities
{
    /// <summary>
    /// Приход
    /// </summary>
    public class InCome : BaseEntity
    {
        public DateTime Date { get; set; }
        public virtual ICollection<InComeEntity> Incoms { get; set; }
    }

    public class InComeEntity : BaseEntity
    {
        public int InComeId { get; set; }
        public virtual InCome InCome { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Count { get; set; }
    }
}

using System.Collections.Generic;
using Base.Entities;
using Base.Security.Entities;

namespace Data.Entities
{
    public class Bascet : BaseEntity
    {
        public int? UserID { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<BascetProduct> Products { get; set; } 
    }

    public class BascetProduct : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Count { get; set; }        
    }
}

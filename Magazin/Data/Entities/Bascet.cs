using System.Collections.Generic;
using Base.Entities;
using Base.Security.Entities;

namespace Data.Entities
{
    public class Bascet 
    {
        public int? UserID { get; set; }
        public virtual User User { get; set; }

        public ICollection<BascetProduct> Products { get; set; } = new List<BascetProduct>();
    }

    public class BascetProduct
    {
        public Product Product { get; set; }

        public int Count { get; set; }        
    }
}

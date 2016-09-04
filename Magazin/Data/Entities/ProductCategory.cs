using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Entities;

namespace Data.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string Title { get; set; }

        public int? ParentId { get; set; }
        public virtual ProductCategory Parent { get; set; }
    }


}

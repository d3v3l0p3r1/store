using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.Entities;

namespace DataCore.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int? FileID { get; set; }
        public virtual FileData File { get; set; }

        public decimal Price { get; set; }

        public int CategoryID { get; set; }
        public virtual ProductCategory Category { get; set; }
    }
}

using System.Collections.Generic;
using Base.Entities;

namespace Data.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }

        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        public int Price { get; set; }

        public virtual ICollection<ProductFile> ProductFiles { get; set; }
    }

    public class ProductFile : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int FileId { get; set; }
        public virtual FileData File { get; set; }
    }
}

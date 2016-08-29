using Base.Entities;

namespace Data.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }

        public int ProductCategoryID { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseCore.DAL.Implementations.Entities.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(x => x.Category);
            builder.HasOne(x => x.Kind);
            builder.HasOne(x => x.Brand);
        }
    }
}

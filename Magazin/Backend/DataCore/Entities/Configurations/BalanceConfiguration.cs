using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseCore.DAL.Implementations.Entities.Configurations
{
    public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.HasMany<BalanceEntry>(x => x.BalanceEntries)
                .WithOne(x => x.Balance);
        }
    }
}

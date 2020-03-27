
using System.Threading.Tasks;
using BaseCore.Entities;
using BaseCore.Security.Entities;
using DataCore.Entities;
using DataCore.Entities.Documents;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using News = DataCore.Entities.News;

namespace DataCore.DAL
{
    public class DataContext : IdentityDbContext<User, Role, long>, IPersistedGrantDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<FileData> Files { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<ProductKind> ProductKinds { get; set; }
        public DbSet<IncomingDocument> IncomingDocuments { get; set; }
        public DbSet<Balance> Balance { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Carousel> Carousels { get; set; }
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DataContext(DbContextOptions<DataContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().HasOne(x => x.Category);
            builder.Entity<Product>().HasOne(x => x.Kind);
            builder.Entity<Product>().HasOne(x => x.Brand);

            builder.Entity<Order>()
                .HasMany(x => x.Products).WithOne(x => x.Order);

            builder.Entity<OrderProduct>()
                .HasOne(x => x.Product);

            builder.Entity<IncomingDocument>()
                .HasMany(x => x.Entries).WithOne(x => x.Document);

            builder.Entity<OutComingDocument>()
                .HasMany(x => x.Entries).WithOne(x => x.Document);

            builder.Entity<Balance>()
                .HasMany(x => x.BalanceEntries).WithOne(x => x.Balance);

            builder.Entity<Contractor>()
                .HasOne(x => x.Image);

            if (_operationalStoreOptions != null)
            {
                builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
            }
        }

        int IPersistedGrantDbContext.SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        Task<int> IPersistedGrantDbContext.SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}

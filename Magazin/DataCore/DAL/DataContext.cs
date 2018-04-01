
using BaseCore.Entities;
using BaseCore.Security.Entities;
using DataCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News = DataCore.Entities.News;

namespace DataCore.DAL
{
    public class DataContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<FileData> Files { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<News> News { get; set; }

        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().HasOne(x => x.Category);
        }
    }
}

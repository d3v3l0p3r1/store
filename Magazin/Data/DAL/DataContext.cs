using System.Data.Entity;
using Base.Entities;
using Data.Entities;

namespace Data.DAL
{

    public class DataContext : DbContext
    {

        static DataContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, EFContextConfiguration>());
            Database.SetInitializer(new DbInitializer());
        }

        public DataContext() : base()
        {
            Configuration.LazyLoadingEnabled = true;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }

    public class DbInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            base.Seed(context);

            ProductCategory category = new ProductCategory() { Title = "Базовая" };
            context.ProductCategories.Add(category);
            context.SaveChanges();


            var product = new Product { Title = " Dwadwadwa", ProductCategory = category };

            context.Products.Add(product);
            context.SaveChanges();

            var admin = new User()
            {
                Email = "",
                Name = "Admin",
                IsAdmin = true
            };

            context.Users.Add(admin);
        }
    }
}

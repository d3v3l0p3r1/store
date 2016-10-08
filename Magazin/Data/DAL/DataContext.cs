using System;
using System.Collections.Generic;
using System.Data.Entity;
using Base.Entities;
using Base.Security.Entities;
using Data.Entities;
using Data.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data.DAL
{
    public class DataContext : IdentityDbContext<User, Role, int, Login, UserRole, Claim>
    {
        static DataContext()
        {

        }

        public DataContext() : base(nameof(DataContext))
        {
            if (Database.Exists())
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, EFContextConfiguration>());
            }
            else
            {
                Database.SetInitializer(new DbInitilializer());
            }


            Configuration.LazyLoadingEnabled = true;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Bascet> Bascets { get; set; }
        public DbSet<BascetProduct> BascetProducts { get; set; }
        public DbSet<BalanceOfProduct> BalanceOfProducts { get; set; }

        public System.Data.Entity.DbSet<Data.Entities.InComeEntity> InComeEntities { get; set; }

        public System.Data.Entity.DbSet<Data.Entities.InCome> InComes { get; set; }
    }

    internal class DbInitilializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            base.Seed(context);

#if DEBUG

            for (int i = 0; i < 25; i++)
            {
                var cat = new ProductCategory
                {
                    Title = $"Category {i}"
                };
                context.ProductCategories.Add(cat);
            }
            context.SaveChanges();

            var random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                var product = new Product
                {
                    ProductCategoryId = random.Next(1, 15),
                    Title = $"product {i}"
                };
                context.Products.Add(product);
            }

            context.SaveChanges();

            CreateUser(context);

#endif
        }

        private void CreateUser(DataContext context)
        {
            var um = new UserManager<User, int>(new UserRepository(context));
            var rm = new RoleManager<Role, int>(new RoleRepository(context));


            var adminRole = new Role()
            {
                Name = "Admin"
            };
            var userRole = new Role()
            {
                Name = "User"
            };

            rm.Create(adminRole);
            rm.Create(userRole);

            var admin = new User
            {
                UserName = "zhan_chip@mail.ru",
                Email = "zhan_chip@mail.ru",
                EmailConfirmed = true
            };            


            var res = um.Create(admin, "AB43c518");

            if (res.Succeeded)
            {
                um.AddToRole(admin.Id, "admin");
            }

            else
            {
                throw new InvalidOperationException();
            }


            
        }
    }
}

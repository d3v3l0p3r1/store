using System;
using System.Collections.Generic;
using System.Data.Entity;
using Base.Entities;
using Base.Security.Entities;
using Data.Entities;
using Data.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data.DAL
{

    public class DataContext : IdentityDbContext<User, Role, int, Login, UserRole, Claim>
    {
        static DataContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, EFContextConfiguration>());
            Database.SetInitializer(new DbInitializer());
        }

        public DataContext() : base(nameof(DataContext))
        {
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


            var adminRole = new Role() { Name = "Admin" };
            var userRole = new Role() { Name = "User" };
            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);
            context.SaveChanges();

            var admin = new User() { UserName = "Admin" };
            context.Users.Add(admin);
            context.SaveChanges();

            var t = new UserRole
            {
                RoleId = adminRole.Id,
                UserId = admin.Id
            };

            admin.Roles.Add(t);
            context.SaveChanges();            
        }
    }
}

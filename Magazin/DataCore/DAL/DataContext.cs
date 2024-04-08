﻿
using BaseCore.Entities;
using DataCore.Entities;
using DataCore.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using News = DataCore.Entities.News;

namespace DataCore.DAL
{
    public class DataContext : DbContext
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
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Brand> Brands { get; set; }

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

            builder.Entity<ProductPrice>()
                .HasOne(x => x.Product);

            builder.Entity<Contractor>()
                .HasOne(x => x.Image);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Security.Entities;
using BaseCore.Security.Services.Concrete;
using Bogus;
using DataCore.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Extensions;

namespace DataCore.DAL
{
    public static class DbMigrationsConfiguration
    {


        public static void Seed(DataContext dataContext)
        {
            if (!dataContext.ProductCategories.Any())
            {

                var makeup = WriteCategory("Макияж", 1, dataContext);
                makeup.WriteChildCategory("Для лица", 0, dataContext)
                .WriteChildCategory("Для глаз", 1, dataContext)
                .WriteChildCategory("Для ногтей", 2, dataContext)
                .WriteChildCategory("Для бровей", 3, dataContext)
                .WriteChildCategory("Для губ", 4, dataContext)
                .WriteChildCategory("Аксассуары", 5, dataContext);

                var safe = WriteCategory("Уход", 2, dataContext);
                safe.WriteChildWithCildCategory("Уход за лицом", 0, dataContext)
                    .WriteChildCategory("Очищение", 0, dataContext)
                    .WriteChildCategory("Спреи", 1, dataContext)
                    .WriteChildCategory("Маски", 2, dataContext);

                safe.WriteChildCategory("Уход за телом", 1, dataContext);

                safe.WriteChildCategory("Уход за руками", 2, dataContext)
                .WriteChildCategory("Уход за ногами", 3, dataContext);


                WriteCategory("Парфюмерия", 2, dataContext);
                WriteCategory("Акксессуары", 4, dataContext);
            }

            if (!dataContext.ProductKinds.Any())
            {
                WriteKind("ECO товары", dataContext);
            }

            if (!dataContext.Brands.Any())
            {
                WriteBrands(dataContext);
            }

            if (!dataContext.Products.Any())
            {
                WriteProducts(dataContext);
            }
        }

        private static void WriteBrands(DataContext dataContext)
        {
            var fake = new Faker(locale:"ru");

            for (var i = 0; i < 100; i++)
            {
                var brand = new Brand()
                {
                    Title = fake.Company.CompanyName(),
                    Description = fake.Lorem.Text()
                };

                dataContext.Brands.Add(brand);
            }

            dataContext.SaveChanges();
        }

        private static void WriteProducts(DataContext dataContext)
        {
            var fake = new Faker(locale: "ru");
            var random = new Random();

            for (var i = 0; i < 1000; i++)
            {
                var product = new Product()
                {
                    Title = fake.Commerce.ProductName(),
                    BrandId = random.Next(1, 100),
                    CategoryId = random.Next(1, 17),
                    Description = fake.Lorem.Text()
                };

                dataContext.Add(product);
            }

            dataContext.SaveChanges();
        }

        private static ProductCategory WriteCategory(string name, decimal sortOrder, DataContext context)
        {
            var category = new ProductCategory
            {
                Title = name,
                SortOrder = sortOrder,
            };

            context.ProductCategories.Add(category);
            context.SaveChanges();

            category.Mask = $";{category.Id};";
            context.ProductCategories.Update(category);
            context.SaveChanges();

            return category;
        }

        public static ProductCategory WriteChildCategory(this ProductCategory parentCategory, string name, decimal sortOrder, DataContext context)
        {
            var category = new ProductCategory()
            {
                Title = name,
                SortOrder = sortOrder,
                ParentId = parentCategory.Id
            };

            context.ProductCategories.Add(category);
            context.SaveChanges();

            category.Mask = $"{parentCategory.Mask}{category.Id};";
            context.ProductCategories.Update(category);
            context.SaveChanges();

            return parentCategory;
        }

        public static ProductCategory WriteChildWithCildCategory(this ProductCategory parentCategory, string name, decimal sortOrder, DataContext context)
        {
            var category = new ProductCategory()
            {
                Title = name,
                SortOrder = sortOrder,
                ParentId = parentCategory.Id
            };

            context.ProductCategories.Add(category);
            context.SaveChanges();

            category.Mask = $"{parentCategory.Mask}{category.Id};";
            context.ProductCategories.Update(category);
            context.SaveChanges();

            return category;
        }

        private static void WriteKind(string name, DataContext context)
        {
            var kind = new ProductKind
            {
                Title = name
            };

            context.ProductKinds.Add(kind);
            context.SaveChanges();
        }

        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager roleManager)
        {
            var adminEmail = "zhan_chip@mail.ru";
            var password = "123123";

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "admin",
                });
            }

            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "user",
                });
            }

            if (await roleManager.FindByNameAsync("operator") == null)
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "operator",
                });
            }

            if (await roleManager.FindByNameAsync("company") == null)
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "company",
                });
            }

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };


                var result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}

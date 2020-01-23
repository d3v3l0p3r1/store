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

                var makeup = WriteCategory("Макияж", 1,"makeup" , dataContext);
                makeup.WriteChildCategory("Для лица", 0,"forface" ,dataContext)
                .WriteChildCategory("Для глаз", 1,"foreyes", dataContext)
                .WriteChildCategory("Для ногтей", 2,"dlyanogtey", dataContext)
                .WriteChildCategory("Для бровей", 3,"dlyabrovey", dataContext)
                .WriteChildCategory("Для губ", 4,"dlyagub", dataContext)
                .WriteChildCategory("Аксассуары", 5,"aksesuarudlyalica", dataContext);

                var safe = WriteCategory("Уход", 2,"uhod", dataContext);
                safe.WriteChildWithCildCategory("Уход за лицом",0, "yxodzalicom",dataContext)
                    .WriteChildCategory("Очищение", 0,"clear", dataContext)
                    .WriteChildCategory("Спреи", 1,"sprey", dataContext)
                    .WriteChildCategory("Маски", 2,"maski", dataContext);

                safe.WriteChildCategory("Уход за телом", 1,"yxodzatelom", dataContext);

                safe.WriteChildCategory("Уход за руками", 2,"yxodzarukami", dataContext)
                .WriteChildCategory("Уход за ногами", 3,"yxodzanogami",dataContext);


                WriteCategory("Парфюмерия", 2, "parfum", dataContext);
                WriteCategory("Аксессуары", 4, "access",dataContext);
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
            var fake = new Faker(locale: "ru");

            for (var i = 0; i < 20; i++)
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

            var categories = dataContext.ProductCategories.ToList();

            foreach (var productCategory in categories)
            {
                for (var i = 0; i < 50; i++)
                {
                    var product = new Product()
                    {
                        Title = fake.Commerce.ProductName(),
                        BrandId = random.Next(1, 20),
                        CategoryId = productCategory.Id,
                        Description = fake.Lorem.Text(),
                        Price = random.Next(100, 10999)
                    };

                    dataContext.Add(product);
                }

                dataContext.SaveChanges();
            }
        }

        private static ProductCategory WriteCategory(string name, decimal sortOrder,string routeName, DataContext context)
        {
            var category = new ProductCategory
            {
                Title = name,
                SortOrder = sortOrder,
                RouteName = routeName
            };

            context.ProductCategories.Add(category);
            context.SaveChanges();

            category.Mask = $";{category.Id};";
            context.ProductCategories.Update(category);
            context.SaveChanges();

            return category;
        }

        public static ProductCategory WriteChildCategory(this ProductCategory parentCategory, string name, decimal sortOrder, string routeName, DataContext context)
        {
            var category = new ProductCategory()
            {
                Title = name,
                SortOrder = sortOrder,
                ParentId = parentCategory.Id,
                RouteName = routeName
            };

            context.ProductCategories.Add(category);
            context.SaveChanges();

            category.Mask = $"{parentCategory.Mask}{category.Id};";
            context.ProductCategories.Update(category);
            context.SaveChanges();

            return parentCategory;
        }

        public static ProductCategory WriteChildWithCildCategory(this ProductCategory parentCategory, string name, decimal sortOrder, string routeName, DataContext context)
        {
            var category = new ProductCategory()
            {
                Title = name,
                SortOrder = sortOrder,
                ParentId = parentCategory.Id,
                RouteName = routeName
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

using System;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.DAL.Implementations;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Security.Entities;
using BaseCore.Security.Services.Concrete;
using Bogus;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Migrations
{
    public static class DbMigrationsConfiguration
    {


        public static void Seed(DataContext dataContext)
        {
            if (!dataContext.Set<ProductCategory>().Any())
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

            if (!dataContext.Set<ProductKind>().Any())
            {
                WriteKind("ECO товары", dataContext);
            }

            if (!dataContext.Set<Brand>().Any())
            {
                WriteBrands(dataContext);
            }

            if (!dataContext.Set<Product>().Any())
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

                dataContext.Set<Brand>().Add(brand);
            }

            dataContext.SaveChanges();
        }

        private static void WriteProducts(DataContext dataContext)
        {
            var fake = new Faker(locale: "ru");
            var random = new Random();

            var categories = dataContext.Set<ProductCategory>().ToList();

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

            context.Set<ProductCategory>().Add(category);
            context.SaveChanges();

            category.Mask = $";{category.Id};";
            context.Set<ProductCategory>().Update(category);
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

            context.Set<ProductCategory>().Add(category);
            context.SaveChanges();

            category.Mask = $"{parentCategory.Mask}{category.Id};";
            context.Set<ProductCategory>().Update(category);
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

            context.Set<ProductCategory>().Add(category);
            context.SaveChanges();

            category.Mask = $"{parentCategory.Mask}{category.Id};";
            context.Set<ProductCategory>().Update(category);
            context.SaveChanges();

            return category;
        }

        private static void WriteKind(string name, DataContext context)
        {
            var kind = new ProductKind
            {
                Title = name
            };

            context.Set<ProductKind>().Add(kind);
            context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
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

            if (await roleManager.FindByNameAsync("exchange") == null)
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "exchange",
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


                var exchangeUser = new User
                {
                    UserName = "exchange",
                };

                result = await userManager.CreateAsync(exchangeUser, ".6g:KQ3rG");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(exchangeUser, "exchange");
                }
            }
        }
    }
}

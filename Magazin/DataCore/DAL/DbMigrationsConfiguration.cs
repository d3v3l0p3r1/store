using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Security.Entities;
using BaseCore.Security.Services.Concrete;
using DataCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace DataCore.DAL
{
    public class DbMigrationsConfiguration
    {


        public static void Seed(DataContext dataContext)
        {
            if (!dataContext.ProductCategories.Any())
            {
                
                WriteCategory("Роллы", 1, dataContext);
                WriteCategory("Суши", 2, dataContext);
                WriteCategory("Сеты", 2, dataContext);
                WriteCategory("Горячие блюда", 4, dataContext);
                WriteCategory("Салаты", 5, dataContext);
                WriteCategory("Десерты", 6, dataContext);
                WriteCategory("Напитки", 7, dataContext);
                WriteCategory("Соусы", 8,dataContext);
                WriteCategory("Блюда за бонусы", 9, dataContext);                
            }

            if (!dataContext.Products.Any())
            {
                var random = new Random();

                for (int i = 0; i < 100; i++)
                {
                    var product = new Product
                    {
                        CategoryID = random.Next(1, 9),
                        Title = Faker.Name.First(),
                        Description = Faker.Name.FullName(),
                        Price = random.Next(100, 1000)
                    };

                    dataContext.Products.Add(product);
                    dataContext.SaveChanges();
                }
            }

        }

        private static void WriteCategory(string name, decimal sortOrder, DataContext context)
        {
            var category = new ProductCategory
            {
                Title = name,
                SortOrder = sortOrder
            };

            context.ProductCategories.Add(category);
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
                    Name = "admin"
                });
            }

            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "user"
                });
            }

            if (await roleManager.FindByNameAsync("operator") == null)
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "operator"
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

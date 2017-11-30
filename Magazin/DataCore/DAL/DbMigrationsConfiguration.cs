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
                for (int i = 0; i < 15; i++)
                {
                    var cat = new ProductCategory
                    {
                        Title = Faker.Company.Name(),
                        Description = Faker.Company.BS()
                    };
                    
                    dataContext.ProductCategories.Add(cat);
                    dataContext.SaveChanges();
                }
            }

            if (!dataContext.Products.Any())
            {
                var random = new Random();

                for (int i = 0; i < 1000; i++)
                {
                    var product = new Product
                    {
                        CategoryID = random.Next(1, 15),
                        Title = Faker.Name.First(),
                        Description = Faker.Name.FullName(),
                        Price = random.Next(100, 1000)
                    };

                    dataContext.Products.Add(product);
                    dataContext.SaveChanges();
                }
            }

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

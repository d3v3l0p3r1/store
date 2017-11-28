using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Security.Entities;
using BaseCore.Security.Services.Concrete;
using Microsoft.AspNetCore.Identity;

namespace DataCore.DAL
{
    public class DbMigrationsConfiguration
    {


        public static void Seed(DataContext dataContext)
        {

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

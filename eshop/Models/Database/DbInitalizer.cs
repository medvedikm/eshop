﻿using eshop.Models.DatabaseFake;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public class DbInitializer
    {
        public static void Initialize(EshopDBContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Carousels.Count() == 0) 
            {
                IList<Carousel> carousels = CarouselHelper.GenerateCarousels();
                foreach (var item in carousels)
                    dbContext.Carousels.Add(item);

                dbContext.SaveChanges();
            }

            if (dbContext.Carousels.Count() == 0)
            {
                IList<Product> products = ProductHelper.GenerateProducts();
                foreach (var item in products)
                    dbContext.Products.Add(item);

                dbContext.SaveChanges();
            }
        }

        public async static void EnsureRoleCreated(IServiceProvider serviceProvider)
        {
            using (var services = serviceProvider.CreateScope())
            {
                RoleManager<Role> roleManager = services.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                string[] roles = Enum.GetNames(typeof(Roles));

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new Role(role));
                }
            }
        }

        public async static void EnsureAdminCreated(IServiceProvider serviceProvider)
        {
            using (var services = serviceProvider.CreateScope())
            {
                UserManager<User> userManager = services.ServiceProvider.GetRequiredService<UserManager<User>>();
                User admin = new User()
                {
                    UserName = "admin",
                    Email = "admin_medvedik@utb.cz",
                    Name = "Michal",
                    LastName = "Medvedík",
                    EmailConfirmed = true
                };

                var password = "abcd";

                User adminInDatabase = await userManager.FindByNameAsync(admin.UserName);

                if (adminInDatabase == null)
                {
                    IdentityResult iResult = await userManager.CreateAsync(admin, password);

                    if (iResult.Succeeded)
                    {
                        string[] roles = Enum.GetNames(typeof(Roles));

                        foreach (var role in roles)
                        {
                            await userManager.AddToRoleAsync(admin, role);
                        }
                    }
                    else if (iResult.Errors != null && iResult.Errors.Count() > 0)
                    {
                        foreach (var error in iResult.Errors)
                        {
                            Debug.WriteLine("Error during role Creation: " + error.Code + " -> " + error.Description);
                        }
                    }
                }

                User manager = new User()
                {
                    UserName = "manager",
                    Email = "manager_medvedik@utb.cz",
                    Name = "Michal",
                    LastName = "Medvedík",
                    EmailConfirmed = true
                };

                User managerInDatabase = await userManager.FindByNameAsync(manager.UserName);

                if (managerInDatabase == null)
                {
                    IdentityResult iResult = await userManager.CreateAsync(manager, password);

                    if (iResult.Succeeded)
                    {
                        string[] roles = Enum.GetNames(typeof(Roles));

                        foreach (var role in roles)
                        {
                            await userManager.AddToRoleAsync(manager, role);
                        }
                    }
                    else if (iResult.Errors != null && iResult.Errors.Count() > 0)
                    {
                        foreach (var error in iResult.Errors)
                        {
                            Debug.WriteLine("Error during role Creation: " + error.Code + " -> " + error.Description);
                        }
                    }
                }
            }
        }
    }
}
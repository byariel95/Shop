
namespace Shop.Web.Data
{
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Helpers;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context,IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            if (!this.context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Quetzaltenango" });
                cities.Add(new City { Name = "Guatemala" });
                cities.Add(new City { Name = "Solola" });
                cities.Add(new City { Name = "Mazatenango" });
                cities.Add(new City { Name = "Peten" });
                cities.Add(new City { Name = "Jalapa" });

                this.context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Guatemala"
                });

                await this.context.SaveChangesAsync();
            }



            var user = await this.userHelper.GetUserByEmailAsync("byron_1995_@hotmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Ariel",
                    LastName = "Monterroso",
                    Email = "byron_1995_@hotmail.com",
                    UserName = "byron_1995_@hotmail.com",
                    PhoneNumber ="54513968",
                    Address = "4ta Calle 24-56",
                    CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()

                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
                var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
                await this.userHelper.ConfirmEmailAsync(user, token);

            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }



            if (!this.context.Products.Any())
            {
                this.AddProduct("Iphone X",user);
                this.AddProduct("Mouse Magic",user);
                this.AddProduct("Iwatch Series 4",user);
                this.AddProduct("Xiaomi Mi 8",user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(100),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User = user
            });
        }
    }


}

using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantsSeeder(RestaurantsDbContext dbContext) : IRestaurantsSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {

                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();


                }

                if (!dbContext.Roles.Any())
                {

                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();

                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
            [
                new (UserRoles.User),
                new (UserRoles.Owner),
                new (UserRoles.Admin)
            ];

            return roles;

        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>
            {
            new Restaurant
            {
                
                Name = "Spice Garden",
                Description = "Authentic Indian cuisine with rich flavors.",
                Category = "Indian",
                HasDelivery = true,
                ContactEmail = "contact@spicegarden.com",
                ContactNumber = "555-1234",
                Address = new Address
                {
                    City = "Chicago",
                    Street = "123 Curry Lane",
                    PostalCode = 60616
                },
                Dishes = new List<Dish>
                {
                    new Dish { Name = "Butter Chicken", Description = "Creamy tomato-based chicken curry", price = 13.99m},
                    new Dish { Name = "Paneer Tikka", Description = "Grilled paneer cubes with spices", price = 10.99m},
                    new Dish { Name = "Garlic Naan", Description = "Soft bread with garlic butter", price = 2.99m}
                }
            },
            new Restaurant
            {
               
                Name = "Pasta House",
                Description = "Classic Italian dishes with fresh ingredients.",
                Category = "Italian",
                HasDelivery = false,
                ContactEmail = "info@pastahouse.com",
                ContactNumber = "555-5678",
                Address = new Address
                {
                    City = "New York",
                    Street = "456 Olive Blvd",
                    PostalCode = 10011
                },
                Dishes = new List<Dish>
                {
                    new Dish { Name = "Spaghetti Carbonara", Description = "Pasta with bacon, egg, and parmesan", price = 14.49m},
                    new Dish { Name = "Margherita Pizza", Description = "Classic pizza with tomato, mozzarella, and basil", price = 11.99m},
                    new Dish { Name = "Tiramisu", Description = "Coffee-flavored layered dessert", price = 6.49m}
                }
            }
            };
            return restaurants;
        }
    }
}

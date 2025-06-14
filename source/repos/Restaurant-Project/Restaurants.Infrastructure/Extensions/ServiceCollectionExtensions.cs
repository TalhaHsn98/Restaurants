using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Restaurants.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantsDb");
            services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantsDbContext>();

            services.AddScoped<IRestaurantsSeeder, RestaurantsSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantRequirementHandler>();

            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality, 
                builder => builder.RequireClaim(AppClaimTypes.Nationality, "German", "Polish"))
                .AddPolicy(PolicyNames.Atleast20,
                builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
                .AddPolicy(PolicyNames.CreatedAtleast2Restaurants,
                builder => builder.AddRequirements(new CreatedMultipleRestaurantRequirement(2)));

            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        }

    }
}

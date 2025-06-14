using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Application.Restaurants;
using FluentValidation;
using FluentValidation.AspNetCore;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));  
            services.AddAutoMapper(applicationAssembly);

            services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

            services.AddScoped<IUserContext, UserContext>();

            services.AddHttpContextAccessor();
        }
    }
}


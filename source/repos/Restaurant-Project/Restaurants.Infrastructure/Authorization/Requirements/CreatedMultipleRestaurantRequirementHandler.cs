using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    internal class CreatedMultipleRestaurantRequirementHandler(IUserContext userContext, IRestaurantsRepository restaurantsRepository) : AuthorizationHandler<CreatedMultipleRestaurantRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();
            var restaurants = await restaurantsRepository.GetAllAsync();

            var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == currentUser!.Id);

            if (userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }

            else
            {
                context.Fail();
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
        IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
        {

            var user = userContext.GetCurrentUser();
            logger.LogInformation("Authorizing user {UserEail}, to {Operation} for restaurant {RestaurantName}",
                user.Email,
                resourceOperation,
                restaurant.Name);


            if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/read operation - successful authorzied");

                return true;
            }

            if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Delete operation - successful authorzied");

                return true;
            }


            if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
                && user.Id == restaurant.OwnerId)
            {
                logger.LogInformation(" restaurant owner - successful authorzied");

                return true;
            }

            return false;
        }

    }
}

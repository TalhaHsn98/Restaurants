using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirement> logger,
        IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();


            logger.LogInformation("user: {Email}, date of birth {DoB} - handling MinimumAgeRequirement",
                currentUser.Email,
                currentUser.DateOfBirth);

            if(currentUser.DateOfBirth == null)
            {
                logger.LogWarning("Userdate of birth is nullll");
                context.Fail();
                return Task.CompletedTask;
            }

            if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                logger.LogInformation("Authorization Secceded");
                context.Succeed(requirement);
            }
            else {

                context.Fail();

            }

            return Task.CompletedTask;
        }
    }
}

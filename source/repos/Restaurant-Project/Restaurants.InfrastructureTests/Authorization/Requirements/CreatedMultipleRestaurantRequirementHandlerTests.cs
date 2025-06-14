using Xunit;
using Restaurants.Infrastructure.Authorization.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Application.Users;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using FluentAssertions;

namespace Restaurants.Infrastructure.Authorization.Requirements.Tests
{
    public class CreatedMultipleRestaurantRequirementHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsyncTest_ShouldPass()
        {
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            var userContextMock = new Mock<IUserContext>();


            userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);


            var restaurants = new List<Restaurant>()
            {
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = "2",
                },

            };

            var restaurantRespositoryMock = new Mock<IRestaurantsRepository>();
            restaurantRespositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

            var requirement = new CreatedMultipleRestaurantRequirement(2);

            var handler = new CreatedMultipleRestaurantRequirementHandler(userContextMock.Object, 
                restaurantRespositoryMock.Object);
            var context = new AuthorizationHandlerContext([requirement], null, null);

            //act

            await handler.HandleAsync(context);


            //assert

            context.HasSucceeded.Should().BeTrue(); 

        }
    }
}
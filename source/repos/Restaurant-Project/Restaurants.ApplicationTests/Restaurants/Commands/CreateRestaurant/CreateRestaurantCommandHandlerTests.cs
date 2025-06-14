using Xunit;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Application.Users;
using FluentAssertions;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ReturnCreatedREstaurantId()
        {
            var command = new CreateRestaurantCommand();
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
            var mapperMock = new Mock<IMapper>();

            var restaurant = new Restaurant();

           
            mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);

            var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();

            restaurantRepositoryMock
                .Setup(repo => repo.Create(It.IsAny<Restaurant>()))
                .ReturnsAsync(1);

            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("Owner-id", "test@Test.com", [], null, null);

            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object,mapperMock.Object, restaurantRepositoryMock.Object, userContextMock.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            //Assert

            result.Should().Be(1);
            restaurant.OwnerId.Should().Be("Owner-id");
            
        }
    }
}
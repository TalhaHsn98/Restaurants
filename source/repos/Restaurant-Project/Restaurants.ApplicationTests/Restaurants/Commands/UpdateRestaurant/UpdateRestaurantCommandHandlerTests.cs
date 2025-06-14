using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Xunit;


namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {

        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _logger;
        private readonly Mock<IRestaurantsRepository> _repository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IRestaurantAuthorizationService> _authorizationService;

        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests()
        {
            _logger = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
            _repository = new Mock<IRestaurantsRepository>();
            _mapper = new Mock<IMapper>();
            _authorizationService = new Mock<IRestaurantAuthorizationService>();

            _handler = new UpdateRestaurantCommandHandler(_logger.Object,
                _repository.Object,
                _mapper.Object,
                _authorizationService.Object);


        }




        [Fact()]
        public async Task HandleTest_WithValidRequest_ShouldUpdateRestaurant()
        {
            var restaurantId = 1;
            var command = new UpdateRestaurantCommand()
            {
                Id = restaurantId,
                Name = "new Test",
                Description = "some descript",
                HasDelivery = true

            };

            var restaurant = new Restaurant()
            {
                Id = restaurantId,
                Name = "Test",
                Description = "Testing"
            };


            _repository.Setup(r => r.GetByIdAsync(restaurantId)).ReturnsAsync(restaurant);

            _authorizationService.Setup(m => m.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
                .Returns(true);


            //Act

            await _handler.Handle(command, CancellationToken.None);

            //Assert 

            _repository.Verify(r => r.SaveChanges(), Times.Once());
            _mapper.Verify(m => m.Map(command, restaurant), Times.Once());
        }


    }
}
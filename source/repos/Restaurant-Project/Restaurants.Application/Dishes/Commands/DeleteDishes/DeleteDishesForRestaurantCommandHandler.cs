using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository,
        IMapper mapper, IDishesRepository dishesRepository, IRestaurantAuthorizationService restaurantAuthorizationService
        ) : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("Removing all the dishes from Restaurant: {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(request.RestaurantId.ToString());


            if (!restaurantAuthorizationService.Authorize(restaurant, Domain.Constants.ResourceOperation.Delete))
                throw new ForbidException();

            await dishesRepository.Delete(restaurant.Dishes);


        }
    }
}

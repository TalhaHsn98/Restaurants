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

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleteing the restaurant");
            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
            if (restaurant is null) 
                 throw new NotFoundException($"Restaurant does not exist");

            if (!restaurantAuthorizationService.Authorize(restaurant, Domain.Constants.ResourceOperation.Delete))
                throw new ForbidException();
            
            await restaurantsRepository.Delete(restaurant);
            
        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository, IMapper mapper, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating the restaurant");
            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
            if (restaurant is null)
                 throw new NotFoundException($"Restaurant does not exist");

            if (!restaurantAuthorizationService.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
                throw new ForbidException();

            mapper.Map(request, restaurant);
            //restaurant.Name = request.Name;
            //restaurant.Description = request.Description;
            //restaurant.HasDelivery = request.HasDelivery;

            await restaurantsRepository.SaveChanges();

            
        }
    }
}

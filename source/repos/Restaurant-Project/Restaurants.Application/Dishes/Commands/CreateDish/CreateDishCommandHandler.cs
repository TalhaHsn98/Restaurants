using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
        IRestaurantsRepository restaurantRepository, IDishesRepository dishesRepository,
        IMapper mapper, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken) {

            logger.LogInformation("Creating new dish: {@DishRequest}", request);
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(request.RestaurantId.ToString());


            if (!restaurantAuthorizationService.Authorize(restaurant, Domain.Constants.ResourceOperation.Create))
                throw new ForbidException();

            var dish = mapper.Map<Dish>(request);

            return await dishesRepository.Create(dish);             
                
        }


    }
}

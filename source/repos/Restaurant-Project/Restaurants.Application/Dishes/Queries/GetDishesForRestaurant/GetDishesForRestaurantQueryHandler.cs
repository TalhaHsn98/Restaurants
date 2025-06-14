using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant
{
    public class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger, 
        IRestaurantsRepository restaurantRepository, IMapper mapper) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
    {


        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dishes for the restaurant with id: {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

            if(restaurant == null) throw new NotFoundException(request.RestaurantId.ToString());

            var results = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

            return results;

        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDisheByIdForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler(IRestaurantsRepository restaurantRepository, IMapper mapper,
        ILogger<GetDishByIdForRestaurantQueryHandler> logger) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dish: {DishId} for the restaurant with id: {RestaurantId}",request.DishId, request.RestaurantId);

            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

            if (restaurant == null) throw new NotFoundException(request.RestaurantId.ToString());

            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
            if (dish == null) throw new NotFoundException(request.DishId.ToString());

            var results = mapper.Map<DishDto>(dish);

            return results;
        }
    }
}

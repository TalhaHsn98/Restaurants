using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Application.Restaurants.Dtos;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IMapper mapper) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Getting Restaurant By {request.Id}");
            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException($"Restaurant does not exist");
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
    }
}

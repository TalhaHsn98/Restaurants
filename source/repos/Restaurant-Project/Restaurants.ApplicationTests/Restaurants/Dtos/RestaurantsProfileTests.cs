using Xunit;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
using FluentAssertions;

namespace Restaurants.Application.Restaurants.Dtos.Tests
{
    public class RestaurantsProfileTests
    {
        private IMapper _mapper;

        public RestaurantsProfileTests()
        {

            var configuration = new MapperConfiguration(cfg=>
            {
                cfg.AddProfile<RestaurantsProfile>();
            });

            _mapper = configuration.CreateMapper();
        }
        [Fact()]
        public void CreateMap_FroUpdateRestaurantToRestaurant()
        {

            //arrange
            var updateCommand = new UpdateRestaurantCommand()
            {
                Id = 1,
                Name = "Updated Restaurant"
            };

           var restaurant =  _mapper.Map<Restaurant>(updateCommand);


            restaurant.Id.Should().Be(1);
            restaurant.Name.Should().Be(updateCommand.Name);    

        }
    }
}
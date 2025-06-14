using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;
using System.ComponentModel;
using MediatR;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using static System.Net.Mime.MediaTypeNames;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Microsoft.AspNetCore.Authorization;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    //[Authorize(Policy = PolicyNames.CreatedAtleast2Restaurants)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRestaurantsQuery query)
    {
        var restaurants = await mediator.Send(query);
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        //var userId = User.Claims.FirstOrDefault(c => c.Type == "<id claim type>")!.Value;
    
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
       
        
        return Ok(restaurant);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
     

        return NotFound();
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));
        
        return NoContent();

      
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand Command)
    {
        int id = await mediator.Send(Command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

}



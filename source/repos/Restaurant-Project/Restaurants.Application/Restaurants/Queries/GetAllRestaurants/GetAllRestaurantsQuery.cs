using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Application.Restaurants.Dtos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurants.Application.Common;
using Restaurants.Domain.Constansts;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDto>>
    {
        public string? SearchPhrase {  get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; } 
        public SortDirection SortDirection {  get; set; }
    }
}

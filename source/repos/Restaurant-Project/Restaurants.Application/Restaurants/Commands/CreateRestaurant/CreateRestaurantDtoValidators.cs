using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidators : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidators()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);
            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description should not be empty");

            RuleFor(dto => dto.Category)
                .NotEmpty().WithMessage("Insert a valid cateory");

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .WithMessage("Please provide a valid email");

        }
    }
}

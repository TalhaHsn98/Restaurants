using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(dish => dish.price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be non negativiewno");


            RuleFor(dish => dish.KiloCalories)
                .GreaterThanOrEqualTo(0)
                .WithMessage("kilo must be non negativiewno");

        }
    }
}

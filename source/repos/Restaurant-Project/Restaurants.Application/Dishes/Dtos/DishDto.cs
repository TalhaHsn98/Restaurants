﻿using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Dtos
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;
        public decimal price { get; set; }

        public int? KiloCalories { get; set; }

     
    }
}

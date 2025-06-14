﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommand : IRequest
    {
        public string userEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}

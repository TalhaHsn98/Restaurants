using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommandHandler(ILogger<DeleteUserRoleCommandHandler> logger,
        UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<DeleteUserRoleCommand>
    {
        public async Task Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting user Roles: {@Requet}", request);

            var user = await userManager.FindByEmailAsync(request.userEmail)
                ?? throw new NotFoundException(request.userEmail);

            var role = await roleManager.FindByNameAsync(request.RoleName)
                ?? throw new NotFoundException($"{request.RoleName}");

            await userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}

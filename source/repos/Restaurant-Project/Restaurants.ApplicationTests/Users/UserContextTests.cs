using Xunit;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moq;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUserTest_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {

            //arrange

            var dateOfBirth = new DateOnly(1990, 1, 1);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, "1"),
                new(ClaimTypes.Email, "test@test.com"),
                new(ClaimTypes.Role, UserRoles.Admin),
                new(ClaimTypes.Role, UserRoles.User),
                new("Nationality", "German"),
                new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))


            };

            var user = new ClaimsPrincipal( new ClaimsIdentity(claims,"Test" ) );

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContext(httpContextAccessorMock.Object);


            //act

            var currentuser = userContext.GetCurrentUser();

            //assert

            currentuser.Should().NotBeNull();
            currentuser.Id.Should().Be("1");
            currentuser.Email.Should().Be("test@test.com");
            currentuser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
            currentuser.Nationality.Should().Be("German");
            currentuser.DateOfBirth.Should().Be(dateOfBirth);
        }
    }
}
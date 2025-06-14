using Xunit;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class CurrentUserTests { 
    
        // TestMethod_Scenario_ExpectedResult
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            //Arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            //Act
            var isinole = currentUser.IsInRole(roleName);


            //Assert
            isinole.Should().BeTrue();
        }



        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            //Arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            //Act
            var isinole = currentUser.IsInRole(UserRoles.Owner);


            //Assert
            isinole.Should().BeFalse();

        }

        [Fact()]
        public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
        {
            //Arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            //Act
            var isinole = currentUser.IsInRole(UserRoles.Admin.ToLower());


            //Assert
            isinole.Should().BeFalse();

        }
    }
}
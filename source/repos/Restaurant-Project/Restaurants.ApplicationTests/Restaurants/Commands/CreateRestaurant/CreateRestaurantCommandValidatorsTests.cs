using FluentValidation.TestHelper;
using Xunit;


namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorsTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {

            //arrange


            var command = new CreateRestaurantCommand() { 
                
                Name = "Test",
                Category = "Indian",
                Description = "Got you in it",
                ContactEmail = "test@teast.com",
                PostalCode = 45432
            
            };
            //act


            var Validation = new CreateRestaurantCommandValidators();

            var result = Validation.TestValidate(command);
            //assert

            result.ShouldNotHaveAnyValidationErrors();

        }


        [Fact()]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            //Arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Tt",
                Description = ""
            };

            var validation = new CreateRestaurantCommandValidators();

            //Act

            var result = validation.TestValidate(command);

            //Assert

            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Description);

        }

        [Theory()]
        [InlineData("Indian")]
        [InlineData("Japanese")]
        [InlineData("America")]
        [InlineData("africa")]
        [InlineData("autralia")]
        public void validator_ForValidCategory_ShouldNotHaveAnyValidationErrors(string category)
        {
            var command = new CreateRestaurantCommand() { Category = category };

            //Act

            var Validation = new CreateRestaurantCommandValidators();

            var result = Validation.TestValidate(command);


            result.ShouldNotHaveValidationErrorFor(c => c.Category);


        }

    }
}
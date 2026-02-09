using OnlineQuizz.Application.Features.Auth.Register.Commands;
using Shouldly;
using Xunit;

namespace OnlineQuizz.Application.UnitTests.Auth.Register.Commands
{
    public class RegisterCommandValidatorTests
    {
        private static RegisterCommand CreateValidCommand() => new RegisterCommand
        {
            FirstName = "Alice",
            LastName = "Tester",
            Email = "alice@example.com",
            UserName = "alice",
            Password = "Password1!"
        };

        [Fact]
        public async Task Validate_ValidCommand_Passes()
        {
            var validator = new RegisterCommandValidator();
            var command = CreateValidCommand();

            var result = await validator.ValidateAsync(command);

            result.IsValid.ShouldBeTrue();
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Validate_MissingFirstName_Fails()
        {
            var validator = new RegisterCommandValidator();
            var command = CreateValidCommand();
            command.FirstName = "";

            var result = await validator.ValidateAsync(command);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.PropertyName == "FirstName");
        }

        [Fact]
        public async Task Validate_WeakPassword_Fails()
        {
            var validator = new RegisterCommandValidator();
            var command = CreateValidCommand();
            command.Password = "weak";

            var result = await validator.ValidateAsync(command);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.PropertyName == "Password");
        }
    }
}

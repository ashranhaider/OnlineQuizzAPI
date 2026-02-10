using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using Shouldly;
using Xunit;

namespace OnlineQuizz.Application.UnitTests.Quizzes.Commands
{
    public class CreateQuizzCommandValidatorTests
    {
        private static CreateQuizzCommand CreateValidCommand() => new CreateQuizzCommand
        {
            Name = "Sample Quizz",
            TimeAllowed = 10,
            IsActive = true
        };

        [Fact]
        public async Task Validate_ValidCommand_Passes()
        {
            var validator = new CreateQuizzCommandValidator();
            var command = CreateValidCommand();

            var result = await validator.ValidateAsync(command);

            result.IsValid.ShouldBeTrue();
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Validate_MissingName_Fails()
        {
            var validator = new CreateQuizzCommandValidator();
            var command = CreateValidCommand();
            command.Name = "";

            var result = await validator.ValidateAsync(command);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.PropertyName == "Name");
        }

        [Fact]
        public async Task Validate_TimeAllowedLessThanOne_Fails()
        {
            var validator = new CreateQuizzCommandValidator();
            var command = CreateValidCommand();
            command.TimeAllowed = 0;

            var result = await validator.ValidateAsync(command);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.PropertyName == "TimeAllowed");
        }
    }
}

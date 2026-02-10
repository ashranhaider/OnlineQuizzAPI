using OnlineQuizz.Application.Features.Quizzes.Commands.DeleteQuizz;
using Shouldly;
using Xunit;

namespace OnlineQuizz.Application.UnitTests.Quizzes.Commands
{
    public class DeleteQuizzCommandValidatorTests
    {
        [Fact]
        public async Task Validate_WithId_Passes()
        {
            var validator = new DeleteQuizzCommandValidator();
            var command = new DeleteQuizzCommand { Id = 1 };

            var result = await validator.ValidateAsync(command);

            result.IsValid.ShouldBeTrue();
        }
    }
}

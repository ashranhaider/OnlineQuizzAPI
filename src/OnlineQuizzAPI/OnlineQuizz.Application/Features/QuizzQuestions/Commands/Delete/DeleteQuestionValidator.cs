using FluentValidation;
using OnlineQuizz.Application.Contracts.Persistence;

namespace OnlineQuizz.Application.Features.QuizzQuestions.Commands.Delete
{
    public class DeleteQuestionValidator : AbstractValidator<DeleteQuestionCommand>
    {
        public DeleteQuestionValidator()
        {
            RuleFor(q => q.Id)
                .NotNull()
                .GreaterThan(0).WithMessage("Question Id must be greater than zero!");
        }
    }
}

using FluentValidation;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuestionOptions.Commands.CreateQuestionOption
{
    public class CreateQuestionOptionCommandValidator : AbstractValidator<CreateQuestionOptionCommand>
    {
        public CreateQuestionOptionCommandValidator()
        {
            RuleFor(p => p.OptionText)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(q => q.QuestionId)
                .GreaterThan(0).WithMessage("Question Id must be greater than zero!");
        }
    }
}

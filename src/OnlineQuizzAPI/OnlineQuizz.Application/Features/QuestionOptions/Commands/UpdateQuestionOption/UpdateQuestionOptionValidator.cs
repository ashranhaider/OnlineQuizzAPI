using FluentValidation;
using OnlineQuizz.Application.Features.Quizzes.Commands.UpdateQuizz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuestionOptions.Commands.UpdateQuestionOption
{
    public class UpdateQuestionOptionValidator : AbstractValidator<UpdateQuestionOption>
    {
        public UpdateQuestionOptionValidator()
        {
            RuleFor(q => q.Id)
                .NotNull();

            RuleFor(p => p.OptionText)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(q => q.QuestionId)
                .NotNull();
        }
    }
}

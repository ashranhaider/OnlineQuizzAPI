using FluentValidation;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuizzQuestions.Commands.Update
{
    public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionValidator()
        {
            RuleFor(q => q.QuestionText)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("Maximum length of 500 exceeded.");

            RuleFor(q => q.Id)
                .NotNull();

            RuleFor(q => q.QuizzId)
                .NotNull();

            RuleFor(q => q.QuestionType)
                .NotNull()
                .IsInEnum().WithMessage("QuestionType must be a valid value.");

            RuleFor(q => q.Score)
                .LessThanOrEqualTo(1000).WithMessage("Score must be less than or equal to 1000.")
                .GreaterThanOrEqualTo(0).WithMessage("Score must be less than or equal to 0.");
        }
    }
}

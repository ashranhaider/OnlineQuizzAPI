using FluentValidation;
using OnlineQuizz.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz
{
    public class CreateQuizzCommandValidator : AbstractValidator<CreateQuizzCommand>
    {
        public CreateQuizzCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.UniqueURL)
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");
            
            RuleFor(q => q.IsActive)
                .NotNull();
        }
    }
}
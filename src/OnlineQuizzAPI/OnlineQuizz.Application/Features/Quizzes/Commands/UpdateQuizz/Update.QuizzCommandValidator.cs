using FluentValidation;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Commands.UpdateQuizz
{
    public class UpdateQuizzCommandValidator : AbstractValidator<UpdateQuizzCommand>
    {
        public UpdateQuizzCommandValidator()
        {
            RuleFor(q => q.Id)
                .NotNull();

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.UniqueURL)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");
            
            RuleFor(q => q.IsActive)
                .NotNull();
        }
    }
}
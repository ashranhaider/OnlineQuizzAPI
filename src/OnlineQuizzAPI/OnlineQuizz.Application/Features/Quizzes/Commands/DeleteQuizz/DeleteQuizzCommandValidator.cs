using FluentValidation;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Features.Events.Commands.DeleteEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Commands.DeleteQuizz
{
    public class DeleteQuizzCommandValidator : AbstractValidator<DeleteQuizzCommand>
    {
        public DeleteQuizzCommandValidator()
        {
            RuleFor(q => q.Id)
                .NotNull();
        }
    }
}
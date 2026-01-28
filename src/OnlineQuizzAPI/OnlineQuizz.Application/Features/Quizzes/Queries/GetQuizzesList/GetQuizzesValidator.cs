using FluentValidation;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList
{
    public class GetQuizzesValidator : AbstractValidator<GetQuizzesQuery>
    {
        public GetQuizzesValidator()
        {
            // If page is provided, it must be > 0
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .When(x => x.Page.HasValue);

            // If size is provided, it must be within range
            RuleFor(x => x.Size)
                .InclusiveBetween(1, 100)
                .When(x => x.Size.HasValue);

            // Pair validation (MOST IMPORTANT)
            RuleFor(x => x)
                .Must(x => x.Page.HasValue == x.Size.HasValue)
                .WithMessage("Page and Size must be provided together.");
        }
    }

}

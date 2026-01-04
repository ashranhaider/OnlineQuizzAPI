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
            RuleFor(x => x.Page)
                .GreaterThan(0);

            RuleFor(x => x.Size)
                .InclusiveBetween(1, 1000);
        }
    }
}

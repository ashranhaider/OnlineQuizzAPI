using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuestionOptions.Commands.DeleteQuestionOption
{
    public class DeleteQuestionOptionValidator : AbstractValidator<DeleteQuestionOptionCommand>
    {
        public DeleteQuestionOptionValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
    
}

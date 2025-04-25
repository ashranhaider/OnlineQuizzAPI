using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuestionOptions.Commands.DeleteQuestionOption
{
    public class DeleteQuestionOptionCommand : IRequest
    {
        public int Id { get; set; }
    }    
}

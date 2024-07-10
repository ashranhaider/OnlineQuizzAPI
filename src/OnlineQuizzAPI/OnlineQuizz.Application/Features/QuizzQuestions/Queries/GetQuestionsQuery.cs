using MediatR;
using OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuizzQuestions.Queries
{
    public class GetQuestionsQuery : IRequest<List<GetQuestionsVM>>
    {
        public int QuizzId { get; set; }
    }
}

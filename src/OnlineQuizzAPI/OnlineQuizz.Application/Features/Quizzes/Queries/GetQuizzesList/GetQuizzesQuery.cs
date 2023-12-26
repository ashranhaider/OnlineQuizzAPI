using MediatR;
using OnlineQuizz.Application.Features.Events.Queries.GetEventsList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList
{
    public class GetQuizzesQuery : IRequest<List<GetQuizzVM>>
    {
    }
}

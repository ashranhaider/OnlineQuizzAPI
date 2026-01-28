using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList
{
    public class GetQuizzesQuery : IRequest<GetQuizzVM>
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
    }
}

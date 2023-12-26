using MediatR;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Commands.DeleteQuizz
{
    public class DeleteQuizzCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}

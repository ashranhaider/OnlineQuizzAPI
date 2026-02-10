using MediatR;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz
{
    public class CreateQuizzCommand : IRequest<int>
    {
        public string Name { get; set; } = String.Empty;
        public int? TimeAllowed { get; set; }
        public bool IsActive { get; set; }
    }
}

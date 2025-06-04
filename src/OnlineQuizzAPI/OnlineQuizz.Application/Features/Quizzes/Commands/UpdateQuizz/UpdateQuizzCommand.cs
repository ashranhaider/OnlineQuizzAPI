using MediatR;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Commands.UpdateQuizz
{
    public class UpdateQuizzCommand : IRequest<Quizz>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UniqueURL { get; set; }
        public bool IsActive { get; set; }
    }
}

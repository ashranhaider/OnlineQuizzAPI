using MediatR;
using OnlineQuizz.Domain.Entities;
using OnlineQuizz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuizzQuestions.Commands
{
    public class CreateQuestionCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = "";
        public int QuestionType { get; set; }
        public byte[] QuestionImage { get; set; } = new byte[0];
        public int QuizzId { get; set; }
    }
}

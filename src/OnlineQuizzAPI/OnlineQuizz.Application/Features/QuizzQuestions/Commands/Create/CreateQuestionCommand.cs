using MediatR;
using OnlineQuizz.Domain.Entities;
using OnlineQuizz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuizzQuestions.Commands.Create
{
    public class CreateQuestionCommand : IRequest<int>
    {
        public string QuestionText { get; set; } = "";
        public QuestionTypes QuestionType { get; set; }
        //public byte[] QuestionImage { get; set; } = new byte[0];
        public int QuizzId { get; set; }
    }
}

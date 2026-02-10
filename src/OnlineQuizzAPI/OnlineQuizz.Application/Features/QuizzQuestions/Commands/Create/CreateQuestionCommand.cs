using MediatR;
using OnlineQuizz.Domain.Entities;
using OnlineQuizz.Domain.Enums;
using OnlineQuizz.Application.Features.QuestionOptions.Commands.CreateQuestionOption;
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
        public bool IsActive { get; set; }
        public double Score { get; set; }
        //public byte[] QuestionImage { get; set; } = new byte[0];
        public int QuizzId { get; set; }
        public List<CreateQuestionOptionCommand>? QuestionOptions { get; set; }
    }
}

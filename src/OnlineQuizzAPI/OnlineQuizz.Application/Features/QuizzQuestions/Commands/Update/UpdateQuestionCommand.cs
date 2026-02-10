using MediatR;
using OnlineQuizz.Domain.Entities;
using OnlineQuizz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuizzQuestions.Commands.Update
{
    public class UpdateQuestionCommand : IRequest
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = "";
        public QuestionTypes QuestionType { get; set; }
        //public byte[] QuestionImage { get; set; } = new byte[0];
        public bool IsActive { get; set; }
        public double Score { get; set; }
        public int QuizzId { get; set; }
        public List<UpsertQuestionOptionDto>? QuestionOptions { get; set; }
    }

    public class UpsertQuestionOptionDto
    {
        public int? Id { get; set; }
        public string OptionText { get; set; } = "";
        public bool IsCorrect { get; set; }
        public byte[]? OptionImage { get; set; }
    }
}

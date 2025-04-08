using MediatR;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuestionOptions.Commands.UpdateQuestionOption
{
    public class UpdateQuestionOption : IRequest
    {
        public int Id { get; set; }
        public string OptionText { get; set; } = "";
        public bool IsCorrect { get; set; }
        //public byte[] OptionImage { get; set; } = new byte[0];
        [JsonIgnore]
        public int QuestionId { get; set; }
    }
}

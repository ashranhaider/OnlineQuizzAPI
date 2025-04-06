using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuestionOptions.Commands.CreateQuestionOption
{
    public class CreateQuestionOptionCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string OptionText { get; set; } = "";
        public bool IsCorrect { get; set; }
        public byte[] OptionImage { get; set; } = new byte[0];
        public int QuestionId { get; set; }
    }
}

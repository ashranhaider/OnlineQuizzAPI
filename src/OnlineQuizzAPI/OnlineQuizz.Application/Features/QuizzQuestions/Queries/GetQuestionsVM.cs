using OnlineQuizz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuizzQuestions.Queries
{
    public class GetQuestionsVM
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = String.Empty;
        public string QuestionType { get; set; } = String.Empty;
        public byte[] QuestionImage { get; set; } = new byte[0];
        public int QuizzId { get; set; }
        public string QuizzName { get; set; } = String.Empty;
    }
}

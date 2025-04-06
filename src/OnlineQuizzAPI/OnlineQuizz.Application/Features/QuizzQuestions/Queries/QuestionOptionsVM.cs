using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuizzQuestions.Queries
{
    public class QuestionOptionsVM
    {
        public int Id { get; set; }
        public string OptionText { get; set; } = "";
        public bool IsCorrect { get; set; }
        public byte[] OptionImage { get; set; } = new byte[0];
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}

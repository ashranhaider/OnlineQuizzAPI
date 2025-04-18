using OnlineQuizz.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Domain.Entities
{
    public class Answer: AuditableEntity
    {
        public int Id { get; set; }
        public string AnswerText { get; set; } = null!;
        // Required relationship with Question (non-nullable)
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; } = null!;

        // Optional relationship with QuestionOption (nullable)
        public int? QuestionOptionId { get; set; }
        public virtual QuestionOption? QuestionOption { get; set; } // Nullable
        public int AttemptId { get; set; }
        public virtual Attempt Attempt { get; set; } = null!;
    }
}

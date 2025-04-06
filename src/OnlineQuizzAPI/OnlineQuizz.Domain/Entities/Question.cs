using OnlineQuizz.Domain.Common;
using OnlineQuizz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Domain.Entities
{
    public class Question : AuditableEntity
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = "";
        public QuestionTypes QuestionType { get; set; }
        public byte[] QuestionImage { get; set; } = new byte[0];
        public bool IsActive { get; set; }
        public double Score { get; set; }
        public int QuizzId { get; set; }
        public virtual Quizz? Quizz { get; set; } = new Quizz();
        public virtual ICollection<QuestionOption>? QuestionOptions { get; set; } = new List<QuestionOption>();
        public virtual ICollection<Answer>? Answers { get; set; } = new List<Answer>();
    }
}

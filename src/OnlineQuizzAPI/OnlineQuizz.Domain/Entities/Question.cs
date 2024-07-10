using OnlineQuizz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = "";
        public QuestionTypes QuestionType { get; set; }
        public byte[] QuestionImage { get; set; } = new byte[0];
        [ForeignKey("Quizz")]
        public int QuizzId { get; set; }
        public virtual Quizz Quizz { get; set; } = new Quizz();
    }
}

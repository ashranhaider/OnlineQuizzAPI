using OnlineQuizz.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Domain.Entities
{
    public class Attempt : AuditableEntity
    {
        public int Id { get; set; }

        public int QuizzId { get; set; }
        public Quizz Quizz { get; set; } = null!;
        public int TotalScore { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}

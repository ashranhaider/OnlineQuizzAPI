using OnlineQuizz.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Domain.Entities
{
    public class Quizz : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int? TimeAllowed { get; set; }
        public string OwnerUserId { get; set; } = String.Empty;
        public bool IsActive { get; set; }
        public ICollection<Question> Questions { get; private set; } = new List<Question>();
        public ICollection<Attempt> Attempts { get; private set; } = new List<Attempt>();
    }
}

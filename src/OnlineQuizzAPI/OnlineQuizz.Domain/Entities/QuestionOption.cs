using OnlineQuizz.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Domain.Entities
{
    public class QuestionOption: AuditableEntity
    {
        public int Id { get; set; }
        public string OptionText { get; set; } = "";
        public bool IsCorrect { get; set; }
        public byte[] OptionImage { get; set; } = new byte[0];
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; } = null!;
    }
}

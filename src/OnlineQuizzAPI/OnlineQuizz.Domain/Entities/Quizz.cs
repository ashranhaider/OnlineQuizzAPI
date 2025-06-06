﻿using OnlineQuizz.Domain.Common;
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
        public string URL { get; set; } = String.Empty;
        public string UniqueURL { get; set; } = String.Empty;
        public bool IsActive { get; set; }
        public virtual ICollection<Question>? Questions { get; set; }
        public virtual ICollection<Attempt>? Attempts { get; set; }
    }
}

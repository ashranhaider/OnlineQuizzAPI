using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList
{
    public class GetQuizzVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string UniqueURL { get; set; } = String.Empty;
        public bool IsActive { get; set; }
    }
}

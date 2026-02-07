using OnlineQuizz.Application.Features.QuizzQuestions.Queries;
using System.Collections.Generic;

namespace OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzById
{
    public class GetQuizzByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UniqueURL { get; set; } = string.Empty;
        public int? TimeAllowed { get; set; }
        public bool IsActive { get; set; }
        public List<GetQuestionsVM> Questions { get; set; } = new List<GetQuestionsVM>();
    }
}

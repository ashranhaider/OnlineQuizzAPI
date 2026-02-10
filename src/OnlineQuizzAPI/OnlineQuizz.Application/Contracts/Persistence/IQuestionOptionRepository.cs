using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Application.Contracts.Persistence
{
    public interface IQuestionOptionRepository : IAsyncRepository<QuestionOption>
    {
        Task<List<QuestionOption>> GetByQuestionIdAsync(int questionId);
    }
}

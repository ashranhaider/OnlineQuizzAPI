using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Application.Contracts.Persistence
{
    public interface IAnswerRepository : IAsyncRepository<Answer>
    {
        Task<IReadOnlyList<Answer>> GetAnswersByQuestionIdAsync(int questionId);
    }
} 
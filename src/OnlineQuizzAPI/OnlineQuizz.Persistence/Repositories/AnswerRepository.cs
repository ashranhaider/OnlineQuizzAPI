using Microsoft.EntityFrameworkCore;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Persistence.Repositories
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {

        public AnswerRepository(OnlineQuizzDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Answer>> GetAnswersByQuestionIdAsync(int questionId)
        {
            return await _dbContext.Answers
                .Where(a => a.QuestionId == questionId)
                .ToListAsync();
        }
    }
} 
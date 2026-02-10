using Microsoft.EntityFrameworkCore;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Persistence.Repositories
{
    public class QuestionOptionRepository : BaseRepository<QuestionOption>, IQuestionOptionRepository
    {
        public QuestionOptionRepository(OnlineQuizzDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<QuestionOption>> GetByQuestionIdAsync(int questionId)
        {
            return await _dbContext.Set<QuestionOption>()
                .Where(qo => qo.QuestionId == questionId)
                .ToListAsync();
        }
    }
}

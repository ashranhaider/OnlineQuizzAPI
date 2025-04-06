using Microsoft.EntityFrameworkCore;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Persistence.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(OnlineQuizzDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Question>> GetQuestionsByQuizzIdAsync(int quizzId)
        {
            return await _dbContext.Questions.Where(q => q.QuizzId == quizzId).Include(q => q.QuestionOptions).ToListAsync();
        }
    }
}
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
    public class QuizzRepository : BaseRepository<Quizz>, IQuizzRepository
    {
        public QuizzRepository(OnlineQuizzDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Quizz>> GetPagedQuizzes(int page, int size)
        {
            return await _dbContext.Quizzes.Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }
    }
}

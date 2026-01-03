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

        public async Task<List<Quizz>> GetPagedQuizzes(string UserId, int page, int size)
        {
            return await _dbContext.Quizzes.Where(q => q.OwnerUserId == UserId && q.IsActive).Skip((page - 1) * size).OrderBy(q => q.CreatedDate).Take(size).AsNoTracking().ToListAsync();
        }
    }
}

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

        /// <summary>
        /// Returns a paginated list of quizzes along with the total quiz count
        /// for a specific user.
        /// </summary>
        /// <param name="userId">The owner user identifier.</param>
        /// <param name="page">Page number (1-based).</param>
        /// <param name="size">Number of items per page.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A tuple where:
        /// <list type="bullet">
        /// <item>
        /// <description>Item1: List of quizzes for the requested page.</description>
        /// </item>
        /// <item>
        /// <description>Item2: Total count of quizzes for the user.</description>
        /// </item>
        /// </list>
        /// </returns>

        public async Task<(List<Quizz>, int)> GetPagedQuizzesWithCount(string userId, int? page, int? size, CancellationToken cancellationToken)
        {
            var baseQuery = _dbContext.Quizzes
                .Where(q => q.OwnerUserId == userId)
                .OrderByDescending(q => q.CreatedDate)
                .AsNoTracking();

            var total = await baseQuery.CountAsync(cancellationToken);
            
            if(page.HasValue && size.HasValue)
            {
                baseQuery = baseQuery
                .Skip((page.Value - 1) * size.Value)
                .Take(size.Value);
            }
            var items = await baseQuery.ToListAsync(cancellationToken);

            return (items, total);
        }

        public async Task<int> GetTotalQuizzesCount(string UserId, CancellationToken cancellationToken = default)
        {

            return await _dbContext.Quizzes.Where(q => q.OwnerUserId == UserId && q.IsActive).CountAsync(cancellationToken);
        }
    }
}

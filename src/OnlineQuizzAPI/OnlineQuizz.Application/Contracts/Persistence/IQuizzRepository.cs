using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Contracts.Persistence
{
    public interface IQuizzRepository : IAsyncRepository<Quizz>
    {
        Task<List<Quizz>> GetPagedQuizzes(int page, int size);
    }
}

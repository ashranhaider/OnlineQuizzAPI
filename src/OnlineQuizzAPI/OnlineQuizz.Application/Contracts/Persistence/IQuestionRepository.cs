using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Contracts.Persistence
{
    public interface IQuestionRepository : IAsyncRepository<Question>
    {
        Task<List<Question>> GetQuestionsByQuizzIdAsync(int quizzId);
    }
}

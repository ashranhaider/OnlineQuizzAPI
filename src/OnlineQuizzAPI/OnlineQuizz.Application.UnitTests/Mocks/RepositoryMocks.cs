using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Domain.Entities;
using Moq;

namespace OnlineQuizz.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IQuizzRepository> GetQuizzRepository()
        {
            
            var categories = new List<Quizz>
            {
                new Quizz
                {
                    Id = 1,
                    Name = "Concerts"
                },
                new Quizz
                {
                    Id = 2,
                    Name = "Musicals"
                },
                new Quizz
                {
                    Id = 3,
                    Name = "Conferences"
                },
                 new Quizz
                {
                    Id = 4,
                    Name = "Plays"
                }
            };

            var mockQuizzRepository = new Mock<IQuizzRepository>();
            mockQuizzRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(categories);

            mockQuizzRepository.Setup(repo => repo.AddAsync(It.IsAny<Quizz>())).ReturnsAsync(
                (Quizz Quizz) =>
                {
                    categories.Add(Quizz);
                    return Quizz;
                });

            return mockQuizzRepository;
        }
    }
}

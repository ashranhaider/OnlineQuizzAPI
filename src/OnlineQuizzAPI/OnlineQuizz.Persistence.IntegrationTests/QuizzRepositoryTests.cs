using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Domain.Entities;
using OnlineQuizz.Persistence.Repositories;
using Shouldly;
using Xunit;

namespace OnlineQuizz.Persistence.IntegrationTests
{
    public class QuizzRepositoryTests
    {
        [Fact]
        public async Task AddAsync_PersistsQuizz()
        {
            var options = new DbContextOptionsBuilder<OnlineQuizzDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var loggedInUser = new Mock<ILoggedInUserService>();
            loggedInUser.SetupGet(u => u.UserId).Returns("user-1");

            await using var context = new OnlineQuizzDbContext(options, loggedInUser.Object);
            var repository = new QuizzRepository(context);

            var quizz = new Quizz
            {
                Name = "Persisted",
                IsActive = true,
                OwnerUserId = "user-1"
            };

            var created = await repository.AddAsync(quizz);

            created.Id.ShouldBeGreaterThan(0);
            (await context.Quizzes.CountAsync()).ShouldBe(1);
        }
    }
}

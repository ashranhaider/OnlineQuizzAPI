using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Domain.Entities;
using OnlineQuizz.Persistence.Repositories;
using Shouldly;
using Xunit;

namespace OnlineQuizz.Persistence.IntegrationTests
{
    public class QuizzRepositoryUpdateDeleteTests
    {
        [Fact]
        public async Task UpdateAsync_PersistsChanges()
        {
            var options = new DbContextOptionsBuilder<OnlineQuizzDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var loggedInUser = new Mock<ILoggedInUserService>();
            loggedInUser.SetupGet(u => u.UserId).Returns("user-1");

            await using var context = new OnlineQuizzDbContext(options, loggedInUser.Object);
            var repository = new QuizzRepository(context);

            var quizz = new Quizz { Name = "Old", IsActive = false, OwnerUserId = "user-1" };
            await repository.AddAsync(quizz);

            quizz.Name = "New";
            quizz.IsActive = true;
            await repository.UpdateAsync(quizz);

            var updated = await context.Quizzes.FirstAsync();
            updated.Name.ShouldBe("New");
            updated.IsActive.ShouldBeTrue();
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntity()
        {
            var options = new DbContextOptionsBuilder<OnlineQuizzDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var loggedInUser = new Mock<ILoggedInUserService>();
            loggedInUser.SetupGet(u => u.UserId).Returns("user-1");

            await using var context = new OnlineQuizzDbContext(options, loggedInUser.Object);
            var repository = new QuizzRepository(context);

            var quizz = new Quizz { Name = "Delete", IsActive = true, OwnerUserId = "user-1" };
            await repository.AddAsync(quizz);

            await repository.DeleteAsync(quizz);

            (await context.Quizzes.CountAsync()).ShouldBe(0);
        }
    }
}

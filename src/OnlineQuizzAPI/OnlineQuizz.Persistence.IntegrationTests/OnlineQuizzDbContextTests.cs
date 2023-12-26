using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace OnlineQuizz.Persistence.IntegrationTests
{
    public class OnlineQuizzDbContextTests
    {
        private readonly OnlineQuizzDbContext _onlineQuizzDbContext;
        private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private readonly string _loggedInUserId;

        public OnlineQuizzDbContextTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<OnlineQuizzDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _loggedInUserId = "00000000-0000-0000-0000-000000000000";
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

            _onlineQuizzDbContext = new OnlineQuizzDbContext(dbContextOptions, _loggedInUserServiceMock.Object);
        }

        [Fact]
        public async void Save_SetCreatedByProperty()
        {
            var quizz = new Quizz() {Id = 1, Name = "Test quizz" };

            _onlineQuizzDbContext.Quizzes.Add(quizz);
            await _onlineQuizzDbContext.SaveChangesAsync();

            quizz.CreatedBy.ShouldBe(_loggedInUserId);
        }
    }
}

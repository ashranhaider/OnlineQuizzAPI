using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Features.Quizzes.Commands.UpdateQuizz;
using OnlineQuizz.Application.Profiles;
using OnlineQuizz.Domain.Entities;
using Shouldly;
using Xunit;

namespace OnlineQuizz.Application.UnitTests.Quizzes.Commands
{
    public class UpdateQuizzCommandHandlerTests
    {
        private readonly IMapper _mapper;

        public UpdateQuizzCommandHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ExistingQuizz_UpdatesAndReturnsQuizz()
        {
            var quizzRepository = new Mock<IQuizzRepository>();
            var loggedInUserService = new Mock<ILoggedInUserService>();
            loggedInUserService.SetupGet(s => s.UserId).Returns("user-1");
            var logger = new Mock<ILogger<UpdateQuizzCommandHandler>>();

            var existing = new Quizz
            {
                Id = 10,
                Name = "Old",
                IsActive = false,
                TimeAllowed = 5
            };

            quizzRepository.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(existing);

            var handler = new UpdateQuizzCommandHandler(_mapper, quizzRepository.Object, logger.Object, loggedInUserService.Object);

            var result = await handler.Handle(new UpdateQuizzCommand
            {
                Id = 10,
                Name = "New",
                IsActive = true,
                TimeAllowed = 20
            }, CancellationToken.None);

            result.Name.ShouldBe("New");
            result.IsActive.ShouldBeTrue();
            result.TimeAllowed.ShouldBe(20);
            result.LastModifiedBy.ShouldBe("user-1");
            result.LastModifiedDate.ShouldNotBeNull();

            quizzRepository.Verify(r => r.UpdateAsync(existing), Times.Once);
        }

        [Fact]
        public async Task Handle_QuizzNotFound_ThrowsNotFoundException()
        {
            var quizzRepository = new Mock<IQuizzRepository>();
            var loggedInUserService = new Mock<ILoggedInUserService>();
            var logger = new Mock<ILogger<UpdateQuizzCommandHandler>>();

            quizzRepository.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Quizz?)null);

            var handler = new UpdateQuizzCommandHandler(_mapper, quizzRepository.Object, logger.Object, loggedInUserService.Object);

            await Should.ThrowAsync<NotFoundException>(() =>
                handler.Handle(new UpdateQuizzCommand { Id = 99, Name = "X" }, CancellationToken.None));
        }
    }
}

using AutoMapper;
using Moq;
using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using OnlineQuizz.Application.Profiles;
using OnlineQuizz.Domain.Entities;
using Shouldly;
using Xunit;

namespace OnlineQuizz.Application.UnitTests.Quizzes.Commands
{
    public class CreateQuizzCommandHandlerTests
    {
        private readonly IMapper _mapper;

        public CreateQuizzCommandHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCommand_SetsOwnerAndReturnsId()
        {
            var quizzRepository = new Mock<IQuizzRepository>();
            var loggedInUserService = new Mock<ILoggedInUserService>();
            loggedInUserService.SetupGet(s => s.UserId).Returns("user-1");

            Quizz? addedQuizz = null;

            quizzRepository
                .Setup(r => r.AddAsync(It.IsAny<Quizz>()))
                .Callback<Quizz>(q =>
                {
                    addedQuizz = q;
                    q.Id = 42;
                })
                .ReturnsAsync((Quizz q) => q);

            var handler = new CreateQuizzCommandHandler(_mapper, quizzRepository.Object, loggedInUserService.Object);

            var result = await handler.Handle(new CreateQuizzCommand
            {
                Name = "Sample",
                TimeAllowed = 5,
                IsActive = true
            }, CancellationToken.None);

            result.ShouldBe(42);
            addedQuizz.ShouldNotBeNull();
            addedQuizz!.OwnerUserId.ShouldBe("user-1");
            addedQuizz.Name.ShouldBe("Sample");
            addedQuizz.TimeAllowed.ShouldBe(5);
            addedQuizz.IsActive.ShouldBeTrue();

            quizzRepository.Verify(r => r.AddAsync(It.IsAny<Quizz>()), Times.Once);
        }
    }
}

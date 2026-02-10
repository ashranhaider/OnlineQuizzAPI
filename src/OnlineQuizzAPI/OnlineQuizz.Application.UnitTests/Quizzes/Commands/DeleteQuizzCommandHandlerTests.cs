using AutoMapper;
using Moq;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Features.Quizzes.Commands.DeleteQuizz;
using OnlineQuizz.Application.Profiles;
using OnlineQuizz.Domain.Entities;
using Shouldly;
using Xunit;

namespace OnlineQuizz.Application.UnitTests.Quizzes.Commands
{
    public class DeleteQuizzCommandHandlerTests
    {
        private readonly IMapper _mapper;

        public DeleteQuizzCommandHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ExistingQuizz_DeletesAndReturnsId()
        {
            var quizzRepository = new Mock<IQuizzRepository>();
            var quizz = new Quizz { Id = 5, Name = "To delete" };

            quizzRepository.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(quizz);

            var handler = new DeleteQuizzCommandHandler(_mapper, quizzRepository.Object);

            var result = await handler.Handle(new DeleteQuizzCommand { Id = 5 }, CancellationToken.None);

            result.ShouldBe(5);
            quizzRepository.Verify(r => r.DeleteAsync(quizz), Times.Once);
        }

        [Fact]
        public async Task Handle_QuizzNotFound_ThrowsNotFoundException()
        {
            var quizzRepository = new Mock<IQuizzRepository>();
            quizzRepository.Setup(r => r.GetByIdAsync(77)).ReturnsAsync((Quizz?)null);

            var handler = new DeleteQuizzCommandHandler(_mapper, quizzRepository.Object);

            await Should.ThrowAsync<NotFoundException>(() =>
                handler.Handle(new DeleteQuizzCommand { Id = 77 }, CancellationToken.None));
        }
    }
}

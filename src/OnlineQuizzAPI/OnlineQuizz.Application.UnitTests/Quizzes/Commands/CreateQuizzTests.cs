using AutoMapper;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Profiles;
using OnlineQuizz.Application.UnitTests.Mocks;
using OnlineQuizz.Domain.Entities;
using Moq;
using Shouldly;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;

namespace OnlineQuizz.Application.UnitTests.Categories.Commands
{
    public class CreateQuizzTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IQuizzRepository> _mockQuizzRepository;

        public CreateQuizzTests()
        {
            _mockQuizzRepository = RepositoryMocks.GetQuizzRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoriesRepo()
        {
            var handler = new CreateQuizzCommandHandler(_mapper, _mockQuizzRepository.Object);

            await handler.Handle(new CreateQuizzCommand() { Name = "Test", UniqueURL = "test" }, CancellationToken.None);

            var allCategories = await _mockQuizzRepository.Object.ListAllAsync();
            allCategories.Count.ShouldBe(5);
        }
    }
}

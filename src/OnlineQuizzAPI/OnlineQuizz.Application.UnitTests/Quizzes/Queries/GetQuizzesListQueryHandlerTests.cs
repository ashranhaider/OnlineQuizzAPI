using AutoMapper;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Profiles;
using OnlineQuizz.Application.UnitTests.Mocks;
using OnlineQuizz.Domain.Entities;
using Moq;
using Shouldly;
using OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList;

namespace OnlineQuizz.Application.UnitTests.Categories.Queries
{
    public class GetCategoriesListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Quizz>> _mockQuizzRepository;

        public GetCategoriesListQueryHandlerTests()
        {
            _mockQuizzRepository = RepositoryMocks.GetQuizzRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetQuizzesListTest()
        {
            var handler = new GetQuizzesQueryHandler(_mapper, _mockQuizzRepository.Object);

            var result = await handler.Handle(new GetQuizzesQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<GetQuizzVM>>();

            result.Count.ShouldBe(4);
        }
    }
}

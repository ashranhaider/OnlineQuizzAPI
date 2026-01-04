using AutoMapper;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Profiles;
using OnlineQuizz.Application.UnitTests.Mocks;
using OnlineQuizz.Domain.Entities;
using Moq;
using Shouldly;
using OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList;
using OnlineQuizz.Application.Contracts;

namespace OnlineQuizz.Application.UnitTests.Categories.Queries
{
    public class GetCategoriesListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILoggedInUserService> _mockLoggedInUserService;
        private readonly Mock<IQuizzRepository> _mockQuizzRepository;

        public GetCategoriesListQueryHandlerTests()
        {
            _mockQuizzRepository = RepositoryMocks.GetQuizzRepository();
            _mockLoggedInUserService = new Mock<ILoggedInUserService>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetQuizzesListTest()
        {
            var handler = new GetQuizzesQueryHandler(_mapper, _mockQuizzRepository.Object, _mockLoggedInUserService.Object);

            var result = await handler.Handle(new GetQuizzesQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetQuizzVM>();

            result.Total.ShouldBe(4);
        }
    }
}

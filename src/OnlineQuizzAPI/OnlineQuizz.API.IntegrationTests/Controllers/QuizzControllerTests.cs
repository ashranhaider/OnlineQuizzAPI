using OnlineQuizz.API.IntegrationTests.Base;
using OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList;
using System.Text.Json;

namespace OnlineQuizz.API.IntegrationTests.Controllers
{

    public class QuizzControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public QuizzControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessResult()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/Quizz/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<GetQuizzVM>>(responseString);
            
            Assert.IsType<List<GetQuizzVM>>(result);
            Assert.NotEmpty(result);
        }
    }
}

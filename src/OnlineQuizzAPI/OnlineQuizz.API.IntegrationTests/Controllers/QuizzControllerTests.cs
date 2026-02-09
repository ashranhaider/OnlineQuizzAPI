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
            var client = _factory.GetAuthenticatedClient();

            var response = await client.GetAsync("/api/quizzes");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var result = JsonSerializer.Deserialize<GetQuizzVM>(responseString, options);

            Assert.NotNull(result);
            Assert.NotNull(result!.Quizzes);
            Assert.NotEmpty(result.Quizzes);
        }
    }
}

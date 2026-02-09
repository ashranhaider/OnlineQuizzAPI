using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineQuizz.Api.Controllers;
using OnlineQuizz.Application.Contracts.Identity;
using OnlineQuizz.Application.Features.Auth.Register.Commands;
using OnlineQuizz.Application.Responses;
using Shouldly;
using Xunit;

namespace OnlineQuizz.Api.UnitTests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task RegisterAsync_ReturnsOkWithApiResponse()
        {
            var mediator = new Mock<IMediator>();
            var authService = new Mock<IAuthenticationService>();

            var command = new RegisterCommand
            {
                FirstName = "Alice",
                LastName = "Tester",
                Email = "alice@example.com",
                UserName = "alice",
                Password = "Password1!"
            };

            var registrationResponse = new RegistrationResponse
            {
                UserId = "user-1",
                Message = "User registered successfully"
            };

            mediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(registrationResponse);

            var controller = new AccountController(authService.Object, mediator.Object);

            var result = await controller.RegisterAsync(command);

            var okResult = result.Result as OkObjectResult;
            okResult.ShouldNotBeNull();

            var apiResponse = okResult!.Value as ApiResponse<RegistrationResponse>;
            apiResponse.ShouldNotBeNull();

            apiResponse!.Data.ShouldBe(registrationResponse);
            apiResponse.Message.ShouldBe("User Registered");

            mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

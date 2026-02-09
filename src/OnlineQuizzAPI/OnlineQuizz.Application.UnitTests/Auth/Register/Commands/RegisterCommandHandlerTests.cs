using Moq;
using OnlineQuizz.Application.Contracts.Identity;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Features.Auth.Register.Commands;
using Shouldly;
using Xunit;

namespace OnlineQuizz.Application.UnitTests.Auth.Register.Commands
{
    public class RegisterCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_CallsAuthServiceAndReturnsResponse()
        {
            var authService = new Mock<IAuthenticationService>();
            var command = new RegisterCommand
            {
                FirstName = "Alice",
                LastName = "Tester",
                Email = "alice@example.com",
                UserName = "alice",
                Password = "Password1!"
            };

            var expectedResponse = new RegistrationResponse
            {
                UserId = "user-1",
                Message = "ok"
            };

            authService
                .Setup(a => a.RegisterAsync(It.Is<RegistrationRequest>(r =>
                    r.FirstName == command.FirstName &&
                    r.LastName == command.LastName &&
                    r.Email == command.Email &&
                    r.UserName == command.UserName &&
                    r.Password == command.Password)))
                .ReturnsAsync(expectedResponse);

            var handler = new RegisterCommandHandler(authService.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldBe(expectedResponse);
            authService.VerifyAll();
        }

        [Fact]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            var authService = new Mock<IAuthenticationService>();
            var command = new RegisterCommand
            {
                FirstName = "Alice",
                LastName = "Tester",
                Email = "bad-email",
                UserName = "al",
                Password = "Password1!"
            };

            var handler = new RegisterCommandHandler(authService.Object);

            await Should.ThrowAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

            authService.Verify(a => a.RegisterAsync(It.IsAny<RegistrationRequest>()), Times.Never);
        }
    }
}

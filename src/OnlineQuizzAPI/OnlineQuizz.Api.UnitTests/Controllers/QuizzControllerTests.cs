using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineQuizz.Api.Controllers;
using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using OnlineQuizz.Application.Features.Quizzes.Commands.UpdateQuizz;
using OnlineQuizz.Application.Features.Quizzes.Commands.DeleteQuizz;
using Shouldly;
using Xunit;

namespace OnlineQuizz.Api.UnitTests.Controllers
{
    public class QuizzControllerTests
    {
        [Fact]
        public async Task Create_ReturnsOkWithId()
        {
            var mediator = new Mock<IMediator>();
            var loggedInUserService = new Mock<ILoggedInUserService>();

            var command = new CreateQuizzCommand
            {
                Name = "Sample",
                TimeAllowed = 5,
                IsActive = true
            };

            mediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(123);

            var controller = new QuizzController(mediator.Object, loggedInUserService.Object);

            var result = await controller.Create(command);

            var okResult = result.Result as OkObjectResult;
            okResult.ShouldNotBeNull();
            okResult!.Value.ShouldBe(123);

            mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update_ReturnsOkWithQuizz()
        {
            var mediator = new Mock<IMediator>();
            var loggedInUserService = new Mock<ILoggedInUserService>();

            var command = new UpdateQuizzCommand
            {
                Id = 10,
                Name = "Updated",
                IsActive = true,
                TimeAllowed = 20
            };

            var quizz = new OnlineQuizz.Domain.Entities.Quizz
            {
                Id = 10,
                Name = "Updated",
                IsActive = true,
                TimeAllowed = 20
            };

            mediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(quizz);

            var controller = new QuizzController(mediator.Object, loggedInUserService.Object);

            var result = await controller.Update(command);

            var okResult = result.Result as OkObjectResult;
            okResult.ShouldNotBeNull();
            okResult!.Value.ShouldBe(quizz);

            mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsOkWithId()
        {
            var mediator = new Mock<IMediator>();
            var loggedInUserService = new Mock<ILoggedInUserService>();

            mediator.Setup(m => m.Send(It.IsAny<DeleteQuizzCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(7);

            var controller = new QuizzController(mediator.Object, loggedInUserService.Object);

            var result = await controller.Delete(7);

            var okResult = result.Result as OkObjectResult;
            okResult.ShouldNotBeNull();
            okResult!.Value.ShouldBe(7);

            mediator.Verify(m => m.Send(It.IsAny<DeleteQuizzCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizz.Application.Features.Answers.Commands.CreateAnswer;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;

namespace OnlineQuizz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddAnswer")]
        public async Task<ActionResult<int>> Create([FromBody] CreateAnswerCommand createAnswerCommand)
        {
            var response = await _mediator.Send(createAnswerCommand);
            return Ok(response);
        }
    }
}

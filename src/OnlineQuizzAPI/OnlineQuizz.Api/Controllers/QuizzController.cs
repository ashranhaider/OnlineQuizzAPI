using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using OnlineQuizz.Application.Features.Quizzes.Commands.DeleteQuizz;
using OnlineQuizz.Application.Features.Quizzes.Commands.UpdateQuizz;
using OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList;
using OnlineQuizz.Application.Features.QuizzQuestions.Commands;
using OnlineQuizz.Application.Features.QuizzQuestions.Queries;
using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class QuizzController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuizzController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region Quizzes

        [HttpPost(Name = "AddQuizz")]
        public async Task<ActionResult<int>> Create([FromBody] CreateQuizzCommand createQuizzCommand)
        {
            var response = await _mediator.Send(createQuizzCommand);
            return Ok(response);
        }
        [HttpPut(Name = "UpdateQuizz")]
        public async Task<ActionResult<Quizz>> Update([FromBody] UpdateQuizzCommand updateQuizzCommand)
        {
            var response = await _mediator.Send(updateQuizzCommand);
            return Ok(response);
        }
        [HttpGet(Name = "GetQuizzes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetQuizzVM>> GetQuizzes()
        {
            var getQuizzesQuery = new GetQuizzesQuery() { };
            var dtos = await _mediator.Send(getQuizzesQuery);

            return Ok(dtos);
        }

        [HttpDelete(Name = "DeleteQuizz/{quizzId}")]
        public async Task<ActionResult<Quizz>> Delete(int quizzId)
        {
            var response = await _mediator.Send(new DeleteQuizzCommand {Id = quizzId });
            return Ok(response);
        }

        #endregion Quizzes

    }
}
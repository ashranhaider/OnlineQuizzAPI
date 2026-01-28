using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using OnlineQuizz.Application.Features.Quizzes.Commands.DeleteQuizz;
using OnlineQuizz.Application.Features.Quizzes.Commands.UpdateQuizz;
using OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzById;
using OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList;
using OnlineQuizz.Application.Features.QuizzQuestions.Commands;
using OnlineQuizz.Application.Features.QuizzQuestions.Queries;
using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Api.Controllers
{
    [ApiController]
    [Route("api/quizzes")]
    [Authorize]
    public class QuizzController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuizzController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region Quizzes

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateQuizzCommand createQuizzCommand)
        {
            int response = await _mediator.Send(createQuizzCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult<Quizz>> Update([FromBody] UpdateQuizzCommand updateQuizzCommand)
        {
            Quizz response = await _mediator.Send(updateQuizzCommand);
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<GetQuizzVM>> GetAll([FromQuery] GetQuizzesQuery quizzQuery, CancellationToken cancellationToken)
        {
            GetQuizzVM quizzes = await _mediator.Send(quizzQuery, cancellationToken);
            return Ok(quizzes);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizzVM>> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var quizzes = await _mediator.Send(new GetQuizzByIdQuery { Id = id}, cancellationToken);
            return Ok(quizzes);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            int response = await _mediator.Send(new DeleteQuizzCommand {Id = id });
            return Ok(response);
        }

        #endregion Quizzes

    }
}
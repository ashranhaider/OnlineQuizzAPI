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
        public async Task<ActionResult<GetQuizzVM>> GetAll([FromQuery] GetQuizzesQuery quizzQuery)
        {
            List<GetQuizzVM> quizzes = await _mediator.Send(quizzQuery);
            return Ok(quizzes);
        }

        [HttpDelete]
        public async Task<ActionResult<int>> Delete(int quizzId)
        {
            int response = await _mediator.Send(new DeleteQuizzCommand {Id = quizzId });
            return Ok(response);
        }

        #endregion Quizzes

    }
}
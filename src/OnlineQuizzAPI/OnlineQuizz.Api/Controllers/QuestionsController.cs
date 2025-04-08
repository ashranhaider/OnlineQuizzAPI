﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizz.Application.Features.QuestionOptions.Commands.CreateQuestionOption;
using OnlineQuizz.Application.Features.QuestionOptions.Commands.UpdateQuestionOption;
using OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList;
using OnlineQuizz.Application.Features.QuizzQuestions.Commands;
using OnlineQuizz.Application.Features.QuizzQuestions.Queries;

namespace OnlineQuizz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Questions

        [HttpPost(Name = "AddQuestion")]
        public async Task<ActionResult<int>> CreateQuestion([FromBody] CreateQuestionCommand createQuestionCommand)
        {
            var response = await _mediator.Send(createQuestionCommand);
            return Ok(response);
        }
        [HttpGet(Name = "GetQuestions")]
        public async Task<ActionResult<GetQuizzVM>> GetQuestions(int quizzId)
        {
            GetQuestionsQuery questionsQuery = new GetQuestionsQuery { QuizzId = quizzId };
            var dtos = await _mediator.Send(questionsQuery);

            return Ok(dtos);
        }
        #endregion

        #region Question Options

        [HttpPost("{QuestionId}/QuestionOption", Name = "AddQuestionOption")]
        public async Task<ActionResult<int>> CreateQuestionOption([FromBody] CreateQuestionOptionCommand createQuestionOptionCommand, int QuestionId)
        {
            createQuestionOptionCommand.QuestionId = QuestionId;
            int response = await _mediator.Send(createQuestionOptionCommand);
            return Ok(response);
        }

        [HttpPatch("{QuestionId}/QuestionOption", Name = "UpdateQuestionOption")]
        public async Task<ActionResult<int>> UpdateQuestionOption([FromBody] UpdateQuestionOption updateQuestionOptionCommand, int QuestionId)
        {
            updateQuestionOptionCommand.QuestionId = QuestionId;
            await _mediator.Send(updateQuestionOptionCommand);
            return Ok();
        }
        #endregion
    }
}

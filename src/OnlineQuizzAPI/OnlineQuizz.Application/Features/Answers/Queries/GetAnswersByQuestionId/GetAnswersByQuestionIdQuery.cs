using MediatR;
using OnlineQuizz.Application.Features.Answers.Queries.GetAnswersByQuestionId;

namespace OnlineQuizz.Application.Features.Answers.Queries.GetAnswersByQuestionId
{
    public class GetAnswersByQuestionIdQuery : IRequest<List<GetAnswersByQuestionIdResponse>>
    {
        public int QuestionId { get; set; }
    }
} 
using MediatR;

namespace OnlineQuizz.Application.Features.Answers.Commands.CreateAnswer
{
    public class CreateAnswerCommand : IRequest<int>
    {
        public string AnswerText { get; set; } = "";
        public int QuestionId { get; set; }
    }
} 
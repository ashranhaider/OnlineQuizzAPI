using MediatR;

namespace OnlineQuizz.Application.Features.Answers.Commands.CreateAnswer
{
    public class CreateAnswerCommand : IRequest<int>
    {
        public string? AnswerText { get; set; } = null!;
        public int QuestionId { get; set; }
        public int? QuestionOptionId { get; set; }
    }
} 
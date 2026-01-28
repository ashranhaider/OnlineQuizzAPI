using MediatR;

namespace OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzById
{
    public class GetQuizzByIdQuery : IRequest<GetQuizzByIdResponse>
    {
        public int Id { get; set; }
    }
}
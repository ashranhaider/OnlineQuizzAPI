using MediatR;
using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Helpers;

namespace OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList
{
    public class GetQuizzesQueryHandler : IRequestHandler<GetQuizzesQuery, GetQuizzVM>
    {
        private readonly IQuizzRepository _quizzRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public GetQuizzesQueryHandler(IQuizzRepository quizzRepository, ILoggedInUserService loggedInUserService)
        {
            _quizzRepository = quizzRepository;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<GetQuizzVM> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
        {

            var userId = _loggedInUserService.UserId ?? throw new AuthenticationFailedException("User not authenticated.");

            var (quizzes, total) = await _quizzRepository.GetPagedQuizzesWithCount(userId, request.Page, request.Size, cancellationToken);
            
            foreach (var quizz in quizzes)
            {
                quizz.UniqueURL = QuizzUrlBuilder.Build(quizz.Id);
            }

            return new GetQuizzVM
            {
                Quizzes = quizzes,
                Total = total,
                Page = request.Page,
                PageSize = request.Size
            };
        }
    }
}

using AutoMapper;
using MediatR;
using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList
{
    public class GetQuizzesQueryHandler : IRequestHandler<GetQuizzesQuery, GetQuizzVM>
    {
        private readonly IQuizzRepository _quizzRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedInUserService _loggedInUserService;

        public GetQuizzesQueryHandler(IMapper mapper, IQuizzRepository quizzRepository, ILoggedInUserService loggedInUserService)
        {
            _mapper = mapper;
            _quizzRepository = quizzRepository;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<GetQuizzVM> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
        {
            string UserId = _loggedInUserService.UserId;

            List<Quizz> allQuizzes = await _quizzRepository.GetPagedQuizzes(UserId, request.Page, request.Size, cancellationToken);

            GetQuizzVM response = new GetQuizzVM
            {
                Quizzes = _mapper.Map<List<QuizzVM>>(allQuizzes),
                Total = await _quizzRepository.GetTotalQuizzesCount(UserId, cancellationToken),
                Page = request.Page,
                PageSize = request.Size
            };
            return response;
        }
    }
}
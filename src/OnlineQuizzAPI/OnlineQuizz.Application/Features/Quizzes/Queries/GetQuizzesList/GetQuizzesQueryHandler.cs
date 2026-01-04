using AutoMapper;
using FluentValidation.Results;
using MediatR;
using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
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

            var userId = _loggedInUserService.UserId ?? throw new AuthenticationFailedException("User not authenticated.");

            var (items, total) = await _quizzRepository.GetPagedQuizzesWithCount(userId, request.Page, request.Size, cancellationToken);

            return new GetQuizzVM
            {
                Quizzes = _mapper.Map<List<QuizzVM>>(items),
                Total = total,
                Page = request.Page,
                PageSize = request.Size
            };
        }
    }
}
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Application.Contracts.Infrastructure;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz
{
    public class CreateQuizzCommandHandler : IRequestHandler<CreateQuizzCommand, int>
    {
        private readonly IQuizzRepository _quizzRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedInUserService _loggedInUserService;
        public CreateQuizzCommandHandler(IMapper mapper, IQuizzRepository quizzRepository, ILoggedInUserService loggedInUserService)
        {
            _mapper = mapper;
            _quizzRepository = quizzRepository;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<int> Handle(CreateQuizzCommand request, CancellationToken cancellationToken)
        {
            var @quizz = _mapper.Map<Quizz>(request);

            if (!String.IsNullOrEmpty(_loggedInUserService.UserId))
            {
                quizz.OwnerUserId = _loggedInUserService.UserId;
            }

            @quizz = await _quizzRepository.AddAsync(@quizz);

            return quizz.Id;
        }
    }
}

using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Application.Contracts.Infrastructure;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Commands.UpdateQuizz
{
    public class UpdateQuizzCommandHandler : IRequestHandler<UpdateQuizzCommand, Quizz>
    {
        private readonly IQuizzRepository _quizzRepository;
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuizzCommandHandler> _logger;

        public UpdateQuizzCommandHandler(IMapper mapper, IQuizzRepository quizzRepository, ILogger<UpdateQuizzCommandHandler> logger, ILoggedInUserService loggedInUserService)
        {
            _mapper = mapper;
            _quizzRepository = quizzRepository;

            _logger = logger;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<Quizz> Handle(UpdateQuizzCommand request, CancellationToken cancellationToken)
        {            
            Quizz? quizz = await _quizzRepository.GetByIdAsync(request.Id);

            if(quizz is null)
            {
                throw new NotFoundException("Invalid quiz", request);
            }
            
            quizz.Name = request.Name;
            quizz.IsActive = request.IsActive;
            quizz.TimeAllowed = request.TimeAllowed;
            quizz.LastModifiedBy = _loggedInUserService.UserId ?? "";
            quizz.LastModifiedDate = DateTime.UtcNow;

            await _quizzRepository.UpdateAsync(@quizz);


            return quizz;
        }
    }
}

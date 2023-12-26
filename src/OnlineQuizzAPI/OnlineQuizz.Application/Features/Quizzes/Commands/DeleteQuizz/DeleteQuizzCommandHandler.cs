using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineQuizz.Application.Contracts.Infrastructure;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Features.Events.Commands.DeleteEvent;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Commands.DeleteQuizz
{
    public class DeleteQuizzCommandHandler : IRequestHandler<DeleteQuizzCommand, int>
    {
        private readonly IQuizzRepository _quizzRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteEventCommandHandler> _logger;

        public DeleteQuizzCommandHandler(IMapper mapper, IQuizzRepository quizzRepository, ILogger<DeleteEventCommandHandler> logger)
        {
            _mapper = mapper;
            _quizzRepository = quizzRepository;
            _logger = logger;
        }

        public async Task<int> Handle(DeleteQuizzCommand request, CancellationToken cancellationToken)
        {
            DeleteQuizzCommandValidator validator = new DeleteQuizzCommandValidator();
            ValidationResult validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            Quizz @quizz = await _quizzRepository.GetByIdAsync(request.Id);

            if(quizz == null)
            {
                throw new NotFoundException("Quizz", request.Id);
            }

            await _quizzRepository.DeleteAsync(@quizz);


            return quizz.Id;
        }
    }
}

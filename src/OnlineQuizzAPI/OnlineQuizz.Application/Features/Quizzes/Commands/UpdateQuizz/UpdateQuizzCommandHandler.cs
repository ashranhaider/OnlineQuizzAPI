using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineQuizz.Application.Contracts.Infrastructure;
using OnlineQuizz.Application.Contracts.Persistence;
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
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuizzCommandHandler> _logger;

        public UpdateQuizzCommandHandler(IMapper mapper, IQuizzRepository quizzRepository, ILogger<UpdateQuizzCommandHandler> logger)
        {
            _mapper = mapper;
            _quizzRepository = quizzRepository;
            _logger = logger;
        }

        public async Task<Quizz> Handle(UpdateQuizzCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateQuizzCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var @quizz = _mapper.Map<Quizz>(request);


            await _quizzRepository.UpdateAsync(@quizz);


            return quizz;
        }
    }
}

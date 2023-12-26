using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly IAsyncRepository<Quizz> _quizzRepository;
        private readonly IMapper _mapper;

        public CreateQuizzCommandHandler(IMapper mapper, IAsyncRepository<Quizz> quizzRepository)
        {
            _mapper = mapper;
            _quizzRepository = quizzRepository;
        }

        public async Task<int> Handle(CreateQuizzCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateQuizzCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var @quizz = _mapper.Map<Quizz>(request);


            @quizz = await _quizzRepository.AddAsync(@quizz);

            return quizz.Id;
        }
    }
}

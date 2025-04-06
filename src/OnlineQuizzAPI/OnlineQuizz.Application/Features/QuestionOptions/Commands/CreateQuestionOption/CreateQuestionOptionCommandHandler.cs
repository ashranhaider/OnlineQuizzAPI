using AutoMapper;
using FluentValidation;
using MediatR;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuestionOptions.Commands.CreateQuestionOption
{
    public class CreateQuestionOptionCommandHandler : IRequestHandler<CreateQuestionOptionCommand, int>
    {
        private readonly IAsyncRepository<QuestionOption> _genericRepository;
        private readonly IMapper _mapper;
        public CreateQuestionOptionCommandHandler(IAsyncRepository<QuestionOption> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateQuestionOptionCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateQuestionOptionCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            QuestionOption @questionOption = _mapper.Map<QuestionOption>(request);
            await _genericRepository.AddAsync(@questionOption);
            
            return @questionOption.Id;
        }
    }
}

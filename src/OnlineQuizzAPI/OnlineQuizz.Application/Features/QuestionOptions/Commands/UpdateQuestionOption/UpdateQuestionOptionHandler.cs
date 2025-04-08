using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Features.Quizzes.Commands.UpdateQuizz;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuestionOptions.Commands.UpdateQuestionOption
{
    public class UpdateQuestionOptionHandler : IRequestHandler<UpdateQuestionOption>
    {
        private readonly IAsyncRepository<QuestionOption> _asyncRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuizzCommandHandler> _logger;

        public UpdateQuestionOptionHandler(IMapper mapper, IAsyncRepository<QuestionOption> asyncRepository, ILogger<UpdateQuizzCommandHandler> logger)
        {
            _mapper = mapper;
            _asyncRepository = asyncRepository;
            _logger = logger;
        }

        public async Task Handle(UpdateQuestionOption request, CancellationToken cancellationToken)
        {
            var validator = new UpdateQuestionOptionValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var @questionOption = _mapper.Map<QuestionOption>(request);


            await _asyncRepository.UpdateAsync(@questionOption);

        }
    }
}

using AutoMapper;
using FluentValidation;
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

namespace OnlineQuizz.Application.Features.QuizzQuestions.Commands.Update
{
    public class UpdateQuestionHandler : IRequestHandler<UpdateQuestionCommand>
    {
        private readonly IAsyncRepository<Question> _asyncRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuizzCommandHandler> _logger;
        public UpdateQuestionHandler(IAsyncRepository<Question> asyncRepository, IMapper mapper, ILogger<UpdateQuizzCommandHandler> logger)
        {
            _asyncRepository = asyncRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateQuestionValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            Question question = _mapper.Map<Question>(request);
            await _asyncRepository.UpdateAsync(question);
            //try
            //{
            //    await _asyncRepository.UpdateAsync(question);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error updating question with id {Id}", request.Id);

            //    string innerMessage = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
            //    throw new Exception($"Error updating question with id {request.Id}. Error: {ex.Message}{Environment.NewLine}{innerMessage}");
            //}
        }
    }
}

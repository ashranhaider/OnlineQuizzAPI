using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Features.QuestionOptions.Commands.CreateQuestionOption;
using OnlineQuizz.Application.Features.QuestionOptions.Commands.UpdateQuestionOption;
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
        private readonly IQuestionOptionRepository _questionOptionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuizzCommandHandler> _logger;
        public UpdateQuestionHandler(
            IAsyncRepository<Question> asyncRepository,
            IQuestionOptionRepository questionOptionRepository,
            IAnswerRepository answerRepository,
            IMapper mapper,
            ILogger<UpdateQuizzCommandHandler> logger)
        {
            _asyncRepository = asyncRepository;
            _questionOptionRepository = questionOptionRepository;
            _answerRepository = answerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateQuestionValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var question = await _asyncRepository.GetByIdAsync(request.Id);

            if (question == null)
                throw new NotFoundException(nameof(Question), request.Id);

            // 🔹 STEP 2 — Map INTO existing object
            _mapper.Map(request, question);

            await _asyncRepository.UpdateAsync(question);

            if (request.QuestionOptions != null)
            {
                var existingOptions = await _questionOptionRepository.GetByQuestionIdAsync(request.Id);
                var existingById = existingOptions.ToDictionary(option => option.Id);

                var validationFailures = new List<ValidationFailure>();
                var incomingIds = new HashSet<int>();
                foreach (var option in request.QuestionOptions)
                {
                    if (option.Id.HasValue && option.Id.Value > 0)
                    {
                        if (!incomingIds.Add(option.Id.Value))
                        {
                            validationFailures.Add(new ValidationFailure(
                                nameof(UpdateQuestionCommand.QuestionOptions),
                                $"Duplicate QuestionOption id {option.Id.Value}."));
                            continue;
                        }

                        if (!existingById.ContainsKey(option.Id.Value))
                        {
                            validationFailures.Add(new ValidationFailure(
                                nameof(UpdateQuestionCommand.QuestionOptions),
                                $"QuestionOption id {option.Id.Value} does not belong to Question {request.Id}."));
                        }
                    }
                }

                if (validationFailures.Count > 0)
                    throw new Exceptions.ValidationException(validationFailures);

                var toDelete = existingOptions
                    .Where(option => !incomingIds.Contains(option.Id))
                    .ToList();

                if (toDelete.Count > 0)
                {
                    var answers = await _answerRepository.GetAnswersByQuestionIdAsync(request.Id);
                    var usedOptionIds = answers
                        .Where(answer => answer.QuestionOptionId.HasValue)
                        .Select(answer => answer.QuestionOptionId!.Value)
                        .ToHashSet();

                    var blockedDeletes = toDelete.Where(option => usedOptionIds.Contains(option.Id)).ToList();
                    if (blockedDeletes.Count > 0)
                    {
                        foreach (var option in blockedDeletes)
                        {
                            validationFailures.Add(new ValidationFailure(
                                nameof(UpdateQuestionCommand.QuestionOptions),
                                $"QuestionOption id {option.Id} cannot be deleted because it is referenced by answers."));
                        }
                        throw new Exceptions.ValidationException(validationFailures);
                    }

                    foreach (var option in toDelete)
                    {
                        await _questionOptionRepository.DeleteAsync(option);
                    }
                }

                var createValidator = new CreateQuestionOptionCommandValidator();
                var updateValidator = new UpdateQuestionOptionValidator();

                foreach (var option in request.QuestionOptions)
                {
                    if (option.Id.HasValue && option.Id.Value > 0)
                    {
                        var existing = existingById[option.Id.Value];
                        var updateCommand = new UpdateQuestionOption
                        {
                            Id = existing.Id,
                            OptionText = option.OptionText,
                            IsCorrect = option.IsCorrect,
                            QuestionId = request.Id
                        };

                        var updateResult = await updateValidator.ValidateAsync(updateCommand, cancellationToken);
                        if (updateResult.Errors.Count > 0)
                            throw new Exceptions.ValidationException(updateResult);

                        existing.OptionText = option.OptionText;
                        existing.IsCorrect = option.IsCorrect;
                        if (option.OptionImage != null)
                            existing.OptionImage = option.OptionImage;

                        await _questionOptionRepository.UpdateAsync(existing);
                    }
                    else
                    {
                        var createCommand = new CreateQuestionOptionCommand
                        {
                            OptionText = option.OptionText,
                            IsCorrect = option.IsCorrect,
                            OptionImage = option.OptionImage ?? new byte[0],
                            QuestionId = request.Id
                        };

                        var createResult = await createValidator.ValidateAsync(createCommand, cancellationToken);
                        if (createResult.Errors.Count > 0)
                            throw new Exceptions.ValidationException(createResult);

                        var entity = _mapper.Map<QuestionOption>(createCommand);
                        await _questionOptionRepository.AddAsync(entity);
                    }
                }
            }
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

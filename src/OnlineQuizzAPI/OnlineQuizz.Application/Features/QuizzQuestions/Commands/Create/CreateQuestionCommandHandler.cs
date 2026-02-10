using AutoMapper;
using FluentValidation;
using MediatR;
using OnlineQuizz.Application.Contracts;
using OnlineQuizz.Application.Contracts.Infrastructure;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Features.QuestionOptions.Commands.CreateQuestionOption;
using OnlineQuizz.Domain.Entities;


namespace OnlineQuizz.Application.Features.QuizzQuestions.Commands.Create
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, int>
    {
        private readonly IAsyncRepository<Question> _questionRepository;
        private readonly IAsyncRepository<QuestionOption> _questionOptionRepository;
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IMapper _mapper;

        public CreateQuestionCommandHandler(
            IMapper mapper,
            IAsyncRepository<Question> questionRepository,
            IAsyncRepository<QuestionOption> questionOptionRepository,
            ILoggedInUserService loggedInUserService)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _questionOptionRepository = questionOptionRepository;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<int> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            CreateQuestionCommandValidator validator = new CreateQuestionCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var question = _mapper.Map<Question>(request);

            question.Quizz = null;
            question.CreatedBy = _loggedInUserService.UserId;
            question = await _questionRepository.AddAsync(question);

            if (request.QuestionOptions != null && request.QuestionOptions.Count > 0)
            {
                var optionValidator = new CreateQuestionOptionCommandValidator();
                foreach (var option in request.QuestionOptions)
                {
                    option.QuestionId = question.Id;
                    var optionValidationResult = await optionValidator.ValidateAsync(option, cancellationToken);
                    if (optionValidationResult.Errors.Count > 0)
                        throw new Exceptions.ValidationException(optionValidationResult);

                    var questionOption = _mapper.Map<QuestionOption>(option);
                    await _questionOptionRepository.AddAsync(questionOption);
                }
            }

            return question.Id;
        }
    }
}

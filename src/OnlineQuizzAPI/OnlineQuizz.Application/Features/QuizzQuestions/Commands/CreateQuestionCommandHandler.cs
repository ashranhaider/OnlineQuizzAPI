using AutoMapper;
using FluentValidation;
using MediatR;
using OnlineQuizz.Application.Contracts.Infrastructure;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Domain.Entities;


namespace OnlineQuizz.Application.Features.QuizzQuestions.Commands
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, int>
    {
        private readonly IAsyncRepository<Question> _questionRepository;
        private readonly IMapper _mapper;

        public CreateQuestionCommandHandler(IMapper mapper, IAsyncRepository<Question> questionRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
        }

        public async Task<int> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            CreateQuestionCommandValidator validator = new CreateQuestionCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var question = _mapper.Map<Question>(request);


            question = await _questionRepository.AddAsync(question);

            return question.Id;
        }
    }
}

using AutoMapper;
using FluentValidation.Results;
using MediatR;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Application.Features.Answers.Commands.CreateAnswer
{
    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, int>
    {
        private readonly IAsyncRepository<Answer> _answerRepo;
        private readonly IMapper _mapper;

        public CreateAnswerCommandHandler(IAsyncRepository<Answer> answerRepo, IMapper mapper)
        {
            _answerRepo = answerRepo;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            CreateAnswerCommandValidator validator = new CreateAnswerCommandValidator();
            ValidationResult validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);


            Answer @answer = _mapper.Map<Answer>(request);

            await _answerRepo.AddAsync(answer);

            return answer.Id;
        }
    }
} 
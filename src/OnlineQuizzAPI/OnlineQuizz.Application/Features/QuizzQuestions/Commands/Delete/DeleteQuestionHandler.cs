using AutoMapper;
using FluentValidation;
using MediatR;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Application.Features.QuizzQuestions.Commands.Delete
{
    public class DeleteQuestionHandler : IRequestHandler<DeleteQuestionCommand>
    {
        private readonly IAsyncRepository<Question> _questionRepository;
        private readonly IMapper _mapper;

        public DeleteQuestionHandler(IMapper mapper, IAsyncRepository<Question> questionRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
        }

        public async Task Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteQuestionValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var question = await _questionRepository.GetByIdAsync(request.Id);

            if (question == null)
            {
                throw new NotFoundException(nameof(Question), request.Id);
            }

            await _questionRepository.DeleteAsync(question);
        }
    }
}

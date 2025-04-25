using MediatR;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Features.QuizzQuestions.Commands.Delete;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuestionOptions.Commands.DeleteQuestionOption
{
    public class DeleteQuestionOptionHandler : IRequestHandler<DeleteQuestionOptionCommand>
    {
        private readonly IAsyncRepository<QuestionOption> _questionOptionRepository;
        public DeleteQuestionOptionHandler(IAsyncRepository<QuestionOption> questionOptionRepository)
        {
            _questionOptionRepository = questionOptionRepository;
        }
        public async Task Handle(DeleteQuestionOptionCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteQuestionOptionValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            QuestionOption? questionOption = await _questionOptionRepository.GetByIdAsync(request.Id);

            if (questionOption != null)
                await _questionOptionRepository.DeleteAsync(questionOption);
        }
    }
}

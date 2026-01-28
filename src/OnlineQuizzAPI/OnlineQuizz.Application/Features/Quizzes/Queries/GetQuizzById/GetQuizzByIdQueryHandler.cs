using AutoMapper;
using MediatR;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Exceptions;
using OnlineQuizz.Application.Features.QuizzQuestions.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzById
{
    public class GetQuizzByIdQueryHandler : IRequestHandler<GetQuizzByIdQuery, GetQuizzByIdResponse>
    {
        private readonly IQuizzRepository _quizzRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public GetQuizzByIdQueryHandler(IMapper mapper, IQuizzRepository quizzRepository, IQuestionRepository questionRepository)
        {
            _mapper = mapper;
            _quizzRepository = quizzRepository;
            _questionRepository = questionRepository;
        }

        public async Task<GetQuizzByIdResponse> Handle(GetQuizzByIdQuery request, CancellationToken cancellationToken)
        {
            var quizz = await _quizzRepository.GetByIdAsync(request.Id);
            if (quizz == null)
                throw new NotFoundException("Quizz", request.Id);

            var questions = await _questionRepository.GetQuestionsByQuizzIdAsync(request.Id);

            return new GetQuizzByIdResponse
            {
                Id = quizz.Id,
                Name = quizz.Name,
                UniqueURL = quizz.UniqueURL,
                IsActive = quizz.IsActive,
                Questions = _mapper.Map<List<GetQuestionsVM>>(questions)
            };
        }
    }
}
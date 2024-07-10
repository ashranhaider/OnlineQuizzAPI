using AutoMapper;
using MediatR;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList;
using OnlineQuizz.Domain.Entities;
using OnlineQuizz.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.QuizzQuestions.Queries
{
    public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, List<GetQuestionsVM>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public GetQuestionsQueryHandler (IMapper mapper, IQuestionRepository questionRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
        }

        public async Task<List<GetQuestionsVM>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            List<Question> questions = (await _questionRepository.GetQuestionsByQuizzIdAsync(request.QuizzId));

            return _mapper.Map<List<GetQuestionsVM>>(questions);
        }
    }
}

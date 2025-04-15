using AutoMapper;
using MediatR;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Application.Features.Answers.Queries.GetAnswersByQuestionId;
using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Application.Features.Answers.Queries.GetAnswersByQuestionId
{
    public class GetAnswersByQuestionIdQueryHandler : IRequestHandler<GetAnswersByQuestionIdQuery, List<GetAnswersByQuestionIdResponse>>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public GetAnswersByQuestionIdQueryHandler(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAnswersByQuestionIdResponse>> Handle(GetAnswersByQuestionIdQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Answer> answers = await _answerRepository.GetAnswersByQuestionIdAsync(request.QuestionId);
            
            return _mapper.Map<List<GetAnswersByQuestionIdResponse>>(answers);
        }
    }
} 
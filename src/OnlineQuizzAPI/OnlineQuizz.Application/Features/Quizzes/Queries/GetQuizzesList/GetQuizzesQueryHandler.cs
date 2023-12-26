using AutoMapper;
using MediatR;
using OnlineQuizz.Application.Contracts.Persistence;
using OnlineQuizz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList
{
    public class GetQuizzesQueryHandler : IRequestHandler<GetQuizzesQuery, List<GetQuizzVM>>
    {
        private readonly IAsyncRepository<Quizz> _quizzRepository;
        private readonly IMapper _mapper;

        public GetQuizzesQueryHandler(IMapper mapper, IAsyncRepository<Quizz> quizzRepository)
        {
            _mapper = mapper;
            _quizzRepository = quizzRepository;
        }

        public async Task<List<GetQuizzVM>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
        {
            var allQuizzes = (await _quizzRepository.ListAllAsync()).OrderBy(x => x.CreatedDate);
            return _mapper.Map<List<GetQuizzVM>>(allQuizzes);
        }
    }
}
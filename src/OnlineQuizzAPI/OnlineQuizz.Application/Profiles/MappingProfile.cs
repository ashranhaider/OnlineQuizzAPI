using AutoMapper;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using OnlineQuizz.Application.Features.Quizzes.Commands.UpdateQuizz;
using OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList;
using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Quizz, GetQuizzVM>().ReverseMap();
            CreateMap<Quizz, CreateQuizzCommand>().ReverseMap();
            CreateMap<Quizz, UpdateQuizzCommand>().ReverseMap();
        }
    }
}

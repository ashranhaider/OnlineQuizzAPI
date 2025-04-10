using AutoMapper;
using OnlineQuizz.Application.Features.QuestionOptions.Commands.CreateQuestionOption;
using OnlineQuizz.Application.Features.QuestionOptions.Commands.UpdateQuestionOption;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using OnlineQuizz.Application.Features.Quizzes.Commands.UpdateQuizz;
using OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList;
using OnlineQuizz.Application.Features.QuizzQuestions.Commands.Create;
using OnlineQuizz.Application.Features.QuizzQuestions.Queries;
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

            CreateMap<Question, GetQuestionsVM>()
                .ForMember(dest => dest.QuestionType, opt => opt.MapFrom(src => src.QuestionType.ToString()))
                .ReverseMap();
            CreateMap<Question, CreateQuestionCommand>().ReverseMap();


            CreateMap<QuestionOption, CreateQuestionOptionCommand>().ReverseMap();
            CreateMap<QuestionOption, QuestionOptionsVM>().ReverseMap();
            CreateMap<QuestionOption, UpdateQuestionOption>().ReverseMap();
        }
    }
}

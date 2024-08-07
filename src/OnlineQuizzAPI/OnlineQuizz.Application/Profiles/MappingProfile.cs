﻿using AutoMapper;
using OnlineQuizz.Application.Features.Quizzes.Commands.CreateQuizz;
using OnlineQuizz.Application.Features.Quizzes.Commands.UpdateQuizz;
using OnlineQuizz.Application.Features.Quizzes.Queries.GetQuizzesList;
using OnlineQuizz.Application.Features.QuizzQuestions.Commands;
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
        }
    }
}

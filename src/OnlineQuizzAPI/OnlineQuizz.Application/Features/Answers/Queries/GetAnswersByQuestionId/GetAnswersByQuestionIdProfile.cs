using AutoMapper;
using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Application.Features.Answers.Queries.GetAnswersByQuestionId
{
    public class GetAnswersByQuestionIdProfile : Profile
    {
        public GetAnswersByQuestionIdProfile()
        {
            CreateMap<Answer, GetAnswersByQuestionIdResponse>()
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.Question != null ? src.Question.QuestionText : null))
                .ForMember(dest => dest.SelectedQuestionOption, opt => opt.MapFrom(src => src.QuestionOption != null ? src.QuestionOption.OptionText : null));

        }
    }
} 
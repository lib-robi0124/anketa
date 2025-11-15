using AutoMapper;
using GlasAnketa.Domain.Models;
using GlasAnketa.ViewModels.Models;

namespace GlasAnketa.Services.AutoMappers
{
    public class AnswerMappingProfile : Profile
    {
        public AnswerMappingProfile()
        { 
            CreateMap<Answer, AnswerVM>().ReverseMap();

            CreateMap<AnswerSummary, AnswerSummaryVM>()
                .ForMember(dest => dest.ResponseCount, opt => opt.MapFrom(src => src.TotalResponses));

            CreateMap<AnswerSummaryVM, AnswerSummary>()
                .ForMember(dest => dest.TotalResponses, opt => opt.MapFrom(src => src.ResponseCount));

            CreateMap<Answer, ResultsVM>().ReverseMap();
            CreateMap<Answer, UserFormAnswersVM>()
            .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.Question.Text))
            .ForMember(dest => dest.QuestionType, opt => opt.MapFrom(src => src.Question.QuestionType.Name));
        }
    }
}

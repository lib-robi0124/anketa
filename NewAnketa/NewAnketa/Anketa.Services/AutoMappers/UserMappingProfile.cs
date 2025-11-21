using Anketa.Domain.Entities;
using Anketa.Domain.Enums;
using Anketa.ViewModels.Models;
using AutoMapper;

namespace Anketa.Services.AutoMappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserVM>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ReverseMap();

            CreateMap<UserCredentialsVM, User>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();

            CreateMap<RegisterUserVM, User>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Sector, opt => opt.MapFrom(src => src.Sector))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Line, opt => opt.MapFrom(src => src.Line))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();
        }
    }
}

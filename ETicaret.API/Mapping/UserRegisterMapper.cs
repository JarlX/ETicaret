namespace ETicaret.API.Mapping;

using AutoMapper;
using Entity;
using Entity.DTO;

public class UserRegisterMapper : Profile
{
    public UserRegisterMapper()
    {
        CreateMap<User, UserDTORequest>()
            .ForMember(dest => dest.FirstName, opt =>
            {
                opt.MapFrom(src => src.FirstName);
            })
            .ForMember(dest => dest.LastName, opt =>
            {
                opt.MapFrom(src => src.LastName);
            })
            .ForMember(dest => dest.UserName, opt =>
            {
                opt.MapFrom(src => src.UserName);
            })
            .ForMember(dest => dest.Password, opt =>
            {
                opt.MapFrom(src => src.Password);
            })
            .ForMember(dest => dest.PhoneNumber, opt =>
            {
                opt.MapFrom(src => src.PhoneNumber);
            })
            .ForMember(dest => dest.Email, opt =>
            {
                opt.MapFrom(src => src.Email);
            })
            .ForMember(dest => dest.Address, opt =>
            {
                opt.MapFrom(src => src.Address);
            }).ReverseMap();
    }
}
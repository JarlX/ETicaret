namespace ETicaret.API.Mapping;

using AutoMapper;
using Entity;
using Entity.DTO.Category;

public class CategoryResponseMapper : Profile
{
    public CategoryResponseMapper()
    {
        CreateMap<Category, CategoryDTOResponse>()
            .ForMember(dest => dest.CategoryName,
                opt =>
                {
                    opt.MapFrom(src => src.CategoryName);
                })
            .ForMember(dest => dest.Guid, opt =>
            {
                opt.MapFrom(src => src.GUID);
            }).ReverseMap();
    }
}
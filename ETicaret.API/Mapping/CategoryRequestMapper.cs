namespace ETicaret.API.Mapping;

using AutoMapper;
using Entity;
using Entity.DTO.Category;

public class CategoryRequestMapper : Profile
{
    public CategoryRequestMapper()
    {
        CreateMap<Category, CategoryDTORequest>().
            ForMember(dest => dest.CategoryName, opt =>
        {
            opt.MapFrom(src => src.CategoryName);
        }).ReverseMap();
    }
}
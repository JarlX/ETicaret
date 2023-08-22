using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;

using System.Net;
using AutoMapper;
using Business.Abstract;
using Entity;
using Entity.DTO.Category;
using Entity.Result;
using FluentValidation.Results;
using Helper.CustomException;
using Validation.FluentValidation;

[ApiController]
[Route("/ETicaret/[action]")]

public class CategoryController : Controller
{

	private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpPost("/AddCategory")]
    [ProducesResponseType(typeof(Sonuc<CategoryDTOResponse>),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddCategory(CategoryDTORequest categoryDtoRequest)
    {
        CategoryValidator categoryValidator = new CategoryValidator();
        
        if (categoryValidator.Validate(categoryDtoRequest).IsValid)
        {

            Category category = _mapper.Map<Category>(categoryDtoRequest);
        
            await _categoryService.AddSync(category);
            
            CategoryDTOResponse categoryDtoResponse = _mapper.Map<CategoryDTOResponse>(category);
            
            return Ok(Sonuc<CategoryDTOResponse>.SuccessWithData(categoryDtoResponse));
        }
        else
        {
            List<string> validationMessages = new List<string>();

            foreach (var validationFailure in categoryValidator.Validate(categoryDtoRequest).Errors)
            {
                validationMessages.Add(validationFailure.ErrorMessage);
            }

            throw new FieldValidationException(validationMessages);
        }
        
        
    }

    [HttpGet("/Categories")]
    [ProducesResponseType(typeof(Sonuc<List<CategoryDTOResponse>>),(int)HttpStatusCode.OK)]

    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetAllAsync();

        if (categories != null)
        {
            
            List<CategoryDTOResponse> categoryDtoResponseList = new List<CategoryDTOResponse>();
            foreach (var category in categories)
            {
                // categoryDtoResponseList.Add(new CategoryDTOResponse()
                // {
                //     Guid = category.GUID,
                //     CategoryName = category.CategoryName
                // });   
                
                categoryDtoResponseList.Add(_mapper.Map<CategoryDTOResponse>(category));
            }

            return Ok(Sonuc<List<CategoryDTOResponse>>.SuccessWithData(categoryDtoResponseList));
        }
        else
        {
            return NotFound(Sonuc<List<CategoryDTOResponse>>.SuccessDataNotFound()); 
        }

    }

    [HttpGet("/Category/{id:int}")]
    [ProducesResponseType(typeof(Sonuc<CategoryDTOResponse>),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCategoryByID(int id)
    
    {
        var category = await _categoryService.GetAsync(q => q.ID == id);

        if (category != null)
        {
            CategoryDTOResponse categoryDtoResponse = new CategoryDTOResponse()
            {
                CategoryName = category.CategoryName,
                Guid = category.GUID
            };

            return Ok(Sonuc<CategoryDTOResponse>.SuccessWithData(categoryDtoResponse));
        }
        else
        {
            return NotFound(Sonuc<CategoryDTOResponse>.SuccessDataNotFound());
        }
    }

    [HttpGet("/Category/{guid:guid}")]
    [ProducesResponseType(typeof(Sonuc<CategoryDTOResponse>), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> GetCategoryByGUID(Guid guid)
    {
        var category = await _categoryService.GetAsync(q => q.GUID == guid);

        if (category != null)
        {
            CategoryDTOResponse categoryDtoResponse = _mapper.Map<CategoryDTOResponse>(category);

            return Ok(Sonuc<CategoryDTOResponse>.SuccessWithData(categoryDtoResponse));
        }
        else
        {
            return NotFound(Sonuc<CategoryDTOResponse>.SuccessDataNotFound());
        }
    }

    [HttpPut("/UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(CategoryUpdateDTORequest categoryUpdateDtoRequest)
    {
        Category category = await _categoryService.GetAsync(q => q.GUID == categoryUpdateDtoRequest.Guid);

        category.CategoryName = categoryUpdateDtoRequest.CategoryName;
        category.IsActive = categoryUpdateDtoRequest.IsActive != null
            ? categoryUpdateDtoRequest.IsActive
            : category.IsActive;

        await _categoryService.UpdateAsync(category);

        return Ok(Sonuc<CategoryDTOResponse>.SuccessWithoutData(message:"Kategori Başarıyla Güncellendi"));
    }
}
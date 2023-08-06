using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;

using System.Net;
using Business.Abstract;
using ETicaretAPI.Entity;
using ETicaretAPI.Entity.DTO.Category;
using ETicaretAPI.Entity.Result;

[ApiController]
[Route("/ETicaret/[action]")]

public class CategoryController : Controller
{

	private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("/AddCategory")]
    [ProducesResponseType(typeof(Sonuc<CategoryDTOResponse>),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddCategory(CategoryDTORequest categoryDtoRequest)
    {
        Category category = new Category()
        {
            CategoryName = categoryDtoRequest.CategoryName
        };
        
        await _categoryService.AddSync(category);

        CategoryDTOResponse categoryDtoResponse = new CategoryDTOResponse()
        {
            Guid = categoryDtoRequest.Guid,
            CategoryName = categoryDtoRequest.CategoryName
        };
        return Ok(Sonuc<CategoryDTOResponse>.SuccessWithData(categoryDtoResponse));
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
                categoryDtoResponseList.Add(new CategoryDTOResponse()
                {
                    Guid = category.GUID,
                    CategoryName = category.CategoryName
                });   
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

    [HttpPut("/UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(CategoryDTORequest categoryDtoRequest)
    {
        Category category = await _categoryService.GetAsync(q => q.GUID == categoryDtoRequest.Guid);

        category.CategoryName = categoryDtoRequest.CategoryName;

        await _categoryService.UpdateAsync(category);

        return Ok(Sonuc<CategoryDTOResponse>.SuccessWithoutData(message:"Kategori Başarıyla Güncellendi"));
    }
}
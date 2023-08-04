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
        return Ok(new Sonuc<CategoryDTOResponse>(categoryDtoResponse,"İşlem Başarılı",(int)HttpStatusCode.OK,null));
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

            return Ok(new Sonuc<List<CategoryDTOResponse>>(categoryDtoResponseList,"İşlem Başarılı",(int)HttpStatusCode.OK,null));
        }
        else
        {
            return NotFound(new Sonuc<List<CategoryDTOResponse>>(null, "Sonuç Bulunamadı", (int)HttpStatusCode.NotFound,
                new HataBilgisi()
                {
                    Hata = null,
                    HataAciklama = "Sonuç Bulunamadı"
                }));
        }

    }

    [HttpGet("/Category/{guid}")]
    [ProducesResponseType(typeof(Sonuc<CategoryDTOResponse>),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCategoryByID(Guid guid)
    
    {
        var category = await _categoryService.GetAsync(q => q.GUID == guid);

        if (category != null)
        {
            CategoryDTOResponse categoryDtoResponse = new CategoryDTOResponse()
            {
                CategoryName = category.CategoryName,
                Guid = category.GUID
            };

            return Ok(new Sonuc<CategoryDTOResponse>(categoryDtoResponse, "İşlem Başarılı", (int)HttpStatusCode.OK,
                null));
        }
        else
        {
            return NotFound(new Sonuc<CategoryDTOResponse>(null, "Sonuç Bulunamadı", (int)HttpStatusCode.NotFound,new HataBilgisi()
            {
                Hata = null,
                HataAciklama = "Sonuç Bulunamadı"
            }));
        }
    }
}
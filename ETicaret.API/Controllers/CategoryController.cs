using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;

using Business.Abstract;
using ETicaretAPI.Entity;

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
    public async Task<IActionResult> AddCategory(Category category)
    {
        await _categoryService.AddSync(category);
        return Ok(category);
    }

    [HttpGet("/Categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetAllAsync();

        return Ok(categories.ToList());
    }

    [HttpGet("/Category/{id}")]
    public async Task<IActionResult> GetCategoryByID(int id)
    {
        var category = await _categoryService.GetAsync(q => q.ID == id);

        return category != null ? Ok(category) : Ok("İşlem Başarısız");
    }
}
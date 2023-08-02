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
        return Ok("İşlem Başarılı");
    }
}
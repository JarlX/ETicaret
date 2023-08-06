namespace ETicaret.API.Controllers;

using System.Text;
using Business.Abstract;
using ETicaretAPI.Entity.DTO.Login;
using ETicaretAPI.Entity.Result;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[action]")]
public class LoginController
{
    private readonly IUserService _userService;

    private readonly IConfiguration _configuration;

    public LoginController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("/Login")]
    public async Task<IActionResult> LoginAsync(LoginDTORequest loginDtoRequest)
    {
        var user = await _userService.GetAsync(q =>
            q.UserName == loginDtoRequest.UserName && q.Password == loginDtoRequest.Password);

        if (user == null)
        {
            return NotFound(Sonuc<LoginDTOResponse>.AuthenticationError());
        }
        else
        {
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:JWTKey"));
        }
    }
}
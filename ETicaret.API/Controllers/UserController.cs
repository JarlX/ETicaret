using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;

using System.Net;
using Business.Abstract;
using ETicaretAPI.Entity;
using ETicaretAPI.Entity.DTO;
using ETicaretAPI.Entity.Result;

[ApiController]
[Route("ETicaret/[action]")]
public class UserController : Controller
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/AddUser")]
    [ProducesResponseType(typeof(Sonuc<UserDTOResponse>),(int)HttpStatusCode.OK)]
     public async Task<IActionResult> AddUser(UserDTORequest userDtoRequest)
     {
         User user = new User()
         {
             FirstName = userDtoRequest.FirstName,
             LastName = userDtoRequest.LastName,
             UserName = userDtoRequest.UserName,
             Password = userDtoRequest.Password,
             Email = userDtoRequest.Email,
             PhoneNumber = userDtoRequest.PhoneNumber,
             Address = userDtoRequest.Address,
         };

         await _userService.AddSync(user);

         UserDTOResponse userDtoResponse = new UserDTOResponse()
         {
             Guid = user.GUID,
             FirstName = user.FirstName,
             LastName = user.LastName,
             UserName = user.UserName,
             Password = user.Password,
             Email = user.Email,
             PhoneNumber = user.PhoneNumber,
             Address = user.Address,
         };

         return Ok(Sonuc<UserDTOResponse>.SuccessWithData(userDtoResponse));
     }

     [HttpGet("/User/{guid}")]
     [ProducesResponseType(typeof(Sonuc<UserDTOResponse>),(int)HttpStatusCode.OK)]
     public async Task<IActionResult> GetUser(Guid guid)
     {
        var user =  await _userService.GetAsync(q => q.GUID == guid);

        if (user != null)
        {
            UserDTOResponse userDtoResponse = new()
            {
                Guid = user.GUID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
            };

            return Ok(Sonuc<UserDTOResponse>.SuccessWithData(userDtoResponse));
        }
        else
        {
            return NotFound(Sonuc<UserDTOResponse>.SuccessDataNotFound());
        }
        
     }
}
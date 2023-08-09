using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;

using System.Net;
using AutoMapper;
using Business.Abstract;
using Entity;
using Entity.DTO;
using Entity.Result;
using Helper.CustomException;
using Mapping;
using Validation.FluentValidation;

[ApiController]
[Route("ETicaret/[action]")]
public class UserController : Controller
{

    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("/AddUser")]
    [ProducesResponseType(typeof(Sonuc<UserDTOResponse>),(int)HttpStatusCode.OK)]
     public async Task<IActionResult> AddUser(UserDTORequest userDtoRequest)
     {
         UserRegisterValidator userRegisterValidator = new UserRegisterValidator();
         if (userRegisterValidator.Validate(userDtoRequest).IsValid)
         {
             User user = _mapper.Map<User>(userDtoRequest);

             await _userService.AddSync(user);

             UserDTOResponse userDtoResponse = _mapper.Map<UserDTOResponse>(user);

             return Ok(Sonuc<UserDTOResponse>.SuccessWithData(userDtoResponse));
         }

         else
         {
             List<string> validatorString = new List<string>();

             foreach (var validationFailure in userRegisterValidator.Validate(userDtoRequest).Errors)
             {
                 validatorString.Add(validationFailure.ErrorMessage);
             }

             // return BadRequest(Sonuc<UserDTOResponse>.FieldValidationError(validatorString));

             throw new FieldValidationException(validatorString);
         }
         
     }

     [HttpGet("/User/{guid}")]
     [ProducesResponseType(typeof(Sonuc<UserDTOResponse>),(int)HttpStatusCode.OK)]
     public async Task<IActionResult> GetUser(Guid guid)
     {
        var user =  await _userService.GetAsync(q => q.GUID == guid);

        if (user != null)
        {
            UserDTOResponse userDtoResponse = _mapper.Map<UserDTOResponse>(user);

            return Ok(Sonuc<UserDTOResponse>.SuccessWithData(userDtoResponse));
        }
        else
        {
            return NotFound(Sonuc<UserDTOResponse>.SuccessDataNotFound());
        }
        
     }
}
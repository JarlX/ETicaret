using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;

using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Business.Abstract;
using Entity.DTO.Login;
using Entity.Result;
using Helper.CustomException;
using Microsoft.IdentityModel.Tokens;
using Validation.FluentValidation;

[ApiController]
[Route("[action]")]
public class LoginController : Controller
{
    private readonly IUserService _userService;

    private readonly IConfiguration _configuration;

    private readonly IMapper _mapper;

    public LoginController(IUserService userService, IConfiguration configuration, IMapper mapper)
    {
        _userService = userService;
        _configuration = configuration;
        _mapper = mapper;
    }

    [HttpPost("/Login")]
    [ProducesResponseType(typeof(Sonuc<LoginDTOResponse>),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> LoginAsync(LoginDTORequest loginDtoRequest)
    {
        LoginValidator loginValidator = new LoginValidator();

        if (loginValidator.Validate(loginDtoRequest).IsValid)
        {
            var user = await _userService.GetAsync(q =>
                q.UserName == loginDtoRequest.UserName && q.Password == loginDtoRequest.Password);

        
            // JWT TOKEN YAZILDI
        
            if (user != null)
            {
                var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:JWTKey"));

                var claims = new List<Claim>();
                claims.Add(new Claim("KullanıcıAdi",user.UserName));
                claims.Add(new Claim("KullanıcıID",user.ID.ToString()));

                var jwt = new JwtSecurityToken(
                    expires: DateTime.Now.AddDays(30),
                    claims: claims,
                    issuer: "http://salihcancakar.com",
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                );

                var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                LoginDTOResponse loginDtoResponse = _mapper.Map<LoginDTOResponse>(token);

                return Ok(Sonuc<LoginDTOResponse>.SuccessWithData(loginDtoResponse));

            }
            else
            {
                return NotFound(Sonuc<LoginDTORequest>.AuthenticationError());
            }
        }
        else
        {
            List<string> validationMessages = new List<string>();

            foreach (var validationFailure in loginValidator.Validate(loginDtoRequest).Errors)
            {
                validationMessages.Add(validationFailure.ErrorMessage);
            }

            throw new FieldValidationException(validationMessages);
            
        }
        
    }
}
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;

using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Aspects;
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
    

    public LoginController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("/Login")]
    [ValidationFilter(typeof(LoginValidator))]
    [ProducesResponseType(typeof(Sonuc<LoginDTOResponse>),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> LoginAsync(LoginDTORequest loginDtoRequest)
    {

        
            var user = await _userService.GetAsync(q =>
                q.UserName == loginDtoRequest.UserName && q.Password == loginDtoRequest.Password);

        
            // JWT TOKEN YAZILDI
        
            if (user == null)
            {
                return NotFound(Sonuc<LoginDTORequest>.AuthenticationError());

            }
            
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

            LoginDTOResponse loginDtoResponse = new LoginDTOResponse()
            {
                FullName =  user.FirstName + " " +user.LastName,
                UserId = user.ID,
                Token = token,
                Email = user.Email,
                Address = user.Address
                
                
            };

            return Ok(Sonuc<LoginDTOResponse>.SuccessWithData(loginDtoResponse));
            
    }
}
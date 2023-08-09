namespace ETicaret.API.Middleware
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Helper.CustomException;
    using Helper.Globals;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class APIAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IOptionsMonitor<JWTExceptURLList> _jwtExceptUrlList; // Anlık değişiklikleri build etmeden kullanabilmek için OptionsMonitor kullanıldı.

        public APIAuthorizationMiddleware(RequestDelegate next, IConfiguration configuration, IOptionsMonitor<JWTExceptURLList> jwtExceptUrlList)
        {
            _next = next;
            _configuration = configuration;
            _jwtExceptUrlList = jwtExceptUrlList;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            if (!_jwtExceptUrlList.CurrentValue.URLList.Contains(httpContext.Request.Path))
            {
                var jwthandler = new JwtSecurityTokenHandler();
                string authHeader = httpContext.Request.Headers["Authorization"];

                if (authHeader != null)
                {
                    var token = authHeader.Replace("Bearer ", "");
                    var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:JWTKey"));

                    jwthandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    },out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;

                    if (jwtToken.ValidTo < DateTime.Now)
                    {
                        throw new TokenException();
                    }
                }
                else
                {
                    throw new TokenNotFoundException();

                }
            }
            
            await _next(httpContext);

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class APIAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder APIAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APIAuthorizationMiddleware>();
        }
    }
}


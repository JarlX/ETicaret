﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ETicaret.API.Middleware
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class APIAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public APIAuthorizationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var jwthandler = new JwtSecurityTokenHandler();
            string authHeader = httpContext.Request.Headers.FirstOrDefault(q => q.Key.Equals("Authorization")).ToString();

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
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
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

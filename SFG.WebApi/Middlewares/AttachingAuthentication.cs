using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace SFG.WebApi.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AttachingAuthentication
    {
        private readonly IDatabase databaseRedis;
        private readonly RequestDelegate _next;

        public AttachingAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var accessToken = httpContext.Request.Cookies["accessToken"];

            if(accessToken != null)
            {
                httpContext.Request.Headers.Append("Authorization", "Bearer " + accessToken);
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AttachingAuthenticationExtensions
    {
        public static IApplicationBuilder UseAttachingAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AttachingAuthentication>();
        }
    }
}


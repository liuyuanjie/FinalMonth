using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace FinalMonth.Api.CustomMiddlewares
{
    public class CustomJwtMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomJwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Substring("Bearer ".Length).Trim();

            if (token != null)
                 attachLoginUserToContext(context, token);

            await _next(context);
        }

        private void attachLoginUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                var loginName = jwtSecurityToken.Claims.ToList().First(x => x.Type == ClaimTypes.Name).Value;

                // attach account to context on successful jwt validation
                context.Items["loginName"] = loginName;
            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
    }

    public static class CustomJwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomJwtMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomJwtMiddleware>();
        }
    }
}

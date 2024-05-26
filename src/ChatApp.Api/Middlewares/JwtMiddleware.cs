using Infrastructure.Security.Jwt;
using Microsoft.Extensions.Options;

namespace ChatApp.Api.Middlewares
{
    public class JwtMiddleware(RequestDelegate _next, IOptions<JwtSettings> jwtSettings)
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(token))
                AttachUserToContext(context, token.Replace("Bearer ", string.Empty));

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {

                //var tokenHandler = new JwtSecurityTokenHandler();
                //var jwtToken = tokenHandler.ReadJwtToken(token);
                //var userId = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                //context.Items["userId"] = userId;
            }
            catch (Exception exc)
            {
                throw;
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}

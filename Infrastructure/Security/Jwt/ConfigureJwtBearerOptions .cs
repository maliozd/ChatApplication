using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Security.Jwt
{
    public class ConfigureJwtBearerOptions(IOptions<JwtSettings> jwtSettings) : IPostConfigureOptions<JwtBearerOptions>
    {
        readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public void PostConfigure(string name, JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                LifetimeValidator = (notBefore, expires, token, parameters) =>
                {
                    if (expires != null)
                    {
                        return expires > DateTime.UtcNow;
                    }
                    return false;
                },
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            };

            options.Events = new JwtBearerEvents();
            var originalOnMessageReceived = options.Events.OnMessageReceived;
            options.Events.OnMessageReceived = async context =>
            {
                var path = context.HttpContext.Request.Path;
                await originalOnMessageReceived(context);

                if (string.IsNullOrEmpty(context.Token))
                {
                    var accessToken = context.Request.Headers.Authorization; ;
                    if (string.IsNullOrEmpty(accessToken))
                    {
                        accessToken = accessToken.ToString().Replace("Bearer ", string.Empty);
                        accessToken = context.Request.Query["access_token"].ToString();
                        string connectionId = context.Request.Query["id"].ToString();
                    }

                    if (!string.IsNullOrEmpty(accessToken) &&
                        path.StartsWithSegments("/hubs"))
                    {
                        context.Token = accessToken;
                    }

                }
            };
            options.Events.OnAuthenticationFailed = context =>
            {
                var exceptionType = context.Exception.GetType();
                if (exceptionType.Equals(typeof(SecurityTokenExpiredException)) || exceptionType.Equals(typeof(SecurityTokenInvalidLifetimeException)))
                {
                    //context.Response.StatusCode = (int)HttpStatusCode.Unaouthroized;
                    //context.Response.ContentType = "application/json";
                    //var result = JsonSerializer.Serialize(new
                    //{
                    //    message = "Token has expired",
                    //});
                }
                return Task.CompletedTask;
            };
        }
    }
}
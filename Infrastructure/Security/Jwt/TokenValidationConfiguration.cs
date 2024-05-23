using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Security.Jwt
{
    public class TokenValidationConfiguration(IOptions<JwtSettings> jwtConfiguration) : IConfigureNamedOptions<JwtBearerOptions>
    {
        readonly JwtSettings _jwtConfiguration = jwtConfiguration.Value;
        public void Configure(string? name, JwtBearerOptions options) => Configure(options);

        public void Configure(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtConfiguration.Issuer,
                ValidAudience = _jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(_jwtConfiguration.Secret)),
            };
        }
    }
}

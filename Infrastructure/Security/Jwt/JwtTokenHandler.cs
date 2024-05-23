using ChatApp.Application.Common.Dtos;
using ChatApp.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Security.Jwt
{
    public class JwtTokenHandler(IOptions<JwtSettings> jwtConfig) : ITokenHandler
    {
        readonly JwtSettings _jwtConfig = jwtConfig.Value;

        public Task<string> GenerateToken(string id, string username, string email)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,id),
                new(ClaimTypes.Email,email),
                new(ClaimTypes.Name,username),
            };

            var token = new JwtSecurityToken(
           _jwtConfig.Issuer,
           _jwtConfig.Audience,
           claims,
           expires: DateTime.UtcNow.AddMinutes(_jwtConfig.TokenExpirationMinute),
           notBefore: DateTime.UtcNow, // token üretildiği andan ne kadar süre sonra devreye girsin
           signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }

        public async Task<TokenInfo> ReadToken(string token)
        {
            return await Task.Run(() =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(token);

                var userIdClaim = decodedToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var usernameClaim = decodedToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                var emailClaim = decodedToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

                _ = int.TryParse(userIdClaim, out int userId);

                return new TokenInfo(userId, usernameClaim, emailClaim);
            });
        }



    }
}

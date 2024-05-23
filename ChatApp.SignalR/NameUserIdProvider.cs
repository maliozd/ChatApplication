using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChatApp.SignalR
{
    public class NameUserIdProvider() : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            try
            {
                var httpContext = connection.GetHttpContext();
                string token = httpContext.Request.Headers.Authorization;
                token = token.Replace("Bearer ", string.Empty);

                var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

                var userIdClaim = decodedToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                return userIdClaim;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}

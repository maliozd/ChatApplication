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
                var token = httpContext.Request.Query["access_token"].ToString();

                if (string.IsNullOrEmpty(token))
                    token = httpContext.Request.Headers.Authorization;
                if (string.IsNullOrEmpty(token))
                    return string.Empty;

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

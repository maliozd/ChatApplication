
using ChatApp.Application.Common.Dtos;

namespace ChatApp.Application.Common.Interfaces
{
    public interface ITokenHandler
    {
        Task<string> GenerateToken(string id, string username, string email);
        Task<TokenInfo> ReadToken(string token);
    }
}

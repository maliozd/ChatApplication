namespace ChatApp.Application.Common.Interfaces
{
    public interface ITokenHandler
    {
        Task<string> GenerateToken(string id, string username, string email);
    }
}

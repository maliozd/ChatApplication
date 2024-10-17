namespace ChatApp.Application.Common.Interfaces
{
    public interface IConnectionPool
    {
        void AddConnection(int userId, string connectionId);
        void RemoveConnection(int userId, string connectionId);
        IEnumerable<string> GetConnectionIds(int userId);
    }
}

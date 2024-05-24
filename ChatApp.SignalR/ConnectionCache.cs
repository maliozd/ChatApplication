using ChatApp.Application.Common.Interfaces;
using System.Collections.Concurrent;

namespace ChatApp.SignalR
{
    public class ConnectionCache : IConnectionCache

    {
        private static readonly ConcurrentDictionary<int, HashSet<string>> _userConnections = new ConcurrentDictionary<int, HashSet<string>>();

        public void AddConnection(int userId, string connectionId)
        {
            var connections = _userConnections.GetOrAdd(userId, _ => new HashSet<string>());
            lock (connections)
            {
                connections.Add(connectionId);
            }
        }

        public void RemoveConnection(int userId, string connectionId)
        {
            if (_userConnections.TryGetValue(userId, out var connections))
            {
                lock (connections)
                {
                    connections.Remove(connectionId);
                    if (connections.Count == 0)
                    {
                        _userConnections.TryRemove(userId, out _);
                    }
                }
            }
        }

        public IEnumerable<string> GetConnectionIds(int userId)
        {
            if (_userConnections.TryGetValue(userId, out var connections))
            {
                return connections.ToList();
            }
            return Enumerable.Empty<string>();
        }

    }
}

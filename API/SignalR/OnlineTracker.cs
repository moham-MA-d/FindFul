using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SignalR
{
    public class OnlineTracker
    {
        // key: username
        // value: list of connectionId provided by signalR from different possible devices.
        private static readonly Dictionary<string, List<string>> OnlineUsers =
            new Dictionary<string, List<string>>();

        
        public Task UserConnected(string username, string connectionId)
        {
            var isOnline = false;
            
            // Warning: Dictionary is not a safe place to store resources because of concurrency issues.
            //  so we can use lock keyword to lock the dictionary until end of its job per user.
            lock (OnlineUsers)
            {
                if (OnlineUsers.ContainsKey(username))
                    OnlineUsers[username].Add(connectionId);
                else
                {
                    OnlineUsers.Add(username, new List<string> { connectionId });
                    isOnline = true;
                }
            }

            return Task.CompletedTask;
            //return Task.FromResult(isOnline);
        }

        public Task UserDisconnected(string username, string connectionId)
        {
            var isOffline = false;
            lock (OnlineUsers)
            {
                if (!OnlineUsers.ContainsKey(username)) return Task.CompletedTask;// Task.FromResult(isOffline);

                OnlineUsers[username].Remove(connectionId);
                if (OnlineUsers[username].Count == 0)
                {
                    OnlineUsers.Remove(username);
                    isOffline = true;
                }
            }
            return Task.CompletedTask;
            //return Task.FromResult(isOffline);
        }

        public Task<string[]> GetOnlineUsers()
        {
            string[] onlineUsers;
            lock (OnlineUsers)
            {
                onlineUsers = OnlineUsers
                    .OrderBy(k => k.Key)
                    .Select(k => k.Key).ToArray();
            }

            return Task.FromResult(onlineUsers);
        }

        public Task<List<string>> GetConnectionsForUser(string username)
        {
            List<string> connectionIds;
            lock (OnlineUsers)
            {
                connectionIds = OnlineUsers.GetValueOrDefault(username);
            }

            return Task.FromResult(connectionIds);
        }
    }
}
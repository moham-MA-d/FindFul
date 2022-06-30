using System;
using System.Threading.Tasks;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    [Authorize]
    // SignalR and webSocket cannot use authentication header and we can only use query string.

    public class OnlineHub : Hub
    {
        private readonly OnlineTracker _tracker;
        public OnlineHub(OnlineTracker tracker)
        {
            _tracker = tracker;
        }

        // in SignalR there is no way to detect online users in OnConnectedAsync.
        //  because in a web firm when there is more than one web server there is no way of getting the 
        //  connection information from the other server. this service is confined to the server that is running on. 

        //  There are some strategies to fix this:
        //  1. Redis, we can store tracking information in it that can be distribute among many servers.
        //  2. Use a class that is responsible who is connected and store that in a dictionary.
        //      this method is not scalable and work on a single server but it will not work on multiple servers
        //      because what we are going to create is a single server. and to scale this to multiple servers support
        //      we would need to use Redis or use a database to store this information.
        //      we use a class called `OnlineTracker` to implement this.
        public override async Task OnConnectedAsync()
        {
            await _tracker.UserConnected(Context.User.GetUsername(), Context.ConnectionId);
            //if (isOnline)
            //Clients: clients that are connected to the hub
            //UserIsOnline: Method that will be used in client
            await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUsername());

            var currentUsers = await _tracker.GetOnlineUsers();
            //await Clients.Caller.SendAsync("GetOnlineUsers", currentUsers);
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await _tracker.UserDisconnected(Context.User.GetUsername(), Context.ConnectionId);

            //if (isOffline)
            await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUsername());

            await base.OnDisconnectedAsync(exception);
        }
    }
}
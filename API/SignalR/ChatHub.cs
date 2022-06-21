using API.Extensions;
using Core.IServices.Mapper;
using Core.IServices.Messages;
using Core.IServices.SignalR;
using Core.Models.Entities.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly ISignalRService _signalRService;
        private readonly IMessageService _messageService;

       
        //public ChatHub(ISignalRService signalRService, IMessageService messageService)
        //{
        //    _signalRService = signalRService;
        //    _messageService = messageService;
        //}
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var targetUserId = Int32.Parse(httpContext.Request.Query["targetUserId"].ToString());
            var skip = Int32.Parse(httpContext.Request.Query["skip"].ToString());
            var groupName = GetGroupName(Context.User.GetUserId(), targetUserId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var group = await AddToGroup(groupName);
            //await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

            var messages = await _messageService.GetMessages(Context.User.GetUserId(), targetUserId, skip);

            //if (_messageService.HasChanges()) await _messageService.Complete();

            //await Clients.SignalRGroup(groupName).SendAsync("ReceiveMessageThread", messages);
            await Clients.All.SendAsync("ReceiveMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            
        }

        private async Task<SignalRGroup> AddToGroup(string groupName)
        {
            var group = await _signalRService.GetMessageGroup(groupName);
            //var connection = new DbLoggerCategory.Database.Connection(Context.ConnectionId, Context.User.GetUsername());
            var connection = new SignalRConnection(Context.ConnectionId, Context.User.GetUsername());

            if (group == null)
            {
                group = new SignalRGroup(groupName);
                _signalRService.AddGroup(group);
            }

            group.Connections.Add(connection);

            return group;

            throw new HubException("Failed to join group");
        }

        private string GetGroupName(int caller, int other)
        {
            var stringCompare = string.CompareOrdinal(caller.ToString(), other.ToString()) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }

    }
}

using API.Extensions;
using Core.IServices.Mapper;
using Core.IServices.Messages;
using Core.IServices.SignalR;
using Core.IServices.User;
using Core.Models.Entities.SignalR;
using Core.Models.Entities.User;
using DTO.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.SignalR
{
    [Authorize]

    public class MessageHub : Hub
    {
        private readonly IMapperService _mapperService;
        private readonly IMessageService _messageService;
        private readonly ISignalRService _signalRService;
        private readonly IUserService _userService;


        public MessageHub(
            IMapperService mapperService,
            IMessageService messageService,
            IUserService userService,
            ISignalRService signalRService)
        {
            _messageService = messageService;
            _userService = userService;
            _mapperService = mapperService;
            _signalRService = signalRService;
        }



        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var targetUserId = Int32.Parse(httpContext.Request.Query["targetUserId"].ToString());
            var targetUser = _userService.GetById(targetUserId);
            var skip = Int32.Parse(httpContext.Request.Query["skip"].ToString());
            var groupName = GetGroupName(Context.User.GetUserId(), targetUser.Id);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var group = await AddToGroup(groupName);
            await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

            var messages = await _messageService.GetMessages(Context.User.GetUserId(), targetUserId, skip);

            //if (_messageService.HasChanges()) await _messageService.Complete();

            await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // SignalR automatically remove user from a group when they are disconnected.
            var group = await RemoveFromMessageGroup();
            await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
            await base.OnDisconnectedAsync(exception);
        }

        // SendMessage Called by client
        public async Task SendMessage(DtoCreateMessage dtoCreateMessage)
        {
            var User = Context.GetHttpContext().User;

            if (string.IsNullOrWhiteSpace(dtoCreateMessage.Body)) throw new HubException("You cannot send empty message!");

            var sender = await _userService.GetByIdAsync(User.GetUserId());

            AppUser receiver;
            if (dtoCreateMessage.ReceiverId == 0)
                throw new HubException("No user found!");
            
            receiver = await _userService.GetByIdAsync(dtoCreateMessage.ReceiverId);
          
            if (receiver == null) throw new HubException("No user found!");

            if (User.GetUserId() == receiver.Id) throw new HubException("You cannot send a message to yourself!");

            var message = _messageService.Create(dtoCreateMessage, sender, receiver);

            await _messageService.AddAsync(message);

            var dtoMessage = _mapperService.MessageToDtoMessage(message);

            var groupName = GetGroupName(sender.Id, receiver.Id);
            var group = await _signalRService.GetMessageGroup(groupName);

            if (group.Connections.Any(x => x.UserId == receiver.Id.ToString()))
            {
                message.DateRead = DateTime.UtcNow;
            }

            await Clients.Group(groupName).SendAsync("NewMessage", dtoMessage);
        }

        private async Task<SignalRGroup> AddToGroup(string groupName)
        {
            var group = await _signalRService.GetMessageGroup(groupName);
            //var connection = new DbLoggerCategory.Database.Connection(Context.ConnectionId, Context.User.GetUsername());
            var connection = new SignalRConnection(Context.ConnectionId, Context.User.GetUserId().ToString());

            if (group == null)
            {
                group = new SignalRGroup(groupName);
                await _signalRService.AddAsync(group);
            }

            group.Connections.Add(connection);
            await _signalRService.SaveChangesAsync();

            return group;

            throw new HubException("Failed to join group");
        }

        private async Task<SignalRGroup> RemoveFromMessageGroup()
        {
            var group = await _signalRService.GetGroupForConnection(Context.ConnectionId);
            var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            _signalRService.RemoveConnection(connection);

            return group;

            throw new HubException("Failed to remove from group");
        }


        private string GetGroupName(int caller, int other)
        {
            var stringCompare = string.CompareOrdinal(caller.ToString(), other.ToString()) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
      
    }
}
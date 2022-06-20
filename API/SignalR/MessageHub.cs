using API.Extensions;
using Core.IServices.Mapper;
using Core.IServices.Messages;
using Core.IServices.SignalR;
using Core.IServices.User;
using Core.Models.Entities.SignalR;
using Core.Models.Entities.User;
using DTO.Messages;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.SignalR
{
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
            // SignalR automatically remove user from a group when they are disconnected.
            var group = await RemoveFromMessageGroup();
            //await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(DtoCreateMessage dtoCreateMessage)
        {
            var User = Context.GetHttpContext().User;

            if (string.IsNullOrWhiteSpace(dtoCreateMessage.Body)) throw new HubException("You cannot send empty message!");

            var sender = await _userService.GetByIdAsync(User.GetUserId());

            AppUser receiver;
            if (!string.IsNullOrEmpty(dtoCreateMessage.ReceiverUsername))
            {
                var receiverUsername = dtoCreateMessage.ReceiverUsername.ToLower();
                var receiverUser = await _userService.GetByUsernameAsync(receiverUsername);
                receiver = await _userService.GetByIdAsync(receiverUser.Id);
            }
            else
            {
                receiver = await _userService.GetByIdAsync(dtoCreateMessage.ReceiverId);
            }


            if (receiver == null) throw new HubException("No user found!");

            if (User.GetUserId() == receiver.Id) throw new HubException("You cannot send a message to yourself!");

            var message = _messageService.Create(dtoCreateMessage, sender, receiver);

            await _messageService.AddAsync(message);

            var dtoMessage = _mapperService.MessageToDtoMessage(message);

            var groupName = GetGroupName(sender.Id, receiver.Id);
            var group = await _signalRService.GetMessageGroup(groupName);

            if (group.Connections.Any(x => x.Username == receiver.UserName))
            {
                message.DateRead = DateTime.UtcNow;
            }

            await Clients.Group(groupName).SendAsync("NewMessage", dtoMessage);
            
            await Clients.All.SendAsync("NewMessage", dtoMessage);

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
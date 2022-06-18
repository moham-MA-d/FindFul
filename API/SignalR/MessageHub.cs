using API.Extensions;
using Core.IServices.Mapper;
using Core.IServices.Messages;
using Core.IServices.User;
using Core.Models.Entities.User;
using DTO.Messages;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace API.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IMapperService _mapperService;
        private readonly IHubContext<OnlineHub> _onlineHub;
        private readonly OnlineTracker _tracker;
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public MessageHub(
            IMapperService mapperService, 
            IMessageService messageService,
            IUserService userService, 
            IHubContext<OnlineHub> presenceHub,
            OnlineTracker tracker)
        {
            _messageService = messageService;
            _userService = userService;
            _tracker = tracker;
            _onlineHub = presenceHub;
            _mapperService = mapperService;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUserId = Int32.Parse(httpContext.Request.Query["targetUserId"].ToString());
            var skip = Int32.Parse(httpContext.Request.Query["skip"].ToString());
            var groupName = GetGroupName(Context.User.GetUserId(), otherUserId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            //var group = await AddToGroup(groupName);
            //await Clients.Group(groupName).SendAsync("UpdatedGroup", group);
            var messages = await _messageService.GetMessages(Context.User.GetUserId(), otherUserId, skip);

            //if (_messageService.HasChanges()) await _messageService.Complete();

            await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // SignalR automatically remove user from a group when they are disconnected.
            //var group = await RemoveFromMessageGroup();
           // await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
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

            var group = GetGroupName(sender.Id, receiver.Id);

            await Clients.Group(group).SendAsync("NewMessage", dtoMessage);

         }

            //private async Task<Group> AddToGroup(string groupName)
            //{
            //    var group = await _messageService.MessageRepository.GetMessageGroup(groupName);
            //    var connection = new DbLoggerCategory.Database.Connection(Context.ConnectionId, Context.User.GetUsername());

            //    if (group == null)
            //    {
            //        group = new Group(groupName);
            //        _messageService.MessageRepository.AddGroup(group);
            //    }

            //    group.Connections.Add(connection);

            //    if (await _messageService.Complete()) return group;

            //    throw new HubException("Failed to join group");
            //}

        //private async Task<Group> RemoveFromMessageGroup()
        //{
        //    var group = await _messageService.MessageRepository.GetGroupForConnection(Context.ConnectionId);
        //    var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
        //    _messageService.MessageRepository.RemoveConnection(connection);
        //    if (await _messageService.Complete()) return group;

        //    throw new HubException("Failed to remove from group");
        //}

        private string GetGroupName(int caller, int other)
        {
            var stringCompare = string.CompareOrdinal(caller.ToString(), other.ToString()) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}
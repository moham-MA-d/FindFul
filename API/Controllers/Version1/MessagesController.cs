using System;
using System.Threading.Tasks;
using API.Controllers.Version1.Base;
using API.Extensions;
using Core.IServices.Mapper;
using Core.IServices.Messages;
using Core.IServices.User;
using Core.Models.Entities.User;
using DTO.Member;
using DTO.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Version1
{
    [Authorize]
    public class MessagesController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IMapperService _mapperService;

        public MessagesController(IUserService userService, IMessageService messageService, IMapperService mapperService)
        {
            _userService = userService;
            _messageService = messageService;
            _mapperService = mapperService;
        }

    
        [HttpPost("AddMessage")]
        public async Task<ActionResult<DtoMessageResponse>> AddMessage(DtoCreateMessage dtoCreateMessage)
        {
            if (string.IsNullOrWhiteSpace(dtoCreateMessage.Body)) return BadRequest("You cannot send empty message!");

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


            if (receiver == null) return NotFound("No user found!");

            if (User.GetUserId() == receiver.Id) return BadRequest("You cannot send a message to yourself!");

            var message = _messageService.Create(dtoCreateMessage, sender, receiver);

            await _messageService.AddAsync(message);

            var dtoMessage = _mapperService.MessageToDtoMessage(message);

            var baseUrl = HttpContext.GetCurrentLocationUri();

            var locationUri = baseUrl + "/" + message.Id;

            return Created(locationUri, dtoMessage);
        }

        [HttpGet("GetChats")]
        public async Task<ActionResult<DtoMemberChat>> GetChats()
        {
            var userId = User.GetUserId();
            var chats = await _messageService.GetChats(userId);

            return Ok(chats);
        }

        [HttpGet("GetMessages")]
        public async Task<ActionResult<DtoMessageResponse>> GetMessages(int userId, int skip)
        {
            var targetUser = await _userService.GetUserByIdAsync(userId);
            if (targetUser == null) return BadRequest("No User Found!");

            var currentUserId = User.GetUserId();

            var isAnyChat = await _messageService.HasChatAsync(User.GetUserId(), targetUser.Id);
            if (!isAnyChat) return BadRequest("There is no chat!");

            var chats = await _messageService.GetMessages(currentUserId, targetUser.Id, skip);

            return Ok(chats);
        }
    }
}

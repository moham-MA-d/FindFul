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
    public class MessageController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IMapperService _mapperService;

        public MessageController(IUserService userService, IMessageService messageService, IMapperService mapperService)
        {
            _userService = userService;
            _messageService = messageService;
            _mapperService = mapperService;
        }

    
        [HttpPost("AddMessage")]
        public async Task<ActionResult<MessageDTO>> AddMessage(CreateMessageDTO createMessageDTO)
        {
            if (string.IsNullOrWhiteSpace(createMessageDTO.Body)) return BadRequest("You cannot send empty message!");

            var sender = await _userService.GetByIdAsync(User.GetUserId());

            AppUser reciever;
            if (!string.IsNullOrEmpty(createMessageDTO.RecieverUsername))
            {
                var recieverUsername = createMessageDTO.RecieverUsername.ToLower();
                var recieverUser = await _userService.GetByUsernameAsync(recieverUsername);
                reciever = await _userService.GetByIdAsync(recieverUser.Id);
            }
            else
            {
                reciever = await _userService.GetByIdAsync(createMessageDTO.RecieverId);
            }


            if (reciever == null) return NotFound("No user found!");

            if (User.GetUserId() == reciever.Id) return BadRequest("You cannot send a message to yourself!");

            var message = _messageService.Create(createMessageDTO, sender, reciever);

            await _messageService.AddAsync(message);

            var messageDTO = _mapperService.MessageToMessageDTO(message);

            return Ok(messageDTO);
        }

        [HttpGet("GetChats")]
        public async Task<ActionResult<MemberChatDTO>> GetChats()
        {
            var userId = User.GetUserId();
            var chats = await _messageService.GetChats(userId);

            return Ok(chats);
        }

        [HttpGet("GetMessages")]
        public async Task<ActionResult<MessageDTO>> GetMessages(int userId, int skip)
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

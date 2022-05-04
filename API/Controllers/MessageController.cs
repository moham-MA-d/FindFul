using API.Controllers.Base;
using API.Extensions;
using Core.IRepositories.User;
using Core.IService.User;
using Core.IServices.Mapper;
using Core.IServices.Messages;
using Core.Models.Entities.Messages;
using DTO.Member;
using DTO.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
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
            var sender = await _userService.GetByIdAsync(User.GetUserId());
            
            var recieverUsername = createMessageDTO.RecieverUsername.ToLower();
            var recieverUser = await _userService.GetByUsernameAsync(recieverUsername);
            var reciever = await _userService.GetByIdAsync(recieverUser.Id);

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

        [HttpGet("GetMessages/{userId}")]
        public async Task<ActionResult<MessageDTO>> GetMessages(int userId)
        {
            var targetUser = await _userService.GetUserByIdAsync(userId);
            if (targetUser == null) return BadRequest("No User Found!");

            var isAnyChat = await _messageService.HasChatAsync(User.GetUserId(), targetUser.Id);
            if (!isAnyChat) return BadRequest("There is no chat!");

            var chats = _messageService.GetChats(userId);

            return Ok(chats);
        }
    }
}

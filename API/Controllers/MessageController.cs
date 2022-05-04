using API.Controllers.Base;
using API.Extensions;
using Core.IRepositories.User;
using Core.IServices.Mapper;
using Core.IServices.Messages;
using Core.Models.Entities.Messages;
using DTO.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class MessageController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageService _messageService;
        private readonly IMapperService _mapperService;

        public MessageController(IUserRepository userRepository, IMessageService messageService, IMapperService mapperService)
        {
            _userRepository = userRepository;
            _messageService = messageService;
            _mapperService = mapperService;
        }

    
        [HttpPost("AddMessage")]
        public async Task<ActionResult<MessageDTO>> AddMessage(CreateMessageDTO createMessageDTO)
        {
            var currentUsername = User.GetUsername();
            var sender = await _userRepository.GetUserByUsernameAsync(currentUsername);
            
            var recieverUsername = createMessageDTO.RecieverUsername.ToLower();
            var reciever = await _userRepository.GetUserByUsernameAsync(recieverUsername);
            if (reciever == null) return NotFound("No user found!");

            if (currentUsername == recieverUsername) return BadRequest("You cannot send a message to yourself!");

            var message = _messageService.Create(createMessageDTO, sender, reciever);

            await _messageService.AddAsync(message);

            var messageDTO = _mapperService.MessageToMessageDTO(message);

            return Ok(messageDTO);
        }
    }
}

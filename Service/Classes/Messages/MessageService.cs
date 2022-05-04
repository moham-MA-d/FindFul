using Core;
using Core.IService;
using Core.IServices.Messages;
using Core.IRepositories.Messages;
using Core.Models.Entities.Messages;
using Core.Models.Entities.User;
using DTO.Messages;
using System.Collections.Generic;
using DTO.Member;
using System.Threading.Tasks;

namespace Service.Classes.Messages
{
    public class MessageService : EntityService<Message>, IMessageService, IEntityService<Message>
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IUnitOfWork unitOfWork, IMessageRepository messageRepository) : base(unitOfWork, messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public Message Create(CreateMessageDTO createMessageDTO, AppUser sender, AppUser reciever)
        {
            return new Message
            {
                SenderId = sender.Id,
                TheSender = sender,
                SenderUsername = sender.UserName,
                RecieverId = reciever.Id,
                TheReciever = reciever,
                RecieverUsername = reciever.UserName,
                Body = createMessageDTO.Body,
            };
        }

        public async Task<List<MemberChatDTO>> GetChats(int userId)
        {
           return await _messageRepository.GetChats(userId);
        }

        public async Task<bool> HasChatAsync(int currentUserId, int targetUserId)
        {
            return await _messageRepository.HasChat(currentUserId, targetUserId);
        }
    }
}

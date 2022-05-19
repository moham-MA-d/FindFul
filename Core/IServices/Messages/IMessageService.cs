using Core.Models.Entities.Messages;
using Core.Models.Entities.User;
using DTO.Member;
using DTO.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IServices.Messages
{
    public interface IMessageService : IEntityService<Message>
    {
        Message Create(CreateMessageDTO createMessageDTO, AppUser sender, AppUser Reciever);
        Task<List<MemberChatDTO>> GetChats(int userId);
        Task<bool> HasChatAsync(int currentUserId, int targetUserId);
        Task<List<MessageDTO>> GetMessages(int currentUserId, int targetUserId, int skip);

    }
}

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
        Message Create(DtoCreateMessage dtoCreateMessage, AppUser sender, AppUser Reciever);
        Task<List<DtoMemberChat>> GetChats(int userId);
        Task<bool> HasChatAsync(int currentUserId, int targetUserId);
        Task<List<DtoMessageResponse>> GetMessages(int currentUserId, int targetUserId, int skip);

    }
}

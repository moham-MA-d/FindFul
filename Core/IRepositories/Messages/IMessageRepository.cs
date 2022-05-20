using Core.Models.Entities.Messages;
using DTO.Member;
using DTO.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepositories.Messages
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        Task<List<DtoMemberChat>> GetChats(int userId);
        Task<List<DtoMessageResponse>> GetMessages(int currentUserId, int targetUserId, int skip);
        Task<bool> HasChat(int currentUserId, int targetUserId);

    }
}

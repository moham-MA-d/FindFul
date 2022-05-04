using Core.IService;
using Core.Models.Entities.Messages;
using Core.Models.Entities.User;
using DTO.Messages;

namespace Core.IServices.Messages
{
    public interface IMessageService : IEntityService<Message>
    {
        Message Create(CreateMessageDTO createMessageDTO, AppUser sender, AppUser Reciever);
    }
}

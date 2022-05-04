using Core.IRepositories.Messages;
using Core.Models.Entities.Messages;
using Microsoft.Extensions.Logging;

namespace Data.Repositories.Messages
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(DataContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}

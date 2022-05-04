using Core.IRepositories.Messages;
using Core.Models.Entities.Messages;
using DTO.Member;
using DTO.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories.Messages
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(DataContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<List<MemberChatDTO>> GetChats(int userId)
        {
            var senderUsers = _context.Users.OrderBy(x => x.CreateDateTime).AsQueryable();
            var recieverUsers = _context.Users.OrderBy(x => x.CreateDateTime).AsQueryable();

            var senderChats = _context.Messages.Where(x => x.SenderId == userId).Include(x => x.TheReciever.TheRecievedMessagesList).AsQueryable();
            var recieverChats = _context.Messages.Where(x => x.RecieverId == userId).Include(x => x.TheReciever.TheSentMessagesList).AsQueryable();

            senderUsers = recieverChats.Select(y => y.TheSender);
            recieverUsers = senderChats.Select(x => x.TheReciever);

            var senders = await senderUsers.ToListAsync();
            var recievers = await recieverUsers.ToListAsync();

            List<MemberChatDTO> chats = new();

            //TODO: Fix : Remove Repeated users in senders and recievers withing query
            var listSenderIds = new List<int>();
            foreach (var item in senders)
            {
                if (!listSenderIds.Contains(item.Id))
                {
                    MemberChatDTO chat = new()
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        UserName = item.UserName,
                        ProfilePhotoUrl = item.ProfilePhotoUrl,
                        LastMessage = item.TheSentMessagesList.Where(x => x.RecieverId == userId)
                                           .OrderByDescending(x => x.CreateDateTime).FirstOrDefault().Body,
                        LastMessageDateTime = item.TheSentMessagesList.Where(x => x.RecieverId == userId)
                                          .OrderByDescending(x => x.CreateDateTime).FirstOrDefault().CreateDateTime
                    };

                    chats.Add(chat);

                    listSenderIds.Add(item.Id);
                }
            }

            var listRecieverIds = new List<int>();
            foreach (var item in recievers)
            {
                if (!listRecieverIds.Contains(item.Id))
                {
                    MemberChatDTO chat = new()
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        UserName = item.UserName,
                        ProfilePhotoUrl = item.ProfilePhotoUrl,
                        LastMessage = item.TheRecievedMessagesList.Where(x => x.SenderId == userId)
                        .OrderByDescending(x => x.CreateDateTime).FirstOrDefault().Body,
                        LastMessageDateTime = item.TheRecievedMessagesList.Where(x => x.SenderId == userId)
                       .OrderByDescending(x => x.CreateDateTime).FirstOrDefault().CreateDateTime
                    };

                    chats.Add(chat);

                    listRecieverIds.Add(item.Id);
                }
            }

            chats = chats.OrderByDescending(x => x.LastMessageDateTime).ToList();

            return chats;

        }

        public Task<List<MessageDTO>> GetMessages(int currentUserId, int targetUserId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> HasChat(int currentUserId, int targetUserId)
        {

            if (await _context.Messages.AnyAsync(x => x.SenderId == currentUserId && x.RecieverId == targetUserId))
                return true;

            if (await _context.Messages.AnyAsync(x => x.SenderId == targetUserId && x.RecieverId == currentUserId))
                return true;

            return false;

        }
    }
}

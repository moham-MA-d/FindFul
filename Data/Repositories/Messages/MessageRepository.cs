using AutoMapper;
using Core.IRepositories.Messages;
using Core.Models.Entities.Messages;
using DTO.Member;
using DTO.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories.Messages
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly IMapper _mapper;

        public MessageRepository(DataContext context, ILogger logger, IMapper mapper) : base(context, logger)
        {
            _mapper = mapper;
        }


        public async Task<List<DtoMemberChat>> GetChats(int userId)
        {
            var senderUsers = _context.Users.OrderBy(x => x.CreateDateTime).AsQueryable();
            var recieverUsers = _context.Users.OrderBy(x => x.CreateDateTime).AsQueryable();

            var sentChats = _context.Messages.Where(x => x.SenderId == userId)
                .Include(x => x.TheReceiver.TheSentMessagesList)
                .Include(x => x.TheReceiver.TheReceivedMessagesList).AsQueryable();
            var recievedChats = _context.Messages.Where(x => x.ReceiverId == userId)
                .Include(x => x.TheSender.TheReceivedMessagesList)
                .Include(x => x.TheSender.TheSentMessagesList).AsQueryable();

            senderUsers = recievedChats.Select(y => y.TheSender);
            recieverUsers = sentChats.Select(x => x.TheReceiver);

            var senders = await senderUsers.ToListAsync();
            var recievers = await recieverUsers.ToListAsync();

            List<DtoMemberChat> chats = new();

            //TODO: Fix : Remove Repeated users in senders and recievers withing query
            var listRelatedUserIds = new List<int>();

            foreach (var item in senders)
            {
                if (!listRelatedUserIds.Contains(item.Id))
                {
                    var lastMessage = item.TheSentMessagesList.Where(x => x.ReceiverId == userId)
                                           .OrderByDescending(x => x.CreateDateTime).FirstOrDefault();
                    if (item.TheReceivedMessagesList != null)
                    {
                        var lastMessageRecieved = item.TheReceivedMessagesList.Where(x => x.SenderId == userId)
                                           .OrderByDescending(x => x.CreateDateTime).FirstOrDefault();
                        if (lastMessageRecieved != null && lastMessageRecieved.CreateDateTime > lastMessage.CreateDateTime)
                        {
                            lastMessage = lastMessageRecieved;
                        }
                    }

                    DtoMemberChat chat = new()
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        UserName = item.UserName,
                        ProfilePhotoUrl = item.ProfilePhotoUrl,
                        LastMessage = lastMessage.Body,
                        LastMessageDateTime = lastMessage.CreateDateTime
                    };

                    chats.Add(chat);

                    listRelatedUserIds.Add(item.Id);
                }
            }

            foreach (var item in recievers)
            {
                if (!listRelatedUserIds.Contains(item.Id))
                {
                    var lastMessage = item.TheReceivedMessagesList.Where(x => x.SenderId == userId)
                                               .OrderByDescending(x => x.CreateDateTime).FirstOrDefault();

                    if (item.TheSentMessagesList != null)
                    {
                        var lastMessagSender = item.TheSentMessagesList.Where(x => x.ReceiverId == userId)
                                          .OrderByDescending(x => x.CreateDateTime).FirstOrDefault();
                        if (lastMessagSender != null && lastMessagSender.CreateDateTime > lastMessage.CreateDateTime)
                        {
                            lastMessage = lastMessagSender;
                        }
                    }

                    DtoMemberChat chat = new()
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        UserName = item.UserName,
                        ProfilePhotoUrl = item.ProfilePhotoUrl,
                        LastMessage = lastMessage.Body,
                        LastMessageDateTime = lastMessage.CreateDateTime
                    };

                    chats.Add(chat);

                    listRelatedUserIds.Add(item.Id);
                }
            }

            chats = chats.OrderByDescending(x => x.LastMessageDateTime).ToList();

            return chats;

        }
        public async Task<List<DtoMessageResponse>> GetMessages(int currentUserId, int targetUserId, int skip)
        {
            var messagesQuery = _context.Messages
                .Where(x => (x.SenderId == currentUserId && x.ReceiverId == targetUserId) || (x.SenderId == targetUserId && x.ReceiverId == currentUserId))
                .OrderByDescending(x => x.CreateDateTime)
                .Include(x => x.TheSender)
                .Include(x => x.TheReceiver)
                .AsQueryable();

            var messagesCount = await messagesQuery.CountAsync();

            var diff = messagesCount - skip;
            if (diff < 0 || skip >= messagesCount)
                return null;

            if (messagesCount <= skip)
                skip = 0;

            messagesQuery = messagesQuery.Skip(skip);
            messagesQuery = messagesQuery.Take(20);

            var messages = await messagesQuery.OrderBy(x => x.CreateDateTime).ToListAsync();

            var unreadMessages = messages.Where(x => x.DateRead == null && x.ReceiverId == currentUserId).ToList();
            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.Now;
                }
            }

            var messagesDTO = messages.Select(x => new DtoMessageResponse
            {
                SenderId = x.SenderId,
                RecieverId = x.ReceiverId,
                Body = x.Body,
                CreateDateTime = x.CreateDateTime,
                SenderPhotoUrl = x.TheSender.ProfilePhotoUrl,
                RecieverPhotoUrl = x.TheReceiver.ProfilePhotoUrl,
                SenderUsername = x.TheSender.UserName,
                RecieverUsername = x.TheReceiver.UserName,
                DateReaded = x.DateRead,
            }).ToList();


            return messagesDTO;
        }
        public async Task<bool> HasChat(int currentUserId, int targetUserId)
        {

            if (await _context.Messages.AnyAsync(x => x.SenderId == currentUserId && x.ReceiverId == targetUserId))
                return true;

            if (await _context.Messages.AnyAsync(x => x.SenderId == targetUserId && x.ReceiverId == currentUserId))
                return true;

            return false;

        }
    }
}

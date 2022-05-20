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
                .Include(x => x.TheReciever.TheSentMessagesList)
                .Include(x => x.TheReciever.TheRecievedMessagesList).AsQueryable();
            var recievedChats = _context.Messages.Where(x => x.RecieverId == userId)
                .Include(x => x.TheSender.TheRecievedMessagesList)
                .Include(x => x.TheSender.TheSentMessagesList).AsQueryable();

            senderUsers = recievedChats.Select(y => y.TheSender);
            recieverUsers = sentChats.Select(x => x.TheReciever);

            var senders = await senderUsers.ToListAsync();
            var recievers = await recieverUsers.ToListAsync();

            List<DtoMemberChat> chats = new();

            //TODO: Fix : Remove Repeated users in senders and recievers withing query
            var listRelatedUserIds = new List<int>();

            foreach (var item in senders)
            {
                if (!listRelatedUserIds.Contains(item.Id))
                {
                    var lastMessage = item.TheSentMessagesList.Where(x => x.RecieverId == userId)
                                           .OrderByDescending(x => x.CreateDateTime).FirstOrDefault();
                    if (item.TheRecievedMessagesList != null)
                    {
                        var lastMessageRecieved = item.TheRecievedMessagesList.Where(x => x.SenderId == userId)
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
                    var lastMessage = item.TheRecievedMessagesList.Where(x => x.SenderId == userId)
                                               .OrderByDescending(x => x.CreateDateTime).FirstOrDefault();

                    if (item.TheSentMessagesList != null)
                    {
                        var lastMessagSender = item.TheSentMessagesList.Where(x => x.RecieverId == userId)
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
                .Where(x => (x.SenderId == currentUserId && x.RecieverId == targetUserId) || (x.SenderId == targetUserId && x.RecieverId == currentUserId))
                .OrderByDescending(x => x.CreateDateTime)
                .Include(x => x.TheSender)
                .Include(x => x.TheReciever)
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

            var unreadMessages = messages.Where(x => x.DateReaded == null && x.RecieverId == currentUserId).ToList();
            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateReaded = DateTime.Now;
                }
            }

            var messagesDTO = messages.Select(x => new DtoMessageResponse
            {
                SenderId = x.SenderId,
                RecieverId = x.RecieverId,
                Body = x.Body,
                CreateDateTime = x.CreateDateTime,
                SenderPhotoUrl = x.TheSender.ProfilePhotoUrl,
                RecieverPhotoUrl = x.TheReciever.ProfilePhotoUrl,
                SenderUsername = x.TheSender.UserName,
                RecieverUsername = x.TheReciever.UserName,
                DateReaded = x.DateReaded,
            }).ToList();


            return messagesDTO;
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

using Core.Models.Entities.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices.SignalR
{
    public interface ISignalRService : IEntityService<SignalRGroup>
    {
        void AddGroup(SignalRGroup group);
        void RemoveConnection(SignalRConnection connection);
        Task<SignalRConnection> GetConnection(string connectionId);
        Task<SignalRGroup> GetMessageGroup(string groupName);
        Task<SignalRGroup> GetGroupForConnection(string connectionId);
    }
}

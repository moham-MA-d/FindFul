using Core.Models.Entities.SignalR;
using System.Threading.Tasks;

namespace Core.IRepositories.SignalR
{
    public interface ISignalRRepository : IGenericRepository<SignalRGroup>
    {
        void RemoveConnection(SignalRConnection connection);
        Task<SignalRConnection> GetConnection(string connectionId);
        Task<SignalRGroup> GetMessageGroup(string groupName);
        Task<SignalRGroup> GetGroupForConnection(string connectionId);
    }
}

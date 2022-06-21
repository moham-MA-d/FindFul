using Core.IRepositories.SignalR;
using Core.Models.Entities.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.SignalR
{
    public class SignalRRepository : GenericRepository<SignalRGroup>, ISignalRRepository
    {

        public SignalRRepository(DataContext context, ILogger logger) : base(context, logger)
        {
        }


        public async Task<SignalRGroup> GetGroupForConnection(string connectionId)
        {
            return await _context.SignalRGroups
                .Include(c => c.Connections)
                .Where(c => c.Connections.Any(x => x.ConnectionId == connectionId))
                .FirstOrDefaultAsync();
        }

        public async Task<SignalRGroup> GetMessageGroup(string groupName)
        {
            return await _context.SignalRGroups
                .Include(x => x.Connections)
                .FirstOrDefaultAsync(x => x.Name == groupName);
        }

        public void RemoveConnection(SignalRConnection connection)
        {
            _context.SignalRConnections.Remove(connection);
        }

        public async Task<SignalRConnection> GetConnection(string connectionId)
        {
            return await _context.SignalRConnections.FindAsync(connectionId);
        }
    }
}

using Core;
using Core.IRepositories;
using Core.IRepositories.SignalR;
using Core.IServices;
using Core.IServices.SignalR;
using Core.Models.Entities.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Classes.SignalR
{
    public class SignalRService : EntityService<SignalRGroup>, ISignalRService, IEntityService<SignalRGroup>
    {
        readonly ISignalRRepository _signalRRepository;
        public SignalRService(IUnitOfWork unitOfWork, ISignalRRepository signalRRepository) : base(unitOfWork, signalRRepository)
        {
            _signalRRepository = signalRRepository;
        }

        public void AddGroup(SignalRGroup group)
        {
            _signalRRepository.AddGroup(group);
        }

        public async Task<SignalRConnection> GetConnection(string connectionId)
        {
            return await _signalRRepository.GetConnection(connectionId);
        }

        public async Task<SignalRGroup> GetGroupForConnection(string connectionId)
        {
            return await _signalRRepository.GetGroupForConnection(connectionId);
        }

        public async Task<SignalRGroup> GetMessageGroup(string groupName)
        {
            return await _signalRRepository.GetMessageGroup(groupName);   
        }

        public void RemoveConnection(SignalRConnection connection)
        {
            _signalRRepository.RemoveConnection(connection);
        }
    }
}

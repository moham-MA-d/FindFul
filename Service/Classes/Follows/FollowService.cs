using Core;
using Core.IRepositories;
using Core.IRepositories.Follows;
using Core.IService;
using Core.IServices.Follows;
using Core.Models.Entities.Follows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Classes.Follows
{
	public class FollowService : EntityService<Follow>, IFollowService, IEntityService<Follow>
	{
		private readonly IFollowRepository _followRepository;

		public FollowService(IUnitOfWork unitOfWork, IFollowRepository followRepository) : base(unitOfWork, followRepository)
		{
			_followRepository = followRepository;
		}

		public async Task<Follow> GetFollow(int userId, int recipientId)
		{
			return await _followRepository.GetFollow(userId, recipientId);
		}

        public Task<IEnumerable<Follow>> GetFollowers(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Follow>> GetFollowings(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}

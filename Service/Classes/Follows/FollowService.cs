using Core;
using Core.IRepositories.Follows;
using Core.IService;
using Core.IServices.Follows;
using Core.Models.Entities.Follows;
using DTO.Account;
using DTO.Pagination;
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

        public async Task<PagedListBase<MemberDTO>> GetFollowers(UserParameters userParameters)
        {
           return await _followRepository.GetFollowers(userParameters);
        }

		public async Task<PagedListBase<MemberDTO>> GetFollowings(UserParameters userParameters)
        {
			return await _followRepository.GetFollowing(userParameters);
		}
	}
}

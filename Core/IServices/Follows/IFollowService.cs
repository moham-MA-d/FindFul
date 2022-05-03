using Core.IService;
using Core.Models.Entities.Follows;
using DTO.Account;
using DTO.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IServices.Follows
{
	public interface IFollowService : IEntityService<Follow>
	{
		Task<Follow> GetFollow(int userId, int recipientId);
		Task<PagedListBase<MemberDTO>> GetFollowings(UserParameters userParameters);
		Task<PagedListBase<MemberDTO>> GetFollowers(UserParameters userParameters);

	}
}

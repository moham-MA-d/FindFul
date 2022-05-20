using Core.Models.Entities.Follows;
using DTO.Account;
using DTO.Pagination;
using System.Threading.Tasks;

namespace Core.IServices.Follows
{
	public interface IFollowService : IEntityService<Follow>
	{
		Task<Follow> GetFollow(int userId, int recipientId);
		Task<PagedListBase<DtoMember>> GetFollowings(UserParameters userParameters);
		Task<PagedListBase<DtoMember>> GetFollowers(UserParameters userParameters);

	}
}

using Core.IService;
using Core.Models.Entities.Follows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IServices.Follows
{
	public interface IFollowService : IEntityService<Follow>
	{
		Task<Follow> GetFollow(int userId, int recipientId);
		Task<IEnumerable<Follow>> GetFollowings(int userId);
		Task<IEnumerable<Follow>> GetFollowers(int userId);

	}
}

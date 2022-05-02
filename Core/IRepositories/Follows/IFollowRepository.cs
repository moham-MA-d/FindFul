using Core.Models.Entities.Follows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepositories.Follows
{
	public interface IFollowRepository : IGenericRepository<Follow>
	{
		//check if a User has already followed a particular User.
		Task<Follow> GetFollow(int userId, int recipientId);

		Task<IEnumerable<Follow>> GetFollowing(int userId);
		Task<IEnumerable<Follow>> GetFollowers(int userId);


	}
}

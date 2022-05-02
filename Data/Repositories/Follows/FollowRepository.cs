using Core.IRepositories.Follows;
using Core.IRepositories.User;
using Core.Models.Entities.Follows;
using Core.Models.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories.Follows
{
    public class FollowRepository : GenericRepository<Follow>, IFollowRepository
	{
        private readonly IUserRepository userRepository;

        public FollowRepository(DataContext context, ILogger logger, IUserRepository userRepository) : base(context, logger)
		{
            this.userRepository = userRepository;
        }

		public async Task<Follow> GetFollow(int userId, int recipientId)
		{
			return await _context.Follows.FirstOrDefaultAsync(u => u.FollowerId == userId && u.FollowingId == recipientId);
		}

        public async Task<IEnumerable<Follow>> GetFollowers(int userId)
        {
             var user = await _context.Users
                .Include(x => x.TheFollowerList)
                .FirstOrDefaultAsync(u => u.Id == userId);


                return user.TheFollowerList.Where(x => x.FollowerId == userId);
        }

        public async Task<AppUser> GetUserWithFollwers(int userId)
        {
            var q = _context.Users.Include(x => x.TheFollowerList);
            var r = await userRepository.GetUserByIdAsync(userId, q);
            return r;
        }

        public Task<IEnumerable<Follow>> GetFollowing(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}

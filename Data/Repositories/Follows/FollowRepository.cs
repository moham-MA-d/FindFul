using Core.IRepositories.Follows;
using Core.IRepositories.User;
using Core.Models.Entities.Follows;
using Data.Helpers;
using DTO.Account;
using DTO.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Extensions.Common;

namespace Data.Repositories.Follows
{
    public class FollowRepository : GenericRepository<Follow>, IFollowRepository
    {
        private readonly IUserRepository _userRepository;

        public FollowRepository(DataContext context, ILogger logger, IUserRepository userRepository) : base(context, logger)
        {
            _userRepository = userRepository;
        }

        public async Task<Follow> GetFollow(int userId, int recipientId)
        {
            return await _context.Follows.FirstOrDefaultAsync(u => u.FollowerId == userId && u.FollowingId == recipientId);
        }


        public async Task<PagedListBase<MemberDTO>> GetFollowing(UserParameters userParameters)
        {
            var targetUser = await _userRepository.GetMemberByUsernameAsync(userParameters.Username);
            
            var users = _context.Users.OrderBy(x => x.CreateDateTime).AsQueryable();
            var following = _context.Follows.Where(x => x.FollowerId == targetUser.Id).AsQueryable();

            users = following.Select(x => x.TheFollower);

            var followingUsers = users.Select(user => new MemberDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Age = user.DateOfBirth.CalculateAge(),
                City = user.City,
                Id = user.Id,
                Sex = (DTO.Enumerations.UserEnums.Sex)user.Sex,
                Gender = (DTO.Enumerations.UserEnums.Gender)user.Gender,
                ProfilePhotoUrl = user.ProfilePhotoUrl
            });

            return await PagedList<MemberDTO>.CreateAsync(followingUsers, userParameters.PageIndex, userParameters.PageSize);
        }

        public async Task<PagedListBase<MemberDTO>> GetFollowers(UserParameters userParameters)
        {
            var targetUser = await _userRepository.GetMemberByUsernameAsync(userParameters.Username);

            var users = _context.Users.OrderBy(x => x.CreateDateTime).AsQueryable();
            var following = _context.Follows.Where(x => x.FollowingId == targetUser.Id).AsQueryable();

            users = following.Select(x => x.TheFollowing);

            var followingUsers = users.Select(user => new MemberDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Age = user.DateOfBirth.CalculateAge(),
                City = user.City,
                Id = user.Id,
                Sex = (DTO.Enumerations.UserEnums.Sex)user.Sex,
                Gender = (DTO.Enumerations.UserEnums.Gender)user.Gender,
                ProfilePhotoUrl = user.ProfilePhotoUrl
            });

            return await PagedList<MemberDTO>.CreateAsync(followingUsers, userParameters.PageIndex, userParameters.PageSize);
        }
    }
}

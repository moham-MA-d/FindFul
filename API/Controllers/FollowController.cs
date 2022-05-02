using API.Controllers.Base;
using API.Extensions;
using Core.IService.User;
using Core.IServices.Follows;
using Core.Models.Entities.Follows;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public class FollowController : BaseApiController
    {
        private readonly IFollowService _followService;
        private readonly IUserService _userService;

        public FollowController(IFollowService followService, IUserService userService)
        {
            _followService = followService;
            _userService = userService;
        }

        
        [HttpPost("FollowUser/{username}")]
        public async Task<IActionResult> FollowUser(string username)
        {
            var targetUser = await _userService.GetByUsernameAsync(username);
            if (targetUser == null)
                return NotFound();

            int currentUserId = User.GetUserId();

            var follow = await _followService.GetFollow(currentUserId, targetUser.Id);

            if (follow == null)
            {
                follow = new Follow
                {
                    FollowerId = currentUserId,
                    FollowingId = targetUser.Id
                };

                await _followService.AddAsync(follow);

                return Ok(new {data = "follow" });
            }

            // Do Unfollow
            if (!follow.IsActive)
            {
                follow.IsActive = true;
                _followService.Update(follow);
                return Ok(new { data = "unfollow" });
            }

            // Do Follow
            follow.IsActive = false;
            _followService.Update(follow);
            return Ok(new { data = "follow" });

        }

    }
}

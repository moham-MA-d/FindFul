using API.Controllers.Base;
using API.Extensions;
using Core.IService.User;
using Core.IServices.Follows;
using Core.Models.Entities.Follows;
using DTO.Account;
using DTO.Pagination;
using Extensions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

            if(currentUserId == targetUser.Id) return BadRequest("NO!");

            var follow = await _followService.GetFollow(currentUserId, targetUser.Id);

            if (follow == null)
            {
                follow = new Follow
                {
                    FollowerId = currentUserId,
                    FollowingId = targetUser.Id,
                    IsActive = true
                };

                await _followService.AddAsync(follow);

                return Ok(new {data = "follow" });
            }

            if (!follow.IsActive)
            {
                // Do Follow
                follow.IsActive = true;
                _followService.Update(follow);
                return Ok(new { data = "follow" });
                
            }

            // Do Unfollow
            follow.IsActive = false;
            _followService.Update(follow);
            return Ok(new { data = "unfollow" });


        }


        [HttpGet("GetFollowing")]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetFollowing([FromQuery] UserParameters userParameters)
        {
            ++userParameters.PageIndex;

            var members = await _followService.GetFollowings(userParameters);

            Response.AddPaginationHeader(members.PageIndex, members.PageSize, members.TotalItems, members.TotalPages);

            return Ok(members);
        }


        [HttpGet("GetFollowers")]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetFollowers([FromQuery] UserParameters userParameters)
        {
            ++userParameters.PageIndex;

            var members = await _followService.GetFollowers(userParameters);

            Response.AddPaginationHeader(members.PageIndex, members.PageSize, members.TotalItems, members.TotalPages);

            return Ok(members);
        }

    }
}

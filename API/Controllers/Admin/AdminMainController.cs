using System.Linq;
using System.Threading.Tasks;
using API.Controllers.Base;
using Core.Models.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Admin
{
    public class AdminMainController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminMainController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(policy: "AdminPolicy")]
        [HttpGet("GetUsersWithRoles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                .Include(x => x.TheUserRolesList)
                .ThenInclude(x => x.TheRole)
                .Select(u => new
                {
                    u.Id,
                    UserName = u.UserName,
                    Roles = u.TheUserRolesList.Select(r => r.TheRole.Name).ToList()
                })
                .OrderBy(x => x.Roles.FirstOrDefault())
                .ThenBy(x => x.Id)
                .ToListAsync();

            return Ok(users);
        }

        [HttpPost("EditRoles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound("User not found!");

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) return BadRequest("Failed to add to user roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) return BadRequest("Failed to remove from roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(policy: "ModeratePolicy")]
        [HttpGet("UsersWithPhoto")]
        public ActionResult GetUsersWithPhoto()
        {
            return Ok("GetUsersWithRoles");
        }
    }
}

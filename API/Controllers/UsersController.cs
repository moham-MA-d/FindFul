using API.Controllers.Base;
using DTO.Account;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.IService.User;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {
            var members = await _userService.GetAllMembers();
            return Ok(members);
        }


        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUser(string username)
        {
            var member = await _userService.GetByUsername(username);
            if (member == null)
                member = await _userService.GetByEmail(username);

            return Ok(member);
        }
        
    }
}
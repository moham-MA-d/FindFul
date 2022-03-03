using API.Controllers.Base;
using DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.IService.User;
using System.Security.Claims;
using Core.Iservices.Mapper;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapperService _mapperservice;

        public UsersController(IUserService userService, IMapperService mapperservice)
        {
            _userService = userService;
            _mapperservice = mapperservice;
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

        [HttpPut]
        public async Task<ActionResult> Update(MemberUpdateDTO memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var memberDto = await _userService.GetByUsername(username);

            var appUser = await _userService.GetById(memberDto.Id);

            appUser = _mapperservice.MemberUpdateDtoToAppUser(memberUpdateDto, appUser);
            _userService.Update(appUser);

            return NoContent();
        }

    }
}
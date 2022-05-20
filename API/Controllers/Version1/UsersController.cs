using System.Collections.Generic;
using System.Threading.Tasks;
using API.Contracts;
using API.Controllers.Version1.Base;
using API.Extensions;
using API.Services.Interfaces;
using Core.IServices.Mapper;
using Core.IServices.User;
using DTO.Account;
using DTO.Account.Photo;
using DTO.Pagination;
using Extensions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Version1
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapperService _mapperService;
        private readonly IPhotoServiceAPI _photoServiceAPI;

        public UsersController(IUserService userService, IMapperService mapperService, IPhotoServiceAPI photoService, IUserPhotoService userPhotoService)
        {
            _userService = userService;
            _mapperService = mapperService;
            _photoServiceAPI = photoService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetAll([FromQuery] UserParameters userParameters)
        {
            ++userParameters.PageIndex;

            var memberDto = await _userService.GetByUsernameAsync(User.GetUsername());
            userParameters.CurrentUsername = memberDto.UserName;
           
            //Get All Users Except current user
            var members = await _userService.GetAllMembers(userParameters);

            Response.AddPaginationHeader(members.PageIndex, members.PageSize, members.TotalItems, members.TotalPages);

            return Ok(members);
        }
       
        [HttpGet("GetUser/{username}", Name ="GetUser")]
        public async Task<ActionResult<MemberDTO>> GetUser(string username)
        {
            var member = await _userService.GetByUsernameAsync(username) ?? await _userService.GetByEmail(username);
            return Ok(member);
        }
         
        [HttpPut]
        public async Task<ActionResult> Update(MemberUpdateDTO memberUpdateDto)
        {
            var appUser = await _userService.GetByIdAsync(User.GetUserId());

            appUser = _mapperService.MemberUpdateDtoToAppUser(memberUpdateDto, appUser);
            _userService.Update(appUser);

            return NoContent();
        }

        [HttpPost("AddProfilePhoto")]
        public async Task<ActionResult<MemberPhotoDTO>> AddProfilePhoto(IFormFile file)
        {
            var appUser = await _userService.GetUserByIdAsync(User.GetUserId());

            var result = await _photoServiceAPI.AddPhotoAsyncAPI(file);
            if (result.Error != null) return BadRequest(result.Error.Message);

            appUser.ProfilePhotoUrl = result.SecureUrl.AbsoluteUri;
            appUser.ProfilePhotoUrlPublicId = result.PublicId;

            await _userService.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteProfilePhoto")]
        public async Task<ActionResult> DeleteProfilePhoto()
        {
            var appUser = await _userService.GetUserByIdAsync(User.GetUserId());

            appUser.ProfilePhotoUrl = null;
            appUser.ProfilePhotoUrlPublicId = null;

            await _userService.SaveChangesAsync();

            return NoContent();
        }


    }
}
using API.Controllers.Base;
using DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.IService.User;
using Core.IServices.Mapper;
using API.Extensions;
using API.Services.Interfaces;
using Core.IServices.User;
using DTO.Account.Photo;
using Microsoft.AspNetCore.Http;
using Core.Models.Entities.User;
using DTO.Pagination;
using Extensions.Common;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapperService _mapperservice;
        private readonly IPhotoServiceAPI _photoServiceAPI;
        private readonly IUserPhotoService _userPhotoService;

        public UsersController(IUserService userService, IMapperService mapperservice, IPhotoServiceAPI photoService, IUserPhotoService userPhotoService)
        {
            _userService = userService;
            _mapperservice = mapperservice;
            _photoServiceAPI = photoService;
            _userPhotoService = userPhotoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers([FromQuery] PageParameters pageParameters)
        {
            var members = await _userService.GetAllMembers(pageParameters);

            Response.AddPaginationHeader(members.CurrentPage, members.PageSize, members.TotalItems, members.TotalPages);

            return Ok(members);
        }
       

        [HttpGet("GetUser/{username}", Name ="GetUser")]
        public async Task<ActionResult<MemberDTO>> GetUser(string username)
        {
            var member = await _userService.GetByUsernameAsync(username);
            if (member == null)
                member = await _userService.GetByEmail(username);

            return Ok(member);
        }


        [HttpPut]
        public async Task<ActionResult> Update(MemberUpdateDTO memberUpdateDto)
        {
            var memberDto = await _userService.GetByUsernameAsync(User.GetUsername());

            var appUser = await _userService.GetByIdAsync(memberDto.Id);

            appUser = _mapperservice.MemberUpdateDtoToAppUser(memberUpdateDto, appUser);
            _userService.Update(appUser);

            return NoContent();
        }

        //[HttpPut]
        //public ActionResult Update(MemberUpdateDTO memberUpdateDto)
        //{
        //    var memberDto = _userService.GetByUsername(User.GetUsername());

        //    var appUser = _userService.GetById(memberDto.Id);

        //    appUser = _mapperservice.MemberUpdateDtoToAppUser(memberUpdateDto, appUser);
        //    _userService.Update(appUser);

        //    return NoContent();
        //}



        [HttpPost("AddProfilePhoto")]
        public async Task<ActionResult<MemberPhotoDTO>> AddProfilePhoto(IFormFile file)
        {
            var user = await _userService.GetByUsernameAsync(User.GetUsername());
            var appUser = await _userService.GetUserByIdAsync(user.Id);

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
            var user = await _userService.GetByUsernameAsync(User.GetUsername());
            var appUser = await _userService.GetUserByIdAsync(user.Id);

            appUser.ProfilePhotoUrl = null;
            appUser.ProfilePhotoUrlPublicId = null;

            await _userService.SaveChangesAsync();

            return NoContent();
        }




    }
}
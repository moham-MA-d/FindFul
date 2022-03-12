using API.Controllers.Base;
using DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.IService.User;
using Core.IServices.Mapper;
using API.Extensions;
using Microsoft.AspNetCore.Http;
using API.Services.Interfaces;
using Core.Models.Entities.User;
using DTO.Account.Photo;
using Core.IServices.User;

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

        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {
            var members = await _userService.GetAllMembers();
            return Ok(members);
        }

       

        [HttpGet("GetUser/{username}", Name ="GetUser")]
        public async Task<ActionResult<MemberDTO>> GetUser(string username)
        {
            var member = await _userService.GetByUsername(username);
            if (member == null)
                member = await _userService.GetByEmail(username);

            return Ok(member);
        }

        [HttpGet("GetUserPhotos/{username}")]
        public async Task<ActionResult<MemberDTO>> GetUserPhotos(string username)
        {
            var user = await _userService.GetByUsername(username);

            var userPhotos = await _userPhotoService.GetAllUserPhotos(user.Id);

            return Ok(userPhotos);
        }

        [HttpPut]
        public async Task<ActionResult> Update(MemberUpdateDTO memberUpdateDto)
        {
            var memberDto = await _userService.GetByUsername(User.GetUsername());

            var appUser = await _userService.GetByIdAsync(memberDto.Id);

            appUser = _mapperservice.MemberUpdateDtoToAppUser(memberUpdateDto, appUser);
            _userService.Update(appUser);

            return NoContent();
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<MemberPhotoDTO>> AddPhoto(IFormFile file)
        {
            var user = await _userService.GetByUsername(User.GetUsername());
            var appUser = await _userService.GetUserByIdAsync(user.Id);

            var result = await _photoServiceAPI.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var userPhoto = new UserPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (appUser.TheUserPhotosList.Count == 0)
                userPhoto.IsMain = true;

            appUser.TheUserPhotosList.Add(userPhoto);
            
            await _userService.SaveChangesAsync();

            // When We create a resource on server we must return 201 response message or actually one of the Create() methods as follow.
            // And also we should have `Location Header` in response.
            // "GetUser" is the name of the route for the GetUser() method.
            return CreatedAtRoute("GetUser", new { username = appUser.UserName }, _mapperservice.UserPhotoToMemberPhotoDto(userPhoto));

        }

    }
}
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.Version1.Base;
using API.Extensions;
using API.Services.Interfaces;
using Core.IServices.Mapper;
using Core.IServices.User;
using Core.Models.Entities.User;
using DTO.Account;
using DTO.Account.Photo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Version1
{
    [Authorize]
    public class UserAlbumController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapperService _mapperservice;
        private readonly IPhotoServiceAPI _photoServiceAPI;
        private readonly IUserPhotoService _userPhotoService;

        public UserAlbumController(IUserService userService, IMapperService mapperservice, IPhotoServiceAPI photoService, IUserPhotoService userPhotoService)
        {
            _userService = userService;
            _mapperservice = mapperservice;
            _photoServiceAPI = photoService;
            _userPhotoService = userPhotoService;
        }


        [HttpGet("GetUserPhotos/{username}")]
        public async Task<ActionResult<DtoMember>> GetUserPhotos(string username)
        {
            var user = await _userService.GetByUsernameAsync(username);

            var userPhotos = await _userPhotoService.GetAllUserPhotos(user.Id);

            return Ok(userPhotos);
        }

        [HttpPost("AddPhoto")]
        public async Task<ActionResult<MemberPhotoDTO>> AddPhoto(IFormFile file)
        {
            var user = await _userService.GetByUsernameAsync(User.GetUsername());
            var appUser = await _userService.GetUserByIdAsync(user.Id);

            var result = await _photoServiceAPI.AddPhotoAsyncAPI(file);

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
            return CreatedAtRoute("GetUser", new { username = appUser.UserName }, _mapperservice.UserPhotoToDtoMemberPhoto(userPhoto));

        }

        [HttpDelete("DeleteAlbumPhoto/{photoId}")]
        public async Task<ActionResult> DeleteAlbumPhoto(int photoId)
        {
            var user = await _userService.GetByUsernameAsync(User.GetUsername());
            var appUser = await _userService.GetUserByIdAsync(user.Id);

            var photo = appUser.TheUserPhotosList.FirstOrDefault(x => x.Id == photoId);
            if (photo == null) return NotFound();

            if (photo.PublicId != null)
            {
                var result = await _photoServiceAPI.RemovePhotoAsyncAPI(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            await _userPhotoService.RemoveAsync(photo);
            await _userService.SaveChangesAsync();

            return Ok();
        }
    }
}

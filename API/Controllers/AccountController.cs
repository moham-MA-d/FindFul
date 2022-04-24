using System.Threading.Tasks;
using DTO.Account;
using API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using API.Services.Interfaces;
using Core.IService.User;
using Extentions.Common;
using DTO._Enumarations;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AccountController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("register")]
        // If paramaters have sent in a body of the request we need Object to recieve them (Findful.DTOs) not just string parameters
        public async Task<ActionResult<UserSessionDTO>> Register(RegisterDTO register)
        {
           
            if (await _userService.GetByUsernameAsync(register.UserName) != null)
                return BadRequest("Username is Taken");

            var user = _userService.CreateAppUserForRegisteration(register);
            
            await _userService.AddAsync(user);

            return new UserSessionDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user.UserName, user.Id),
                Gender = (UserEnums.Gender)user.Gender,
                Sex = (UserEnums.Sex)user.Sex
            };
        }



        [HttpPost("Login")]
        public async Task<ActionResult<UserSessionDTO>> Login(LoginDTO loginDTO)
        {
            if (loginDTO.UserName.IsNullOrEmptyOrWhiteSpace())
                return BadRequest("Username or Emnail is empty");

            var inputType = _userService.CheckUserInputForLogin(loginDTO.UserName);
            
            var user = new MemberDTO();
            switch (inputType)
            {
                case UserEnums.LoginInputType.Email:
                    user = await _userService.GetByEmail(loginDTO.UserName);
                    break;
                case UserEnums.LoginInputType.Phone:
                    user = await _userService.GetByUsernameAsync(loginDTO.UserName);
                    break;
                case UserEnums.LoginInputType.Username:
                case UserEnums.LoginInputType.None:
                    user = await _userService.GetByUsernameAsync(loginDTO.UserName);
                    break;
            }
            
            if (user == null)
                return Unauthorized("Invalid Username");

            var isPasswordCurrect = await _userService.IsPasswordCurrect(user.Id, loginDTO.Password);
            if(!isPasswordCurrect)
                return Unauthorized("Invalid Password");

            return new UserSessionDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user.UserName, user.Id),
                PhotoUrl = user.ProfilePhotoUrl,
                Gender = user.Gender,
                Sex = user.Sex
            };
        }

    }
}
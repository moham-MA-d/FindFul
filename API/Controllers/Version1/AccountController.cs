using System.Threading.Tasks;
using API.Controllers.Version1.Base;
using API.Services.Interfaces;
using Core.IServices.User;
using Core.Models.Entities.User;
using DTO.Account;
using DTO.Enumerations;
using Extensions.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Version1
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(ITokenService tokenService, IUserService userService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _tokenService = tokenService;
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        // If parameters have sent in a body of the request we need Object to receive them (.DTOs) not just string parameters
        public async Task<ActionResult<UserSessionDTO>> Register(RegisterDTO registerDTO)
        {
           
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDTO.UserName.ToLower()))
                return BadRequest("Username is Taken");

            var user = _userService.CreateAppUserForRegisteration(registerDTO);
            
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded) return BadRequest(result.Errors);



            return new UserSessionDTO
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Gender = (UserEnums.Gender)user.Gender,
                Sex = (UserEnums.Sex)user.Sex,
                Id = user.Id
            };
        }



        [HttpPost("Login")]
        public async Task<ActionResult<UserSessionDTO>> Login(LoginDTO loginDTO)
        {
            if (loginDTO.UserName.IsNullOrEmptyOrWhiteSpace())
                return BadRequest("Username or Email is empty");

            //for example: email, phone, username
            var inputType = _userService.DetectUserInputTypeForLogin(loginDTO.UserName); 
            
            var user = new AppUser();
            switch (inputType)
            {
                case UserEnums.LoginInputType.Email:
                    user =  await _userManager.Users.SingleOrDefaultAsync(x => x.Email == loginDTO.UserName);
                    break;
                case UserEnums.LoginInputType.Phone:
                    user = await _userManager.Users.SingleOrDefaultAsync(x => x.Phone == loginDTO.UserName);
                    break;
                case UserEnums.LoginInputType.Username:
                case UserEnums.LoginInputType.None:
                    user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.UserName);
                    break;
            }
            
            if (user == null) return Unauthorized("Invalid Username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserSessionDTO
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.ProfilePhotoUrl,
                Gender = (UserEnums.Gender)user.Gender,
                Sex = (UserEnums.Sex)user.Sex,
                Id = user.Id,
            };
        }

    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.Version1.Base;
using API.Extensions;
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
        private readonly ITokenServiceApi _tokenServiceApi;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(ITokenServiceApi tokenServiceApi, IUserService userService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _tokenServiceApi = tokenServiceApi;
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        // If parameters have sent in a body of the request we need Object to receive them (.DTOs) not just string parameters
        public async Task<ActionResult<DtoAuthenticationResult>> Register(DtoRegister dtoRegister)
        {
           
            if (await _userManager.Users.AnyAsync(x => x.UserName == dtoRegister.UserName.ToLower()))
                return BadRequest("Username is Taken");

            var user = _userService.CreateAppUserForRegistration(dtoRegister);
            
            var result = await _userManager.CreateAsync(user, dtoRegister.Password);

            if (!result.Succeeded) return BadRequest(result.Errors.Select(x => x.Description));

            var roleResult = await _userManager.AddToRoleAsync(user, "DtoMember");
            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return await _tokenServiceApi.CreateTokenAsync(user);

        }



        [HttpPost("Login")]
        public async Task<ActionResult<DtoAuthenticationResult>> Login(DtoLogin dtoLogin)
        {

            if (dtoLogin.UserName.IsNullOrEmptyOrWhiteSpace())
                return BadRequest("Username or Email is empty");

            //for example: email, phone, username
            var inputType = _userService.DetectUserInputTypeForLogin(dtoLogin.UserName); 
            
            AppUser user;
            switch (inputType)
            {
                case UserEnums.LoginInputType.Email:
                    user =  await _userManager.Users.SingleOrDefaultAsync(x => x.Email == dtoLogin.UserName);
                    break;
                case UserEnums.LoginInputType.Phone:
                    user = await _userManager.Users.SingleOrDefaultAsync(x => x.Phone == dtoLogin.UserName);
                    break;
                case UserEnums.LoginInputType.Username:
                case UserEnums.LoginInputType.None:
                    user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == dtoLogin.UserName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (user == null) return Unauthorized("Invalid Username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dtoLogin.Password, false);

            if (!result.Succeeded) return Unauthorized();

            var token = await _tokenServiceApi.CreateTokenAsync(user);
            return Ok(token);
        }



        [HttpPost("Refresh")]
        public async Task<ActionResult<DtoAuthenticationResult>> Refresh(DtoRefreshToken refreshToken)
        {
            var result = await _tokenServiceApi.RefreshTokenAsync(refreshToken.Token, refreshToken.RefreshToken);

            return Ok(result);
        }
    }
}
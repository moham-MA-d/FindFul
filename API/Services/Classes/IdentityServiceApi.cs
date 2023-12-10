using API.Services.Interfaces;
using Core.Models.Entities.User;
using DTO.Account;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace API.Services.Classes
{
    public class IdentityServiceApi : IIdentityServiceApi
    {
        private readonly IFacebookService _facebookService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServiceApi _tokenServiceApi;

        public IdentityServiceApi(IFacebookService facebookService, ITokenServiceApi tokenServiceApi, UserManager<AppUser> userManager)
        {
            _facebookService = facebookService;
            _tokenServiceApi = tokenServiceApi;
            _userManager = userManager;
        }

        public async Task<DtoAuthenticationResult> LoginWithFacebookAsync(string accessToken)
        {

            var validationTokenResult = await _facebookService.ValidateAccessTokenAsync(accessToken);
            if (!validationTokenResult.Data.IsValid)
            {
                return new DtoAuthenticationResult
                {
                    Errors = new[] { "Invalid Facebook Token!" }
                };
            }

            var userInfo = await _facebookService.GetUserInfoAsync(accessToken);

            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var newUser = new AppUser
                {
                    Email = userInfo.Email,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    UserName = userInfo.FirstName + userInfo.LastName,
                    NormalizedUserName = userInfo.FirstName.ToUpper() + userInfo.LastName.ToUpper(),
                    ProfilePhotoUrl = userInfo.Picture.Data.Url.ToString()
                };

                var createdResult = await _userManager.CreateAsync(newUser);
                if (!createdResult.Succeeded)
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        return new DtoAuthenticationResult
                        {
                            Errors = new[] { createdResult.Errors.ToString() }
                        };
                    }
                    else
                    {
                        return new DtoAuthenticationResult
                        {
                            Errors = new[] { "Something went wrong, try again!" }
                        };

                    }
                }

                return await _tokenServiceApi.CreateTokenAsync(newUser);

            }

            return await _tokenServiceApi.CreateTokenAsync(user);
        }
    }
}

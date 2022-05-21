using System.Security.Claims;
using System.Threading.Tasks;
using Core.Models.Entities.User;
using DTO.Account;

namespace API.Services.Interfaces
{
    public interface ITokenServiceApi
    {
        public Task<DtoAuthenticationResult> CreateTokenAsync(AppUser user);

        ClaimsPrincipal GetClaimsPrincipalFromToken(string token);

        public Task<DtoAuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}

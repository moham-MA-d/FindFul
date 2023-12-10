using DTO.SocialNetworks;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IFacebookService
    {
        Task<FacebookTokenValidation> ValidateAccessTokenAsync(string accessToken);
        Task<FacebookUserInfo> GetUserInfoAsync(string accessToken);
    }
}

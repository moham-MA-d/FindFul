using System.Threading.Tasks;
using DTO.SocialNetworks;

namespace Core.IServices.SocialNetworks
{
    public interface IFacebookService
    {
        Task<FacebookTokenValidation> ValidateAccessTokenAsync(string accessToken);
        Task<FacebookUserInfo> GetUserInfoAsync(string accessToken);
    }
}

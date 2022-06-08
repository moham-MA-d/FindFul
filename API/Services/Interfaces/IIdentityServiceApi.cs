using DTO.Account;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IIdentityServiceApi
    {
        Task<DtoAuthenticationResult> LoginWithFacebookAsync(string accessToken);
    }
}

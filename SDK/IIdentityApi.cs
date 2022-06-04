using System.Threading.Tasks;
using DTO.Account;
using Refit;

namespace SDK
{
    public interface IIdentityApi
    {
        [Post("/api/V1/Account/Register")]
        Task<ApiResponse<DtoAuthenticationResult>> RegisterAsync([Body] DtoRegister dtoRegister);

        [Post("/api/v1/account/login")]
        Task<ApiResponse<DtoAuthenticationResult>> LoginAsync([Body] DtoLogin dtoLogin);

        [Post("/api/v1/account/refresh")]
        Task<ApiResponse<DtoAuthenticationResult>> RefreshAsync([Body] DtoRefreshToken dtoRefreshToken);
    }
}

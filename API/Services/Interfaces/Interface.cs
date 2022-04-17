
using DTO.Account;

namespace API.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(string username, int userId);
    }
}

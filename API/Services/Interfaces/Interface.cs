
using System.Threading.Tasks;
using Core.Models.Entities.User;
using DTO.Account;

namespace API.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<string> CreateToken(AppUser user);
    }
}

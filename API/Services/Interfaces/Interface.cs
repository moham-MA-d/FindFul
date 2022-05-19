
using System.Threading.Tasks;
using Core.Models.Entities.User;

namespace API.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<string> CreateToken(AppUser user);
    }
}

using DTO.Account;
using Core.Models.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepositories.User
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        Task<MemberDTO> GetUserByEmailAsync(string email);
        MemberDTO GetUserByUsername(string username);
        Task<MemberDTO> GetUserByUsernameAsync(string username);
        Task<IEnumerable<MemberDTO>> GetAllMembers();
        Task<AppUser> GetUserByIdAsync(int id);
    }
}

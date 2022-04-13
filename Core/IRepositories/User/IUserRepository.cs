using DTO.Account;
using Core.Models.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Pagination;

namespace Core.IRepositories.User
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        Task<MemberDTO> GetUserByEmailAsync(string email);
        MemberDTO GetUserByUsername(string username);
        Task<MemberDTO> GetUserByUsernameAsync(string username);
        Task<PagedListBase<MemberDTO>> GetAllMembers(PageParameters pageParameters);
        Task<AppUser> GetUserByIdAsync(int id);
    }
}

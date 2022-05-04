using DTO.Account;
using Core.Models.Entities.User;
using System.Threading.Tasks;
using DTO.Pagination;
using System.Linq;

namespace Core.IRepositories.User
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        Task<MemberDTO> GetUserByEmailAsync(string email);
        MemberDTO GetUserByUsername(string username);
        Task<MemberDTO> GetMemberByUsernameAsync(string username);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<PagedListBase<MemberDTO>> GetAllMembers(UserParameters userParameters);
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByIdAsync(int id, IQueryable<AppUser> query);
    }
}

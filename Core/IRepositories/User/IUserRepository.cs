using DTO.Account;
using Core.Models.Entities.User;
using System.Threading.Tasks;
using DTO.Pagination;
using System.Linq;

namespace Core.IRepositories.User
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        Task<DtoMember> GetUserByEmailAsync(string email);
        DtoMember GetUserByUsername(string username);
        Task<DtoMember> GetMemberByUsernameAsync(string username);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<PagedListBase<DtoMember>> GetAllMembers(UserParameters userParameters);
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByIdAsync(int id, IQueryable<AppUser> query);
    }
}

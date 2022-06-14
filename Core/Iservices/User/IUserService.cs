using System.Security.Claims;
using System.Threading.Tasks;
using Core.Models.Entities.User;
using DTO.Account;
using DTO.Pagination;
using static DTO.Enumerations.UserEnums;

namespace Core.IServices.User
{
    public interface IUserService : IEntityService<AppUser>
    {

        Task<DtoMember> GetByEmailAsync(string email);
        DtoMember GetByUsername(string username);
        Task<DtoMember> GetByUsernameAsync(string username);
        Task<PagedListBase<DtoMember>> GetAllMembers(UserParameters userParameters);
        Task<AppUser> GetUserByIdAsync(int id);

        Task<bool> IsPasswordCorrect(int userId, string password);
        AppUser CreateAppUserForRegistration(DtoRegister dtoRegister);
        LoginInputType DetectUserInputTypeForLogin(string input);
        bool IsInputEmail(string email);
        bool IsInputPhone(string phone);

    }
}

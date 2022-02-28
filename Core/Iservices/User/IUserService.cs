using DTO.Account;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models.Entities.User;
using static DTO.Enumarations.UserEmums;

namespace Core.IService.User
{
    public interface IUserService : IEntityService<AppUser>
    {
        Task<MemberDTO> GetByEmail(string email);
        Task<MemberDTO> GetByUsername(string username);
        Task<IEnumerable<MemberDTO>> GetAllMembers();

        Task<AppUser> Update(AppUser userToBeUpdated, AppUser user);

        Task<bool> IsPasswordCurrect(int userId, string password);
        AppUser CreateAppUserForRegisteration(RegisterDTO registerDTO);
        LoginInputType CheckUserInputForLogin(string input);
        bool IsInputEmail(string email);
        bool IsInputPhone(string phone);
        
    }
}

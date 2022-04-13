using DTO.Account;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models.Entities.User;
using static DTO.Enumarations.UserEmums;
using DTO.Pagination;

namespace Core.IService.User
{
    public interface IUserService : IEntityService<AppUser>
    {
        Task<MemberDTO> GetByEmail(string email);
        MemberDTO GetByUsername(string username);
        Task<MemberDTO> GetByUsernameAsync(string username);
        Task<PagedListBase<MemberDTO>> GetAllMembers(PageParameters pageParameters);
        Task<AppUser> GetUserByIdAsync(int id);

        Task<bool> IsPasswordCurrect(int userId, string password);
        AppUser CreateAppUserForRegisteration(RegisterDTO registerDTO);
        LoginInputType CheckUserInputForLogin(string input);
        bool IsInputEmail(string email);
        bool IsInputPhone(string phone);

        string GenerateRandomUsername(string name);
        
    }
}

﻿using System.Threading.Tasks;
using Core.Models.Entities.User;
using DTO.Account;
using DTO.Pagination;
using static DTO.Enumerations.UserEnums;

namespace Core.IServices.User
{
    public interface IUserService : IEntityService<AppUser>
    {
        Task<MemberDTO> GetByEmail(string email);
        MemberDTO GetByUsername(string username);
        Task<MemberDTO> GetByUsernameAsync(string username);
        Task<PagedListBase<MemberDTO>> GetAllMembers(UserParameters userParameters);
        Task<AppUser> GetUserByIdAsync(int id);

        Task<bool> IsPasswordCurrect(int userId, string password);
        AppUser CreateAppUserForRegisteration(RegisterDTO registerDTO);
        LoginInputType DetectUserInputTypeForLogin(string input);
        bool IsInputEmail(string email);
        bool IsInputPhone(string phone);

        string GenerateRandomUsername(string name);
        
    }
}

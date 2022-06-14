using System;
using System.Threading.Tasks;
using Core.Models.Entities.User;
using DTO.Account;
using Core;
using Core.IRepositories.User;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Security.Claims;
using Core.IServices;
using Core.IServices.User;
using DTO.Pagination;
using Microsoft.AspNetCore.Identity;
using static DTO.Enumerations.UserEnums;

namespace Service.Classes.User
{
    public class UserService : EntityService<AppUser>, IUserService, IEntityService<AppUser>
    {
        readonly IUserRepository _userRepository;
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, userRepository)
        {
            _userRepository = userRepository;
        }

        public new async Task<AppUser> AddAsync(AppUser entity)
        {
            if (entity.UserName == "")
            {
                return null;
            }
            await _userRepository.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }
        
        public async Task<DtoMember> GetByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }
        public DtoMember GetByUsername(string username)
        {
            return  _userRepository.GetUserByUsername(username);
        }
        public async Task<DtoMember> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetMemberByUsernameAsync(username);
        }
        public async Task<PagedListBase<DtoMember>> GetAllMembers(UserParameters userParameters)
        {
            return await _userRepository.GetAllMembers(userParameters);
        }
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<bool> IsPasswordCorrect(int userId, string password)
        {
            var user = await GetByIdAsync(userId);

            return true;
        }
        public AppUser CreateAppUserForRegistration(DtoRegister dtoRegister)
        {

            var user = new AppUser
            {
                UserName = dtoRegister.UserName,
                Email= dtoRegister.Email,
                ProfilePhotoUrl = "/assets/images/user.png"
            };

            return user;
        }
        public LoginInputType DetectUserInputTypeForLogin(string input)
        {
            if (IsInputEmail(input))
                return LoginInputType.Email;
            
            if (IsInputPhone(input))
                return LoginInputType.Phone;

            return LoginInputType.Username;
        }
        
        public bool IsInputEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                if (!Debugger.IsAttached)
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == trimmedEmail;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool IsInputPhone(string phone)
        {
            return Regex.Match(phone, @"^(\+[0-9]{9})$").Success;
        }

        private string GenerateRandomUsername(string name)
        {
            return name + new Random(11).ToString();
        }

     
    }
}

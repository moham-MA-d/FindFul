using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Entities.User;
using Core.IService;
using DTO.Account;
using Core.IService.User;
using Core;
using Core.IRepositories.User;
using System.Text.RegularExpressions;
using System.Diagnostics;
using DTO.Pagination;
using static DTO._Enumarations.UserEnums;

namespace Service.Classes.User
{
    public class UserService : EntityService<AppUser>, IUserService, IEntityService<AppUser>
    {
        IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<MemberDTO> GetByEmail(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }
        public MemberDTO GetByUsername(string username)
        {
            return  _userRepository.GetUserByUsername(username);
        }
        public async Task<MemberDTO> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetMemberByUsernameAsync(username);
        }
        public async Task<PagedListBase<MemberDTO>> GetAllMembers(UserParameters userParameters)
        {
            return await _userRepository.GetAllMembers(userParameters);
        }
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<bool> IsPasswordCurrect(int userId, string password)
        {
            var user = await GetByIdAsync(userId);

            return true;
        }
        public AppUser CreateAppUserForRegisteration(RegisterDTO registerDTO)
        {

            var user = new AppUser
            {
                UserName = registerDTO.UserName,
            };

            return user;
        }
        public LoginInputType CheckUserInputForLogin(string input)
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

        public string GenerateRandomUsername(string name)
        {
            return name + new Random(11).ToString();
        }

     
    }
}

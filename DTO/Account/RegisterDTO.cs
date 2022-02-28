using System.ComponentModel.DataAnnotations;

namespace DTO.Account
{
    public class RegisterDTO : Base.BaseAccountDTO
    {
        public string Phone { get; set; }
    }
}
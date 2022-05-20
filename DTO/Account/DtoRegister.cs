using System.ComponentModel.DataAnnotations;

namespace DTO.Account
{
    public class DtoRegister : Base.DtoBaseAccount
    {
        [Required]
        public string Email { get; set; }
    }
}
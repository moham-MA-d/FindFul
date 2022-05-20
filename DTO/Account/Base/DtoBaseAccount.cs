using System.ComponentModel.DataAnnotations;

namespace DTO.Account.Base
{
    public class DtoBaseAccount
    {
        private string _username;

        [Required]
        public string UserName
        {
            get => _username;
            set => _username = value.ToLower();
        }

        [Required]
        [StringLength(22, MinimumLength = 5)]
        [ScaffoldColumn(false)]
        public string Password { get; set; }

    }
}

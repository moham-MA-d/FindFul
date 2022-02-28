using System.ComponentModel.DataAnnotations;

namespace DTO.Account.Base
{
    public class BaseAccountDTO
    {
        private string username;

        [Required]
        public string UserName
        {
            get { return username; }
            set { username = value.ToLower(); }
        }

        [Required]
        [StringLength(22, MinimumLength = 5)]
        [ScaffoldColumn(false)]
        public string Password { get; set; }

    }
}

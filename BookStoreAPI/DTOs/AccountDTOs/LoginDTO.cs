using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.AccountDTOs
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

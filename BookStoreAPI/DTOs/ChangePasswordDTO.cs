using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Password and confirm password must be the same.")]
        public string ConfirmPassword { get; set; }
    }
}

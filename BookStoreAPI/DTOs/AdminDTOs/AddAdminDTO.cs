using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.AdminDTOs
{
    public class AddAdminDTO
    {
        [Required(ErrorMessage = "User name can't be empty")]
        [StringLength(20)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password can't be empty")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be 8-15 characters long and contain at least one lowercase letter, one uppercase letter, one digit and one special character.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password can't be empty")]
        [Compare("Password", ErrorMessage = "Password and confirm password must be the same.")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number can't be empty")]
        //[RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
    }
}

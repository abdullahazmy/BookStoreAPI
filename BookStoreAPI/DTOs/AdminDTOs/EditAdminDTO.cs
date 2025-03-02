using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.AdminDTOs
{
    public class EditAdminDTO
    {
        [Required(ErrorMessage = "Id can't be empty")]
        public string Id { get; set; }

        [Required(ErrorMessage = "User name can't be empty")]
        [StringLength(20)]
        public string UserName { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number can't be empty")]
        public string PhoneNumber { get; set; }
    }
}

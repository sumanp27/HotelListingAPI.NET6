using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Models.Users
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "your password is limited to few characters", MinimumLength = 6)]
        public string Password { get; set; }
    }

}

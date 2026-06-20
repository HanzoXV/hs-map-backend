using System.ComponentModel.DataAnnotations;

namespace hs_map_api.Models.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;
    }
}

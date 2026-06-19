using System.ComponentModel.DataAnnotations;

namespace hs_map_backend.Models.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [StringLength(100)]
        public string? DisplayName { get; set; }

        [StringLength(10)]
        public string? PreferredLanguage { get; set; }
    }
}


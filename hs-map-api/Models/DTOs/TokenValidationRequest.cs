using System.ComponentModel.DataAnnotations;

namespace hs_map_api.Models.DTOs
{
    public class TokenValidationRequest
    {
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}

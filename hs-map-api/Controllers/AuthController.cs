using BCrypt.Net;
using hs_map_api.Models.DTOs;
using hs_map_backend.Data;
using hs_map_backend.Models;
using hs_map_backend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace hs_map_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AuthController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            bool emailExists = await _db.Users
                .AnyAsync(u => u.Email.ToLower() == dto.Email.ToLower());

            if (emailExists)
            {
                return BadRequest(new { message = "An account with this email already exists." });
            }

            var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(dto.Password);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email.ToLower().Trim(),
                DisplayName = dto.DisplayName?.Trim(),
                PreferredLanguage = string.IsNullOrWhiteSpace(dto.PreferredLanguage) ? "URDU" : dto.PreferredLanguage.ToUpper(),
                PasswordHash = hashedPassword,
            };

            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                message = "Registration successful.",
                userId = newUser.Id,
                email = newUser.Email,
                displayName = newUser.DisplayName,
                preferredLanguage = newUser.PreferredLanguage
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());

            if (user == null)
                return BadRequest(new { message = "Invalid email or password." });

            if (!BCrypt.Net.BCrypt.EnhancedVerify(dto.Password, user.PasswordHash))
                return BadRequest(new { message = "Invalid email or password." });

            return Ok(new
            {
                message = "Login successful.",
                userId = user.Id,
                email = user.Email,
                displayName = user.DisplayName,
                preferredLanguage = user.PreferredLanguage
            });
        }
    }
}

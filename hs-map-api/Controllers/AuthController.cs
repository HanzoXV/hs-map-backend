using hs_map_backend.Data;
using hs_map_backend.Models;
using hs_map_backend.Models.DTOs;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;


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
                SecurityStamp = Guid.NewGuid().ToString()
            };

            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                message = "Registration successful.",
                userId = newUser.Id
            });
        }
    }
}

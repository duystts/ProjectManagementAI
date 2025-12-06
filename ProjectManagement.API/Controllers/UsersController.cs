using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.API.Data;
using ProjectManagement.API.Models;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.FullName,
                    u.Role,
                    u.CreatedAt
                })
                .ToListAsync();
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            
            if (user.Username == "admin")
                return BadRequest("Cannot edit admin user");

            // Prevent promoting another user to Admin if an admin already exists
            if (request.Role == Models.Enums.UserRole.Admin)
            {
                var existingAdmin = await _context.Users.FirstOrDefaultAsync(u => u.Role == Models.Enums.UserRole.Admin);
                if (existingAdmin != null && existingAdmin.Id != id)
                {
                    return BadRequest("An admin account already exists. Cannot promote another user to Admin.");
                }
            }

            user.FullName = request.FullName;
            user.Role = request.Role;
            
            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<object>> CreateUser(CreateUserRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                return BadRequest("Username already exists");

            // Prevent creating more than one Admin
            if (request.Role == Models.Enums.UserRole.Admin)
            {
                var existingAdmin = await _context.Users.FirstOrDefaultAsync(u => u.Role == Models.Enums.UserRole.Admin);
                if (existingAdmin != null)
                {
                    return BadRequest("An admin account already exists. Cannot create another admin.");
                }
            }

            var user = new User
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FullName = request.FullName,
                Role = request.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.Username,
                user.FullName,
                user.Role,
                user.CreatedAt
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            
            if (user.Username == "admin")
                return BadRequest("Cannot delete admin user");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class CreateUserRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public Models.Enums.UserRole Role { get; set; }
    }

    public class UpdateUserRequest
    {
        public string FullName { get; set; } = string.Empty;
        public Models.Enums.UserRole Role { get; set; }
        public string? NewPassword { get; set; }
    }
}

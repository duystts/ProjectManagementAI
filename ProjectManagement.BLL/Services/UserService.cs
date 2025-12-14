using ProjectManagement.BLL.Interfaces;
using ProjectManagement.DAL.Interfaces;
using ProjectManagement.Entities.Models;
using ProjectManagement.Entities.Models.DTOs;
using ProjectManagement.Entities.Models.Enums;

namespace ProjectManagement.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                FullName = u.FullName,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            });
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserDto?> CreateUserAsync(CreateUserRequest request)
        {
            if (await _userRepository.UsernameExistsAsync(request.Username))
                return null;

            // Prevent creating more than one Admin
            if (request.Role == UserRole.Admin)
            {
                var users = await _userRepository.GetAllAsync();
                var existingAdmin = users.FirstOrDefault(u => u.Role == UserRole.Admin);
                if (existingAdmin != null)
                    return null;
            }

            var user = new User
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FullName = request.FullName,
                Role = request.Role
            };

            var createdUser = await _userRepository.AddAsync(user);

            return new UserDto
            {
                Id = createdUser.Id,
                Username = createdUser.Username,
                FullName = createdUser.FullName,
                Role = createdUser.Role,
                CreatedAt = createdUser.CreatedAt
            };
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            if (user.Username == "admin")
                return false;

            // Prevent promoting another user to Admin if an admin already exists
            if (request.Role == UserRole.Admin)
            {
                var users = await _userRepository.GetAllAsync();
                var existingAdmin = users.FirstOrDefault(u => u.Role == UserRole.Admin);
                if (existingAdmin != null && existingAdmin.Id != id)
                    return false;
            }

            user.FullName = request.FullName;
            user.Role = request.Role;
            
            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            }

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;
            
            if (user.Username == "admin")
                return false;

            await _userRepository.DeleteAsync(user);
            return true;
        }

        public async Task<bool> ResetPasswordAsync(int userId, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
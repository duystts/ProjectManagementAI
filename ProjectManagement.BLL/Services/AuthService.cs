using ProjectManagement.BLL.Interfaces;
using ProjectManagement.DAL.Interfaces;
using ProjectManagement.Entities.Models;
using ProjectManagement.Entities.Models.DTOs;
using BCrypt.Net;

namespace ProjectManagement.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role
            };
        }

        public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
        {
            if (await _userRepository.UsernameExistsAsync(request.Username))
                return null;

            var user = new User
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FullName = request.FullName,
                Role = request.Role
            };

            await _userRepository.AddAsync(user);

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role
            };
        }
    }
}
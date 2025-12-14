using ProjectManagement.Entities.Models.DTOs;

namespace ProjectManagement.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
    }
}
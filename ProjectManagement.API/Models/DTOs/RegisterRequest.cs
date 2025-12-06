using ProjectManagement.API.Models.Enums;

namespace ProjectManagement.API.Models.DTOs
{
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Viewer;
    }
}

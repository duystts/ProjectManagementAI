using ProjectManagement.Entities.Models.Enums;

namespace ProjectManagement.Entities.Models.DTOs
{
    public class CreateUserRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
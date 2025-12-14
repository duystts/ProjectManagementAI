using ProjectManagement.Entities.Models.Enums;

namespace ProjectManagement.Entities.Models.DTOs
{
    public class UpdateUserRequest
    {
        public string FullName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string? NewPassword { get; set; }
    }
}
namespace ProjectManagement.WinForms.Models
{
    public class UpdateUserRequest
    {
        public string FullName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string? NewPassword { get; set; }
    }
}

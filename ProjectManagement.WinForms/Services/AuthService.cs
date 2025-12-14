using ProjectManagement.Entities.Models.DTOs;

namespace ProjectManagement.WinForms.Services
{
    public static class AuthService
    {
        public static AuthResponse? CurrentUser { get; private set; }
        public static string? Token { get; private set; }

        public static void SetUser(AuthResponse user, string token)
        {
            CurrentUser = user;
            Token = token;
        }

        public static void Logout()
        {
            CurrentUser = null;
            Token = null;
        }

        public static bool IsLoggedIn => CurrentUser != null;
    }
}

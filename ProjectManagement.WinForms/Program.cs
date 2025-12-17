using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<LoginForm>());
        }

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient();
                    services.AddSingleton<ApiService>();
                    services.AddTransient<LoginForm>();
                    services.AddTransient<DashboardForm>();
                    services.AddTransient<ProjectForm>();
                    services.AddTransient<TaskForm>();
                    services.AddTransient<UserManagementForm>();
                    services.AddTransient<AddUserForm>();
                    services.AddTransient<EditUserForm>();
                    services.AddTransient<ResetPasswordForm>();
                    services.AddTransient<AIAssistantForm>();
                    services.AddTransient<Form1>();
                });
    }
}
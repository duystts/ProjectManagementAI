using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.WinForms.Models;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class LoginForm : Form
    {
        private readonly ApiService _apiService;

        public LoginForm(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter username and password", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Logging in...";

            try
            {
                var request = new LoginRequest
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text
                };

                var response = await _apiService.LoginAsync(request);

                if (response != null)
                {
                    AuthService.SetUser(response, response.Token);
                    var dashboardForm = Program.ServiceProvider?.GetRequiredService<DashboardForm>();
                    if (dashboardForm != null)
                    {
                        dashboardForm.FormClosed += (s, args) => this.Show();
                        dashboardForm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot connect to API server.\n\nError: {ex.Message}\n\nPlease ensure the API is running and the URL in appsettings.json is correct.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Login";
            }
        }


        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        // Ensure handler exists for designer
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Entities.Models.DTOs;
using ProjectManagement.Entities.Models.Enums;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class UserManagementForm : Form
    {
        private readonly ApiService _apiService;

        public UserManagementForm(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;

            // handle button column clicks
            dgvUsers.CellContentClick += DgvUsers_CellContentClick;
        }

        private async void UserManagementForm_Load(object sender, EventArgs e)
        {
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            try
            {
                var users = await _apiService.GetUsersAsync();
                dgvUsers.DataSource = users;

                // Set column widths safely
                if (dgvUsers.Columns.Contains("Id")) dgvUsers.Columns["Id"].Width = 50;
                if (dgvUsers.Columns.Contains("Username")) dgvUsers.Columns["Username"].Width = 150;
                if (dgvUsers.Columns.Contains("FullName")) dgvUsers.Columns["FullName"].Width = 200;
                if (dgvUsers.Columns.Contains("Role")) dgvUsers.Columns["Role"].Width = 100;
                if (dgvUsers.Columns.Contains("CreatedAt")) dgvUsers.Columns["CreatedAt"].Width = 150;

                // Add Password column (masked) if not already present
                if (!dgvUsers.Columns.Contains("Password"))
                {
                    var pwdCol = new DataGridViewTextBoxColumn
                    {
                        Name = "Password",
                        HeaderText = "Password",
                        ReadOnly = true,
                        Width = 150
                    };
                    dgvUsers.Columns.Add(pwdCol);
                }

                // Add Reset Password button column if not already present
                if (!dgvUsers.Columns.Contains("ResetPassword"))
                {
                    var btnCol = new DataGridViewButtonColumn
                    {
                        Name = "ResetPassword",
                        HeaderText = "Reset Password",
                        Text = "Reset",
                        UseColumnTextForButtonValue = true,
                        Width = 100
                    };
                    dgvUsers.Columns.Add(btnCol);
                }

                // Fill masked password values
                foreach (DataGridViewRow row in dgvUsers.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (dgvUsers.Columns.Contains("Password"))
                        {
                            row.Cells["Password"].Value = "������";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DgvUsers_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvUsers.Columns[e.ColumnIndex].Name != "ResetPassword") return;

            var user = dgvUsers.Rows[e.RowIndex].DataBoundItem as UserDto;
            if (user == null) return;

            // Do not allow resetting admin password from this UI
            if (user.Username == "admin")
            {
                MessageBox.Show("Resetting admin password is not allowed.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Reset other users to the initial default password based on role
            string defaultPassword;
            switch (user.Role)
            {
                case UserRole.Manager:
                    defaultPassword = "manager123";
                    break;
                case UserRole.Member:
                    defaultPassword = "member123";
                    break;
                case UserRole.Viewer:
                    defaultPassword = "viewer123";
                    break;
                default:
                    defaultPassword = "user123";
                    break;
            }

            var confirm = MessageBox.Show($"Reset password for '{user.Username}' to the default for their role?\n(This will set password to '{defaultPassword}')", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var success = await _apiService.ResetPasswordAsync(user.Id, defaultPassword);
                if (success)
                {
                    // Show the new password once (do not copy to clipboard)
                    MessageBox.Show($"Password for '{user.Username}' has been reset to: {defaultPassword}\n(Shown once)", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to reset password on server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error resetting password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var addForm = Program.ServiceProvider?.GetRequiredService<AddUserForm>();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                await LoadUsers();
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to edit", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = (UserDto)dgvUsers.SelectedRows[0].DataBoundItem;
            
            if (user.Username == "admin")
            {
                MessageBox.Show("Cannot edit admin user!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var editForm = Program.ServiceProvider?.GetRequiredService<EditUserForm>();
            editForm.LoadUser(user);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                await LoadUsers();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = (UserDto)dgvUsers.SelectedRows[0].DataBoundItem;
            
            if (user.Username == "admin")
            {
                MessageBox.Show("Cannot delete admin user!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (user.Username == AuthService.CurrentUser?.Username)
            {
                MessageBox.Show("You cannot delete yourself!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete user '{user.Username}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var success = await _apiService.DeleteUserAsync(user.Id);
                    if (success)
                    {
                        MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadUsers();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

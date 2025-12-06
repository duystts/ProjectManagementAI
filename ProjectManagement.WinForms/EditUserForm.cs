using ProjectManagement.WinForms.Models;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class EditUserForm : Form
    {
        private ApiService _apiService;
        private UserDto _user;

        public EditUserForm(UserDto user)
        {
            InitializeComponent();
            _apiService = new ApiService();
            _user = user;
        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            lblUsername.Text = $"Username: {_user.Username}";
            txtFullName.Text = _user.FullName;
            cmbRole.DataSource = Enum.GetValues(typeof(UserRole));
            cmbRole.SelectedItem = _user.Role;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full name is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var request = new UpdateUserRequest
                {
                    FullName = txtFullName.Text.Trim(),
                    Role = (UserRole)cmbRole.SelectedItem,
                    NewPassword = string.IsNullOrWhiteSpace(txtNewPassword.Text) ? null : txtNewPassword.Text
                };

                var success = await _apiService.UpdateUserAsync(_user.Id, request);
                if (success)
                {
                    MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

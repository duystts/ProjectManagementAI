using ProjectManagement.WinForms.Models;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class AddUserForm : Form
    {
        private ApiService _apiService;

        public AddUserForm()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            cmbRole.DataSource = Enum.GetValues(typeof(UserRole));
            cmbRole.SelectedIndex = 3; // Default to Viewer
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || 
                string.IsNullOrWhiteSpace(txtPassword.Text) || 
                string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please fill all fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var request = new CreateUserRequest
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text,
                    FullName = txtFullName.Text.Trim(),
                    Role = cmbRole.SelectedItem is UserRole role ? role : UserRole.Viewer
                };

                var result = await _apiService.CreateUserAsync(request);
                if (result != null)
                {
                    MessageBox.Show("User created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create user. Username may already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

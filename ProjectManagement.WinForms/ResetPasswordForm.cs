using System.Windows.Forms;

namespace ProjectManagement.WinForms
{
    public partial class ResetPasswordForm : Form
    {
        public string NewPassword { get; private set; } = string.Empty;

        public ResetPasswordForm(string username)
        {
            InitializeComponent();
            lblInfo.Text = $"Reset password for '{username}'";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                MessageBox.Show("Password cannot be empty", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNewPassword.Text != txtConfirm.Text)
            {
                MessageBox.Show("Passwords do not match", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NewPassword = txtNewPassword.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

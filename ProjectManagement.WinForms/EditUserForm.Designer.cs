namespace ProjectManagement.WinForms
{
    partial class EditUserForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblUsername;
        private TextBox txtFullName;
        private TextBox txtNewPassword;
        private ComboBox cmbRole;
        private Button btnSave;
        private Button btnCancel;
        private Label lblFullName;
        private Label lblNewPassword;
        private Label lblRole;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblUsername = new Label();
            txtFullName = new TextBox();
            txtNewPassword = new TextBox();
            cmbRole = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            lblFullName = new Label();
            lblNewPassword = new Label();
            lblRole = new Label();
            SuspendLayout();
            
            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsername.Location = new Point(20, 20);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(100, 19);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username: ";
            
            // lblFullName
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(20, 60);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(61, 15);
            lblFullName.TabIndex = 1;
            lblFullName.Text = "Full Name";
            
            // txtFullName
            txtFullName.Location = new Point(120, 57);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(250, 23);
            txtFullName.TabIndex = 2;
            
            // lblNewPassword
            lblNewPassword.AutoSize = true;
            lblNewPassword.Location = new Point(20, 100);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(84, 15);
            lblNewPassword.TabIndex = 3;
            lblNewPassword.Text = "New Password";
            
            // txtNewPassword
            txtNewPassword.Location = new Point(120, 97);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '●';
            txtNewPassword.PlaceholderText = "Leave empty to keep current";
            txtNewPassword.Size = new Size(250, 23);
            txtNewPassword.TabIndex = 4;
            
            // lblRole
            lblRole.AutoSize = true;
            lblRole.Location = new Point(20, 140);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(30, 15);
            lblRole.TabIndex = 5;
            lblRole.Text = "Role";
            
            // cmbRole
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(120, 137);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(250, 23);
            cmbRole.TabIndex = 6;
            
            // btnSave
            btnSave.Location = new Point(120, 180);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 30);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            
            // btnCancel
            btnCancel.Location = new Point(270, 180);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 30);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            
            // EditUserForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 230);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(cmbRole);
            Controls.Add(lblRole);
            Controls.Add(txtNewPassword);
            Controls.Add(lblNewPassword);
            Controls.Add(txtFullName);
            Controls.Add(lblFullName);
            Controls.Add(lblUsername);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "EditUserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit User";
            Load += EditUserForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

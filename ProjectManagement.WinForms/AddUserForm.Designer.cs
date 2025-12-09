namespace ProjectManagement.WinForms
{
    partial class AddUserForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtFullName;
        private ComboBox cmbRole;
        private Button btnSave;
        private Button btnCancel;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblFullName;
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
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            txtFullName = new TextBox();
            cmbRole = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            lblUsername = new Label();
            lblPassword = new Label();
            lblFullName = new Label();
            lblRole = new Label();
            SuspendLayout();
            
            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(20, 20);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(60, 15);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username";
            
            // txtUsername
            txtUsername.Location = new Point(120, 17);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(250, 23);
            txtUsername.TabIndex = 1;
            
            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(20, 60);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Password";
            
            // txtPassword
            txtPassword.Location = new Point(120, 57);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(250, 23);
            txtPassword.TabIndex = 3;
            
            // lblFullName
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(20, 100);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(61, 15);
            lblFullName.TabIndex = 4;
            lblFullName.Text = "Full Name";
            
            // txtFullName
            txtFullName.Location = new Point(120, 97);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(250, 23);
            txtFullName.TabIndex = 5;
            
            // lblRole
            lblRole.AutoSize = true;
            lblRole.Location = new Point(20, 140);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(30, 15);
            lblRole.TabIndex = 6;
            lblRole.Text = "Role";
            
            // cmbRole
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(120, 137);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(250, 23);
            cmbRole.TabIndex = 7;
            
            // btnSave
            btnSave.Location = new Point(120, 180);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 30);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            
            // btnCancel
            btnCancel.Location = new Point(270, 180);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 30);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            
            // AddUserForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 230);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(cmbRole);
            Controls.Add(lblRole);
            Controls.Add(txtFullName);
            Controls.Add(lblFullName);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AddUserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add New User";
            Load += AddUserForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

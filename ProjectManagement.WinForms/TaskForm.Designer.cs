namespace ProjectManagement.WinForms
{
    partial class TaskForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private TextBox txtTitle;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Label lblPriority;
        private ComboBox cmbPriority;
        private Button btnSave;
        private Button btnCancel;
        private Button btnDelete;

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
            this.lblTitle = new Label();
            this.txtTitle = new TextBox();
            this.lblDescription = new Label();
            this.txtDescription = new TextBox();
            this.lblStatus = new Label();
            this.cmbStatus = new ComboBox();
            this.lblPriority = new Label();
            this.cmbPriority = new ComboBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.btnDelete = new Button();
            this.SuspendLayout();
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(12, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(32, 15);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title:";
            
            // txtTitle
            this.txtTitle.Location = new Point(12, 33);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Size(360, 23);
            this.txtTitle.TabIndex = 1;
            
            // lblDescription
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new Point(12, 70);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(70, 15);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";
            
            // txtDescription
            this.txtDescription.Location = new Point(12, 88);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(360, 80);
            this.txtDescription.TabIndex = 3;
            
            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new Point(12, 185);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(42, 15);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status:";
            
            // cmbStatus
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatus.Location = new Point(12, 203);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new Size(170, 23);
            this.cmbStatus.TabIndex = 5;
            
            // lblPriority
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new Point(202, 185);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new Size(48, 15);
            this.lblPriority.TabIndex = 6;
            this.lblPriority.Text = "Priority:";
            
            // cmbPriority
            this.cmbPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPriority.Location = new Point(202, 203);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new Size(170, 23);
            this.cmbPriority.TabIndex = 7;
            
            // btnSave
            this.btnSave.Location = new Point(216, 245);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            
            // btnCancel
            this.btnCancel.Location = new Point(297, 245);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            
            // TaskForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(384, 281);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbPriority);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Task Form";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
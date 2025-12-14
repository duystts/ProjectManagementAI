namespace ProjectManagement.WinForms.Controls
{
    partial class TaskCardControl
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblPriority;
        private Label lblId;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnAssign;

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
            lblTitle = new Label();
            lblPriority = new Label();
            lblId = new Label();
            btnEdit = new Button();
            btnDelete = new Button();
            btnAssign = new Button();
            SuspendLayout();
            
            // TaskCardControl
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Size = new Size(260, 100);
            Click += TaskCardControl_Click;
            
            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitle.Location = new Point(8, 8);
            lblTitle.MaximumSize = new Size(200, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(38, 19);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Title";
            lblTitle.Click += Label_Click;
            
            // lblPriority
            lblPriority.AutoSize = true;
            lblPriority.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblPriority.Location = new Point(200, 8);
            lblPriority.Name = "lblPriority";
            lblPriority.Size = new Size(48, 13);
            lblPriority.TabIndex = 1;
            lblPriority.Text = "Priority";
            lblPriority.Click += Label_Click;
            
            // lblId
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 7F);
            lblId.ForeColor = Color.Gray;
            lblId.Location = new Point(8, 78);
            lblId.Name = "lblId";
            lblId.Size = new Size(17, 12);
            lblId.TabIndex = 2;
            lblId.Text = "#1";
            lblId.Click += Label_Click;
            
            // btnEdit
            btnEdit.Location = new Point(120, 75);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(35, 20);
            btnEdit.TabIndex = 3;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += BtnEdit_Click;
            
            // btnDelete
            btnDelete.Location = new Point(160, 75);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(35, 20);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Del";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += BtnDelete_Click;
            
            // btnAssign
            btnAssign.Location = new Point(200, 75);
            btnAssign.Name = "btnAssign";
            btnAssign.Size = new Size(45, 20);
            btnAssign.TabIndex = 5;
            btnAssign.Text = "Assign";
            btnAssign.UseVisualStyleBackColor = true;
            btnAssign.Click += BtnAssign_Click;
            
            Controls.Add(lblTitle);
            Controls.Add(lblPriority);
            Controls.Add(lblId);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(btnAssign);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
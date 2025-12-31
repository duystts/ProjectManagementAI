namespace ProjectManagement.WinForms.Controls
{
    partial class TaskCardControl
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblPriority;
        private Label lblId;
        private Label lblDeadline;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnAssign;
        private Button btnAttachments;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblPriority = new Label();
            lblId = new Label();
            lblDeadline = new Label();
            btnEdit = new Button();
            btnDelete = new Button();
            btnAssign = new Button();
            btnAttachments = new Button();
            SuspendLayout();
            
            // TaskCardControl
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            // BorderStyle handled in OnPaint
            Size = new Size(260, 110); // Increased height
            Click += TaskCardControl_Click;
            
            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitle.Location = new Point(8, 8);
            lblTitle.MaximumSize = new Size(180, 40); // Allow 2 lines max
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(38, 19);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Title";
            lblTitle.Click += Label_Click;
            
            // lblPriority
            lblPriority.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPriority.AutoSize = true;
            lblPriority.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblPriority.Location = new Point(200, 8);
            lblPriority.Name = "lblPriority";
            lblPriority.Size = new Size(48, 13);
            lblPriority.TabIndex = 1;
            lblPriority.Text = "Priority";
            lblPriority.Click += Label_Click;
            
            // lblId
            lblId.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 7F);
            lblId.ForeColor = Color.Gray;
            lblId.Location = new Point(8, 88);
            lblId.Name = "lblId";
            lblId.Size = new Size(17, 12);
            lblId.TabIndex = 2;
            lblId.Text = "#1";
            lblId.Click += Label_Click;

            // lblDeadline
            lblDeadline.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblDeadline.AutoSize = true;
            lblDeadline.Font = new Font("Segoe UI", 8F);
            lblDeadline.ForeColor = Color.Gray;
            lblDeadline.Location = new Point(8, 60);
            lblDeadline.MaximumSize = new Size(200, 0);
            lblDeadline.Name = "lblDeadline";
            lblDeadline.Size = new Size(0, 13);
            lblDeadline.TabIndex = 6;
            lblDeadline.Click += Label_Click;
            
            // btnEdit
            btnEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEdit.Location = new Point(85, 85);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(35, 20);
            btnEdit.TabIndex = 3;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += BtnEdit_Click;
            
            // btnDelete
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDelete.Location = new Point(125, 85);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(35, 20);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Del";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += BtnDelete_Click;
            
            // btnAssign
            btnAssign.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAssign.Location = new Point(165, 85);
            btnAssign.Name = "btnAssign";
            btnAssign.Size = new Size(45, 20);
            btnAssign.TabIndex = 5;
            btnAssign.Text = "Assign";
            btnAssign.UseVisualStyleBackColor = true;
            btnAssign.Click += BtnAssign_Click;
            
            // btnAttachments
            btnAttachments.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAttachments.Location = new Point(215, 85);
            btnAttachments.Name = "btnAttachments";
            btnAttachments.Size = new Size(30, 20);
            btnAttachments.TabIndex = 6;
            btnAttachments.Text = "📎";
            btnAttachments.UseVisualStyleBackColor = true;
            btnAttachments.Click += BtnAttachments_Click;
            
            Controls.Add(lblTitle);
            Controls.Add(lblPriority);
            Controls.Add(lblId);
            Controls.Add(lblDeadline);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(btnAssign);
            Controls.Add(btnAttachments);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
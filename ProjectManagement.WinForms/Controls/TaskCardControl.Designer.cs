namespace ProjectManagement.WinForms.Controls
{
    partial class TaskCardControl
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblPriority;
        private Label lblId;

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
            
            Controls.Add(lblTitle);
            Controls.Add(lblPriority);
            Controls.Add(lblId);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
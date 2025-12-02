namespace ProjectManagement.WinForms.Controls
{
    partial class ProjectCardControl
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblName;
        private Label lblDescription;
        private Label lblCreated;

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
            lblName = new Label();
            lblDescription = new Label();
            lblCreated = new Label();
            SuspendLayout();
            
            // ProjectCardControl
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Size = new Size(300, 80);
            Click += ProjectCardControl_Click;
            
            // lblName
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblName.Location = new Point(8, 8);
            lblName.MaximumSize = new Size(280, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(52, 21);
            lblName.TabIndex = 0;
            lblName.Text = "Name";

            
            // lblDescription
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9F);
            lblDescription.Location = new Point(8, 32);
            lblDescription.MaximumSize = new Size(280, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 1;
            lblDescription.Text = "Description";

            
            // lblCreated
            lblCreated.AutoSize = true;
            lblCreated.Font = new Font("Segoe UI", 8F);
            lblCreated.ForeColor = Color.Gray;
            lblCreated.Location = new Point(8, 58);
            lblCreated.Name = "lblCreated";
            lblCreated.Size = new Size(60, 13);
            lblCreated.TabIndex = 2;
            lblCreated.Text = "01/01/2024";

            
            Controls.Add(lblName);
            Controls.Add(lblDescription);
            Controls.Add(lblCreated);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
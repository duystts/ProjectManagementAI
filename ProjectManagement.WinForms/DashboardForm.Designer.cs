namespace ProjectManagement.WinForms
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                _apiService?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnAddProject = new Button();
            flpProjects = new FlowLayoutPanel();
            SuspendLayout();
            
            // btnAddProject
            btnAddProject.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddProject.Location = new Point(12, 12);
            btnAddProject.Name = "btnAddProject";
            btnAddProject.Size = new Size(150, 35);
            btnAddProject.TabIndex = 0;
            btnAddProject.Text = "Thêm dự án mới";
            btnAddProject.UseVisualStyleBackColor = true;
            btnAddProject.Click += btnAddProject_Click;
            
            // flpProjects
            flpProjects.AutoScroll = true;
            flpProjects.FlowDirection = FlowDirection.TopDown;
            flpProjects.Location = new Point(12, 60);
            flpProjects.Name = "flpProjects";
            flpProjects.Size = new Size(760, 480);
            flpProjects.TabIndex = 1;
            flpProjects.WrapContents = false;
            
            // DashboardForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(btnAddProject);
            Controls.Add(flpProjects);
            Name = "DashboardForm";
            Text = "Project Management - Dashboard";
            Load += DashboardForm_Load;
            
            ResumeLayout(false);
        }
    }
}
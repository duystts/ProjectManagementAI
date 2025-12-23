namespace ProjectManagement.WinForms
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flpProjects;
        private Button btnAddProject;
        private Button btnUserManager;
        private Button btnAIAssistant;
        private Button btnChat;
        private Button btnLogout;
        private Label lblUserInfo;
        private TextBox txtSearch;
        private Button btnSearch;

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
            btnAddProject = new Button();
            btnUserManager = new Button();
            btnChat = new Button();
            btnLogout = new Button();
            lblUserInfo = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();
            flpProjects = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // btnAddProject
            // 
            btnAddProject.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddProject.Location = new Point(14, 77);
            btnAddProject.Margin = new Padding(3, 4, 3, 4);
            btnAddProject.Name = "btnAddProject";
            btnAddProject.Size = new Size(171, 47);
            btnAddProject.TabIndex = 3;
            btnAddProject.Text = "Add New Project";
            btnAddProject.UseVisualStyleBackColor = true;
            btnAddProject.Click += btnAddProject_Click;
            // 
            // btnUserManager
            // 
            btnUserManager.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUserManager.Location = new Point(194, 77);
            btnUserManager.Margin = new Padding(3, 4, 3, 4);
            btnUserManager.Name = "btnUserManager";
            btnUserManager.Size = new Size(171, 47);
            btnUserManager.TabIndex = 4;
            btnUserManager.Text = "User Manager";
            btnUserManager.UseVisualStyleBackColor = true;
            btnUserManager.Visible = false;
            btnUserManager.Click += btnUserManager_Click;
            // 
            // btnAIAssistant
            // 
            btnAIAssistant = new Button();
            btnAIAssistant.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAIAssistant.Location = new Point(374, 77);
            btnAIAssistant.Margin = new Padding(3, 4, 3, 4);
            btnAIAssistant.Name = "btnAIAssistant";
            btnAIAssistant.Size = new Size(171, 47);
            btnAIAssistant.TabIndex = 5;
            btnAIAssistant.Text = "AI Assistant";
            btnAIAssistant.UseVisualStyleBackColor = true;
            btnAIAssistant.Click += btnAIAssistant_Click;
            // 
            // btnChat
            // 
            btnChat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnChat.Location = new Point(554, 77);
            btnChat.Margin = new Padding(3, 4, 3, 4);
            btnChat.Name = "btnChat";
            btnChat.Size = new Size(100, 47);
            btnChat.TabIndex = 6;
            btnChat.Text = "💬 Chat";
            btnChat.UseVisualStyleBackColor = true;
            btnChat.Click += btnChat_Click;
            // 
            // btnLogout
            // 
            btnLogout.Font = new Font("Segoe UI", 9F);
            btnLogout.Location = new Point(711, 16);
            btnLogout.Margin = new Padding(3, 4, 3, 4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(171, 47);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // lblUserInfo
            // 
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Segoe UI", 10F);
            lblUserInfo.Location = new Point(14, 27);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(79, 23);
            lblUserInfo.TabIndex = 0;
            lblUserInfo.Text = "User Info";
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(14, 140);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Multiline = true;
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search projects...";
            txtSearch.Size = new Size(400, 35);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Segoe UI", 12F);
            btnSearch.Location = new Point(425, 140);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(47, 35);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "🔍";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // flpProjects
            // 
            flpProjects.AutoScroll = true;
            flpProjects.FlowDirection = FlowDirection.TopDown;
            flpProjects.Location = new Point(14, 190);
            flpProjects.Margin = new Padding(3, 4, 3, 4);
            flpProjects.Name = "flpProjects";
            flpProjects.Size = new Size(865, 530);
            flpProjects.TabIndex = 5;
            flpProjects.WrapContents = false;
            flpProjects.Click += flpProjects_Click;
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(891, 748);
            Controls.Add(lblUserInfo);
            Controls.Add(btnLogout);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(btnAddProject);
            Controls.Add(btnUserManager);
            Controls.Add(btnAIAssistant);
            Controls.Add(btnChat);
            Controls.Add(flpProjects);
            Margin = new Padding(3, 4, 3, 4);
            Name = "DashboardForm";
            Text = "Project Management - Dashboard";
            Load += DashboardForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

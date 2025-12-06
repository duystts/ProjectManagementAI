using ProjectManagement.WinForms.Controls;
using ProjectManagement.WinForms.Models;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class DashboardForm : Form
    {
        private ApiService _apiService;
        private DateTime _lastProjectOpenTime = DateTime.MinValue;
        private int _lastProjectId = -1;
        private List<ProjectDto> _allProjects = new();

        public DashboardForm()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void DashboardForm_Load(object sender, EventArgs e)
        {
            ConfigureUIByRole();
            await LoadProjects();
        }

        private void ConfigureUIByRole()
        {
            if (AuthService.CurrentUser == null)
            {
                MessageBox.Show("Session expired. Please login again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                var loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
                return;
            }

            lblUserInfo.Text = $"Welcome, {AuthService.CurrentUser.FullName} ({AuthService.CurrentUser.Role})";

            switch (AuthService.CurrentUser.Role)
            {
                case UserRole.Admin:
                    btnAddProject.Visible = true;
                    btnUserManager.Visible = true;
                    break;
                case UserRole.Manager:
                    btnAddProject.Visible = true;
                    btnUserManager.Visible = false;
                    break;
                case UserRole.Member:
                case UserRole.Viewer:
                    btnAddProject.Visible = false;
                    btnUserManager.Visible = false;
                    break;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task LoadProjects()
        {
            try
            {
                _allProjects = await _apiService.GetProjectsAsync();
                DisplayProjects(_allProjects);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading projects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayProjects(List<ProjectDto> projects)
        {
            flpProjects.Controls.Clear();

            foreach (var project in projects)
            {
                var projectCard = new ProjectCardControl();
                projectCard.SetData(project);
                projectCard.OnProjectClicked += (id) => OpenProject(id);
                projectCard.OnEditProject += (id) => EditProject(id);
                projectCard.OnDeleteProject += (id) => DeleteProject(id);
                projectCard.Margin = new Padding(5);
                flpProjects.Controls.Add(projectCard);
            }
            
            if (projects.Count == 0)
            {
                var lblNoResults = new Label
                {
                    Text = string.IsNullOrWhiteSpace(txtSearch.Text) ? "No projects found." : "No projects match your search.",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.Gray,
                    Margin = new Padding(10)
                };
                flpProjects.Controls.Add(lblNoResults);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var searchText = txtSearch.Text.Trim().ToLower();
            
            if (string.IsNullOrEmpty(searchText))
            {
                DisplayProjects(_allProjects);
                return;
            }

            var filteredProjects = _allProjects.Where(p => 
                p.Name.ToLower().Contains(searchText) || 
                (p.Description != null && p.Description.ToLower().Contains(searchText))
            ).ToList();

            DisplayProjects(filteredProjects);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Trigger the same filtering as text changed (useful if user clicks search button)
            txtSearch_TextChanged(sender, e);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void flpProjects_Click(object sender, EventArgs e)
        {
            // no-op, previously hid suggestions
        }

        private void OpenProject(int projectId)
        {
            try
            {
                var now = DateTime.Now;
                if (_lastProjectId == projectId && (now - _lastProjectOpenTime).TotalSeconds < 1)
                    return;
                
                _lastProjectId = projectId;
                _lastProjectOpenTime = now;
                
                var mainForm = new Form1(projectId, this);
                mainForm.WindowState = FormWindowState.Normal;
                mainForm.StartPosition = FormStartPosition.CenterScreen;
                mainForm.Show();
                mainForm.BringToFront();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening project: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAddProject_Click(object sender, EventArgs e)
        {
            var projectForm = new ProjectForm();
            if (projectForm.ShowDialog() == DialogResult.OK)
            {
                await LoadProjects();
            }
        }

        private async void EditProject(int projectId)
        {
            try
            {
                var projects = await _apiService.GetProjectsAsync();
                var project = projects.FirstOrDefault(p => p.Id == projectId);
                if (project != null)
                {
                    var projectForm = new ProjectForm(project);
                    if (projectForm.ShowDialog() == DialogResult.OK)
                    {
                        await LoadProjects();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing project: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUserManager_Click(object sender, EventArgs e)
        {
            var userForm = new UserManagementForm();
            userForm.ShowDialog();
        }

        private async void DeleteProject(int projectId)
        {
            var result = MessageBox.Show("Are you sure you want to delete this project?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var success = await _apiService.DeleteProjectAsync(projectId);
                    if (success)
                    {
                        MessageBox.Show("Project deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadProjects();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete project.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting project: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
using ProjectManagement.WinForms.Controls;
using ProjectManagement.WinForms.Models;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class DashboardForm : Form
    {
        private ApiService _apiService;

        public DashboardForm()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void DashboardForm_Load(object sender, EventArgs e)
        {
            await LoadProjects();
        }

        private async Task LoadProjects()
        {
            try
            {
                var projects = await _apiService.GetProjectsAsync();
                
                flpProjects.Controls.Clear();

                foreach (var project in projects)
                {
                    var projectCard = new ProjectCardControl();
                    projectCard.SetData(project);
                    projectCard.OnProjectClicked += ProjectCard_OnProjectClicked;
                    projectCard.OnEditProject += ProjectCard_OnEditProject;
                    projectCard.OnDeleteProject += ProjectCard_OnDeleteProject;
                    projectCard.Margin = new Padding(5);
                    flpProjects.Controls.Add(projectCard);
                }
                
                if (projects.Count == 0)
                {
                    MessageBox.Show("No projects found. Please add a project first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading projects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProjectCard_OnProjectClicked(int projectId)
        {
            try
            {
                var mainForm = new Form1(projectId);
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

        private async void ProjectCard_OnEditProject(int projectId)
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

        private async void ProjectCard_OnDeleteProject(int projectId)
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
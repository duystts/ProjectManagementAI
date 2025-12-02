using ProjectManagement.WinForms.Controls;
using ProjectManagement.WinForms.Models;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class DashboardForm : Form
    {
        private ApiService _apiService;
        private FlowLayoutPanel flpProjects;
        private Button btnAddProject;

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

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Project functionality not implemented yet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
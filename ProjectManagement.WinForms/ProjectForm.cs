using ProjectManagement.WinForms.Models;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class ProjectForm : Form
    {
        private readonly ApiService _apiService;
        private ProjectDto? _project;
        private bool _isEditMode;

        public ProjectForm(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            _isEditMode = false;
            this.Text = "Add New Project";
            btnSave.Text = "Create";
            btnDelete.Visible = false;
        }

        public void LoadProject(ProjectDto project)
        {
            _project = project;
            _isEditMode = true;

            txtName.Text = _project.Name;
            txtDescription.Text = _project.Description;

            this.Text = "Edit Project";
            btnSave.Text = "Update";
            btnDelete.Visible = true;
        }


        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Project name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnSave.Enabled = false;
                
                var project = new ProjectDto
                {
                    Id = _project?.Id ?? 0,
                    Name = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    CreatedAt = _project?.CreatedAt ?? DateTime.Now
                };

                bool success;
                if (_isEditMode)
                {
                    success = await _apiService.UpdateProjectAsync(project);
                }
                else
                {
                    var result = await _apiService.CreateProjectAsync(project);
                    success = result != null;
                }

                if (success)
                {
                    MessageBox.Show($"Project {(_isEditMode ? "updated" : "created")} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Failed to {(_isEditMode ? "update" : "create")} project.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_project == null) return;
            
            var result = MessageBox.Show("Are you sure you want to delete this project?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var success = await _apiService.DeleteProjectAsync(_project.Id);
                    if (success)
                    {
                        MessageBox.Show("Project deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
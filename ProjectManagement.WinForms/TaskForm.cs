using ProjectManagement.WinForms.Models;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class TaskForm : Form
    {
        private ApiService _apiService;
        private ProjectTaskDto? _task;
        private bool _isEditMode;
        private int _projectId;

        public TaskForm(int projectId) : this(projectId, null) { }

        public TaskForm(int projectId, ProjectTaskDto? task)
        {
            InitializeComponent();
            _apiService = new ApiService();
            _task = task;
            _projectId = projectId;
            _isEditMode = task != null;
            
            InitializeComboBoxes();
            
            if (_isEditMode)
            {
                LoadTaskData();
                this.Text = "Edit Task";
                btnSave.Text = "Update";
            }
            else
            {
                this.Text = "Add New Task";
                btnSave.Text = "Create";
                cmbStatus.SelectedIndex = 0; // Todo
                cmbPriority.SelectedIndex = 1; // Medium
            }
        }

        private void InitializeComboBoxes()
        {
            cmbStatus.Items.AddRange(new[] { "Todo", "InProgress", "Done" });
            cmbPriority.Items.AddRange(new[] { "Low", "Medium", "High" });
        }

        private void LoadTaskData()
        {
            if (_task != null)
            {
                txtTitle.Text = _task.Title;
                txtDescription.Text = _task.Description;
                cmbStatus.SelectedIndex = (int)_task.Status;
                cmbPriority.SelectedIndex = (int)_task.Priority;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Task title is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStatus.SelectedIndex == -1 || cmbPriority.SelectedIndex == -1)
            {
                MessageBox.Show("Please select status and priority.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnSave.Enabled = false;
                
                var task = new ProjectTaskDto
                {
                    Id = _task?.Id ?? 0,
                    ProjectId = _projectId,
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Status = (Models.TaskStatus)cmbStatus.SelectedIndex,
                    Priority = (Models.TaskPriority)cmbPriority.SelectedIndex,
                    CreatedAt = _task?.CreatedAt ?? DateTime.Now
                };

                bool success;
                if (_isEditMode)
                {
                    success = await _apiService.UpdateTaskAsync(task);
                }
                else
                {
                    var result = await _apiService.CreateTaskAsync(task);
                    success = result != null;
                }

                if (success)
                {
                    MessageBox.Show($"Task {(_isEditMode ? "updated" : "created")} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Failed to {(_isEditMode ? "update" : "create")} task.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
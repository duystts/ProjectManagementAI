using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.WinForms.Controls;
using ProjectManagement.Entities.Models.DTOs;
using ProjectManagement.Entities.Models.Enums;
using TaskStatusEnum = ProjectManagement.Entities.Models.Enums.TaskStatus;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class Form1 : Form
    {
        private readonly ApiService _apiService;
        private int _projectId;
        private DashboardForm? _parentDashboard;

        public Form1(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        public void SetProjectContext(int projectId, DashboardForm? parentDashboard = null)
        {
            _projectId = projectId;
            _parentDashboard = parentDashboard;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            ConfigureUIByRole();
            
            // Register resize events for columns
            flpTodo.Resize += (s, ev) => ResizeColumnItems(flpTodo);
            flpProgress.Resize += (s, ev) => ResizeColumnItems(flpProgress);
            flpPendingReview.Resize += (s, ev) => ResizeColumnItems(flpPendingReview);
            flpDone.Resize += (s, ev) => ResizeColumnItems(flpDone);

            await LoadTasks();
        }

        private void ResizeColumnItems(FlowLayoutPanel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is TaskCardControl card)
                {
                    card.Width = panel.ClientSize.Width - 10; // Account for margin/scrollbar
                }
            }
        }

        private void ConfigureUIByRole()
        {
            if (AuthService.CurrentUser == null) return;

            switch (AuthService.CurrentUser.Role)
            {
                case UserRole.Admin:
                case UserRole.Manager:
                case UserRole.Member:
                    btnAddTask.Visible = true;
                    break;
                case UserRole.Viewer:
                    btnAddTask.Visible = false;
                    break;
            }
        }

        private async Task LoadTasks()
        {
            try
            {
                var tasks = await _apiService.GetTasksByProjectAsync(_projectId);
                
                // Clear existing controls
                flpTodo.Controls.Clear();
                flpProgress.Controls.Clear();
                flpPendingReview.Controls.Clear();
                flpDone.Controls.Clear();

                foreach (var task in tasks)
                {
                    var taskCard = new TaskCardControl();
                    taskCard.SetData(task);
                    taskCard.OnTaskClicked += (id) => ShowTaskInfo(id);
                    taskCard.OnEditTask += (id) => EditTask(id);
                    taskCard.OnDeleteTask += (id) => DeleteTask(id);
                    taskCard.OnAssignTask += (id) => AssignTask(id);
                    taskCard.OnUnassignTask += (id) => UnassignTask(id);

                    switch (task.Status)
                    {
                        case TaskStatusEnum.Todo:
                            taskCard.Width = flpTodo.ClientSize.Width - 10;
                            flpTodo.Controls.Add(taskCard);
                            break;
                        case TaskStatusEnum.InProgress:
                            taskCard.Width = flpProgress.ClientSize.Width - 10;
                            flpProgress.Controls.Add(taskCard);
                            break;
                        case TaskStatusEnum.PendingReview:
                            taskCard.Width = flpPendingReview.ClientSize.Width - 10;
                            flpPendingReview.Controls.Add(taskCard);
                            break;
                        case TaskStatusEnum.Done:
                            taskCard.Width = flpDone.ClientSize.Width - 10;
                            flpDone.Controls.Add(taskCard);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowTaskInfo(int taskId)
        {
            MessageBox.Show($"Bạn đã chọn Task ID: {taskId}", "Task Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnAddTask_Click(object sender, EventArgs e)
        {
            var taskForm = new TaskForm(_apiService, _projectId);
            if (taskForm.ShowDialog() == DialogResult.OK)
            {
                await LoadTasks();
            }
        }

        private async void EditTask(int taskId)
        {
            try
            {
                var tasks = await _apiService.GetTasksByProjectAsync(_projectId);
                var task = tasks.FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    var taskForm = new TaskForm(_apiService, _projectId);
                    taskForm.LoadTask(task);
                    if (taskForm.ShowDialog() == DialogResult.OK)
                    {
                        await LoadTasks();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing task: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void DeleteTask(int taskId)
        {
            var result = MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var success = await _apiService.DeleteTaskAsync(taskId);
                    if (success)
                    {
                        MessageBox.Show("Task deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadTasks();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete task.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting task: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBack_Click(object? sender, EventArgs e)
        {
            if (_parentDashboard != null && !_parentDashboard.IsDisposed)
            {
                _parentDashboard.Show();
                _parentDashboard.BringToFront();
            }
            Close();
        }

        private async void AssignTask(int taskId)
        {
            try
            {
                if (AuthService.CurrentUser?.Role == UserRole.Admin)
                {
                    var users = await _apiService.GetUsersAsync();
                    if (users.Count == 0)
                    {
                        MessageBox.Show("No users available to assign.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var userNames = users.Select(u => $"{u.Username} ({u.Role})").ToArray();
                    var selectedIndex = ShowSelectionDialog("Select User to Assign", userNames);
                    
                    if (selectedIndex >= 0)
                    {
                        var selectedUser = users[selectedIndex];
                        var success = await _apiService.AssignTaskAsync(taskId, selectedUser.Id);
                        if (success)
                        {
                            MessageBox.Show($"Task assigned to {selectedUser.Username} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadTasks();
                        }
                        else
                        {
                            MessageBox.Show("Failed to assign task.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    // Self-assign for Manager and Member
                    var success = await _apiService.SelfAssignTaskAsync(taskId);
                    if (success)
                    {
                        MessageBox.Show("Task assigned to yourself successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadTasks();
                    }
                    else
                    {
                        MessageBox.Show("Failed to assign task.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error assigning task: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void UnassignTask(int taskId)
        {
            var result = MessageBox.Show("Are you sure you want to unassign this task?", "Confirm Unassign", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var success = await _apiService.UnassignTaskAsync(taskId);
                    if (success)
                    {
                        MessageBox.Show("Task unassigned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadTasks();
                    }
                    else
                    {
                        MessageBox.Show("Failed to unassign task.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error unassigning task: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int ShowSelectionDialog(string title, string[] items)
        {
            using var form = new Form()
            {
                Text = title,
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent
            };

            var listBox = new ListBox()
            {
                Dock = DockStyle.Fill,
                DataSource = items
            };

            var btnOk = new Button()
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom
            };

            form.Controls.Add(listBox);
            form.Controls.Add(btnOk);
            form.AcceptButton = btnOk;

            return form.ShowDialog() == DialogResult.OK ? listBox.SelectedIndex : -1;
        }


    }
}

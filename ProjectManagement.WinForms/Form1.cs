using ProjectManagement.WinForms.Controls;
using ProjectManagement.WinForms.Models;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class Form1 : Form
    {
        private ApiService _apiService;
        private int _projectId;

        public Form1() : this(1) { }

        public Form1(int projectId)
        {
            InitializeComponent();
            _apiService = new ApiService();
            _projectId = projectId;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadTasks();
        }

        private async Task LoadTasks()
        {
            try
            {
                var tasks = await _apiService.GetTasksByProjectAsync(_projectId);
                
                // Clear existing controls
                flpTodo.Controls.Clear();
                flpProgress.Controls.Clear();
                flpDone.Controls.Clear();

                foreach (var task in tasks)
                {
                    var taskCard = new TaskCardControl();
                    taskCard.SetData(task);
                    taskCard.OnTaskClicked += TaskCard_OnTaskClicked;
                    taskCard.OnEditTask += TaskCard_OnEditTask;
                    taskCard.OnDeleteTask += TaskCard_OnDeleteTask;

                    switch (task.Status)
                    {
                        case Models.TaskStatus.Todo:
                            flpTodo.Controls.Add(taskCard);
                            break;
                        case Models.TaskStatus.InProgress:
                            flpProgress.Controls.Add(taskCard);
                            break;
                        case Models.TaskStatus.Done:
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

        private void TaskCard_OnTaskClicked(int taskId)
        {
            MessageBox.Show($"Bạn đã chọn Task ID: {taskId}", "Task Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnAddTask_Click(object sender, EventArgs e)
        {
            var taskForm = new TaskForm(_projectId);
            if (taskForm.ShowDialog() == DialogResult.OK)
            {
                await LoadTasks();
            }
        }

        private async void TaskCard_OnEditTask(int taskId)
        {
            try
            {
                var tasks = await _apiService.GetTasksByProjectAsync(_projectId);
                var task = tasks.FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    var taskForm = new TaskForm(_projectId, task);
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

        private async void TaskCard_OnDeleteTask(int taskId)
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
            var dashboardForm = new DashboardForm();
            dashboardForm.Show();
            this.Close();
        }


    }
}

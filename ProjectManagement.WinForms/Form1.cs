using ProjectManagement.WinForms.Controls;
using ProjectManagement.WinForms.Models;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class Form1 : Form
    {
        private ApiService _apiService;
        private TableLayoutPanel tableLayoutPanel;
        private FlowLayoutPanel flpTodo, flpProgress, flpDone;
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


    }
}

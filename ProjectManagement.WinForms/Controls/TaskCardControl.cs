using ProjectManagement.WinForms.Models;

namespace ProjectManagement.WinForms.Controls
{
    public partial class TaskCardControl : UserControl
    {
        public event Action<int>? OnTaskClicked;
        
        private ProjectTaskDto? _taskData;
        public ProjectTaskDto? TaskData
        {
            get => _taskData;
            set
            {
                _taskData = value;
                if (value != null)
                    SetData(value);
            }
        }

        public TaskCardControl()
        {
            InitializeComponent();
        }

        public void SetData(ProjectTaskDto task)
        {
            lblTitle.Text = task.Title;
            lblId.Text = $"#{task.Id}";
            lblPriority.Text = task.Priority.ToString();

            // Set priority color
            switch (task.Priority)
            {
                case Models.TaskPriority.High:
                    lblPriority.ForeColor = Color.Red;
                    break;
                case Models.TaskPriority.Medium:
                    lblPriority.ForeColor = Color.Orange;
                    break;
                case Models.TaskPriority.Low:
                    lblPriority.ForeColor = Color.Green;
                    break;
            }
        }

        private void TaskCardControl_Click(object sender, EventArgs e)
        {
            if (_taskData != null)
                OnTaskClicked?.Invoke(_taskData.Id);
        }

        private void Label_Click(object sender, EventArgs e)
        {
            TaskCardControl_Click(sender, e);
        }
    }
}
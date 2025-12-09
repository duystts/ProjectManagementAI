using ProjectManagement.WinForms.Models;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms.Controls
{
    public partial class TaskCardControl : UserControl
    {
        public event Action<int>? OnTaskClicked;
        public event Action<int>? OnEditTask;
        public event Action<int>? OnDeleteTask;
        
        private ProjectTaskDto? _taskData;
        
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
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
            ConfigureButtonsByRole();
        }

        private void ConfigureButtonsByRole()
        {
            if (AuthService.CurrentUser == null)
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                return;
            }

            switch (AuthService.CurrentUser.Role)
            {
                case UserRole.Admin:
                case UserRole.Manager:
                case UserRole.Member:
                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    break;
                case UserRole.Viewer:
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    break;
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (_taskData != null)
                OnEditTask?.Invoke(_taskData.Id);
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_taskData != null)
                OnDeleteTask?.Invoke(_taskData.Id);
        }

        public void SetData(ProjectTaskDto task)
        {
            _taskData = task;
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

        private void TaskCardControl_Click(object? sender, EventArgs e)
        {
            if (_taskData != null)
                OnTaskClicked?.Invoke(_taskData.Id);
        }

        private void Label_Click(object? sender, EventArgs e)
        {
            TaskCardControl_Click(sender, e);
        }
    }
}
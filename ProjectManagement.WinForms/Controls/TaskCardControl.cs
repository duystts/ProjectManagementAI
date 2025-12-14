using ProjectManagement.Entities.Models.DTOs;
using ProjectManagement.Entities.Models.Enums;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms.Controls
{
    public partial class TaskCardControl : UserControl
    {
        public event Action<int>? OnTaskClicked;
        public event Action<int>? OnEditTask;
        public event Action<int>? OnDeleteTask;
        public event Action<int>? OnAssignTask;
        public event Action<int>? OnUnassignTask;
        
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
                btnAssign.Visible = false;
                return;
            }

            switch (AuthService.CurrentUser.Role)
            {
                case UserRole.Admin:
                case UserRole.Manager:
                case UserRole.Member:
                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    btnAssign.Visible = true;
                    break;
                case UserRole.Viewer:
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnAssign.Visible = false;
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

        private void BtnAssign_Click(object? sender, EventArgs e)
        {
            if (_taskData != null)
            {
                if (_taskData.AssignedUserId == null)
                {
                    OnAssignTask?.Invoke(_taskData.Id);
                }
                else if (AuthService.CurrentUser?.Role == UserRole.Admin)
                {
                    OnUnassignTask?.Invoke(_taskData.Id);
                }
                else
                {
                    MessageBox.Show("Only Admin can unassign tasks.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
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
                case TaskPriority.High:
                    lblPriority.ForeColor = Color.Red;
                    break;
                case TaskPriority.Medium:
                    lblPriority.ForeColor = Color.Orange;
                    break;
                case TaskPriority.Low:
                    lblPriority.ForeColor = Color.Green;
                    break;
            }

            // Show assigned user if exists
            if (!string.IsNullOrEmpty(task.AssignedUserName))
            {
                lblTitle.Text += $" (Assigned: {task.AssignedUserName})";
            }

            // Debug info
            System.Diagnostics.Debug.WriteLine($"Task {task.Id}: AssignedUserId={task.AssignedUserId}, AssignedUserName={task.AssignedUserName}");
            System.Diagnostics.Debug.WriteLine($"Current User: {AuthService.CurrentUser?.Username}, Role: {AuthService.CurrentUser?.Role}");

            // Update assign button text
            if (btnAssign.Visible)
            {
                if (_taskData.AssignedUserId == null)
                {
                    btnAssign.Text = "Assign";
                }
                else if (AuthService.CurrentUser?.Role == UserRole.Admin)
                {
                    btnAssign.Text = "Unassign";
                }
                else
                {
                    btnAssign.Text = "Assigned";
                    btnAssign.Enabled = false;
                }
            }

            // Add context menu for assign/unassign
            AddContextMenu();
        }

        private void AddContextMenu()
        {
            if (AuthService.CurrentUser == null || _taskData == null) return;

            var contextMenu = new ContextMenuStrip();

            // Admin and Manager can assign tasks
            if (AuthService.CurrentUser.Role == UserRole.Admin || AuthService.CurrentUser.Role == UserRole.Manager)
            {
                if (_taskData.AssignedUserId == null)
                {
                    var assignItem = new ToolStripMenuItem("Assign Task");
                    assignItem.Click += (s, e) => OnAssignTask?.Invoke(_taskData.Id);
                    contextMenu.Items.Add(assignItem);
                }
            }

            // Only Admin can unassign tasks
            if (AuthService.CurrentUser.Role == UserRole.Admin && _taskData.AssignedUserId != null)
            {
                var unassignItem = new ToolStripMenuItem("Unassign Task");
                unassignItem.Click += (s, e) => OnUnassignTask?.Invoke(_taskData.Id);
                contextMenu.Items.Add(unassignItem);
            }

            // Always set context menu, even if empty (for debugging)
            this.ContextMenuStrip = contextMenu;
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
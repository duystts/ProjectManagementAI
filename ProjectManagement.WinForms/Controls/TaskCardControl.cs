using ProjectManagement.Entities.Models.DTOs;
using ProjectManagement.Entities.Models.Enums;
using TaskStatusEnum = ProjectManagement.Entities.Models.Enums.TaskStatus;
using ProjectManagement.WinForms.Services;
using System.Drawing.Drawing2D;

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
        private System.Windows.Forms.Timer? _deadlineTimer;
        private int _radius = 10;
        
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
            
            // Remove default border style
            this.BorderStyle = BorderStyle.None;
            this.Padding = new Padding(5);

            ConfigureButtonsByRole();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            _deadlineTimer = new System.Windows.Forms.Timer();
            _deadlineTimer.Interval = 30000; // 30 seconds
            _deadlineTimer.Tick += DeadlineTimer_Tick;
            _deadlineTimer.Start();
        }

        private void DeadlineTimer_Tick(object? sender, EventArgs e)
        {
            if (_taskData != null && _taskData.Deadline.HasValue && _taskData.Status != TaskStatusEnum.Done)
            {
                // Check if status changed (deadline just passed)
                bool isOverdue = _taskData.Deadline.Value < DateTime.Now;
                
                // Update label color
                if (isOverdue)
                {
                    lblDeadline.ForeColor = Color.Red;
                }

                // Always invalidate to ensure border is correct
                this.Invalidate();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_deadlineTimer != null)
                {
                    _deadlineTimer.Stop();
                    _deadlineTimer.Dispose();
                }
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
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

            // Show Deadline
            if (task.Deadline.HasValue)
            {
                lblDeadline.Text = $"Due: {task.Deadline.Value:g}";
                if (task.Deadline.Value < DateTime.Now && task.Status != TaskStatusEnum.Done)
                {
                    lblDeadline.ForeColor = Color.Red;
                }
                else
                {
                    lblDeadline.ForeColor = Color.Gray;
                }
            }
            else
            {
                lblDeadline.Text = "";
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
            
            // Trigger repaint for border
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color borderColor = Color.Gray;
            int borderWidth = 1;

            if (_taskData != null && _taskData.Deadline.HasValue && _taskData.Deadline.Value < DateTime.Now && _taskData.Status != TaskStatusEnum.Done)
            {
                borderColor = Color.Red;
                borderWidth = 2;
            }

            using (GraphicsPath path = GetRoundedPath(ClientRectangle, _radius))
            {
                this.Region = new Region(path);
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
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
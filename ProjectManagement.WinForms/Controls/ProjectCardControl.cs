using ProjectManagement.Entities.Models.DTOs;
using ProjectManagement.Entities.Models.Enums;
using ProjectManagement.WinForms.Services;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ProjectManagement.WinForms.Controls
{
    public partial class ProjectCardControl : UserControl
    {
        public event Action<int>? OnProjectClicked;
        public event Action<int>? OnEditProject;
        public event Action<int>? OnDeleteProject;
        
        private ProjectDto? _projectData;
        private int _radius = 10;
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ProjectDto? ProjectData
        {
            get => _projectData;
            set
            {
                _projectData = value;
                if (value != null)
                    SetData(value);
            }
        }

        public ProjectCardControl()
        {
            InitializeComponent();
            
            // Remove default border style
            this.BorderStyle = BorderStyle.None;
            this.Padding = new Padding(5);

            // Đăng ký event click cho tất cả controls
            this.Click += ProjectCardControl_Click;
            lblName.Click += ProjectCardControl_Click;
            lblDescription.Click += ProjectCardControl_Click;
            lblCreated.Click += ProjectCardControl_Click;
            
            // Đăng ký events cho buttons
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            
            ConfigureButtonsByRole();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = GetRoundedPath(ClientRectangle, _radius))
            {
                this.Region = new Region(path);
                using (Pen pen = new Pen(Color.Gray, 1))
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
                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    break;
                case UserRole.Manager:
                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    break;
                case UserRole.Member:
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    break;
                case UserRole.Viewer:
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    break;
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (_projectData != null)
                OnEditProject?.Invoke(_projectData.Id);
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_projectData != null)
                OnDeleteProject?.Invoke(_projectData.Id);
        }

        public void SetData(ProjectDto project)
        {
            _projectData = project; // Lưu dữ liệu lại để click có thể sử dụng
            lblName.Text = project.Name;
            lblDescription.Text = project.Description;
            lblCreated.Text = project.CreatedAt.ToString("dd/MM/yyyy");
        }

        private void ProjectCardControl_Click(object? sender, EventArgs e)
        {
            if (_projectData != null)
                OnProjectClicked?.Invoke(_projectData.Id);
        }


    }
}
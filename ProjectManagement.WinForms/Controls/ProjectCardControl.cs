using ProjectManagement.WinForms.Models;

namespace ProjectManagement.WinForms.Controls
{
    public partial class ProjectCardControl : UserControl
    {
        public event Action<int>? OnProjectClicked;
        public event Action<int>? OnEditProject;
        public event Action<int>? OnDeleteProject;
        
        private ProjectDto? _projectData;
        
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
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
            
            // Đăng ký event click cho tất cả controls
            this.Click += ProjectCardControl_Click;
            lblName.Click += ProjectCardControl_Click;
            lblDescription.Click += ProjectCardControl_Click;
            lblCreated.Click += ProjectCardControl_Click;
            
            // Đăng ký events cho buttons
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
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
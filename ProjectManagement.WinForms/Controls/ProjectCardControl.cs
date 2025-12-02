using ProjectManagement.WinForms.Models;

namespace ProjectManagement.WinForms.Controls
{
    public partial class ProjectCardControl : UserControl
    {
        public event Action<int>? OnProjectClicked;
        
        private ProjectDto? _projectData;
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
        }

        public void SetData(ProjectDto project)
        {
            _projectData = project; // Lưu dữ liệu lại để click có thể sử dụng
            lblName.Text = project.Name;
            lblDescription.Text = project.Description;
            lblCreated.Text = project.CreatedAt.ToString("dd/MM/yyyy");
        }

        private void ProjectCardControl_Click(object sender, EventArgs e)
        {
            if (_projectData != null)
                OnProjectClicked?.Invoke(_projectData.Id);
        }


    }
}
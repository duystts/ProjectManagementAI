using ProjectManagement.WinForms.Services;
using ProjectManagement.Entities.Models.DTOs;

namespace ProjectManagement.WinForms
{
    public partial class AIAssistantForm : Form
    {
        private readonly ApiService _apiService;
        private List<ProjectDto> _projects = new();
        private List<ProjectTaskDto> _currentProjectTasks = new();
        private static string _chatHistory = string.Empty;

        public AIAssistantForm(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
                return;

            var message = txtMessage.Text.Trim();
            txtMessage.Clear();
            
            AppendMessage($"You: {message}", Color.Blue);
            btnSendMessage.Enabled = false;

            try
            {
                // Auto-embed project context if project is selected
                await EmbedCurrentProjectContext();

                var request = new ChatRequest
                {
                    Message = message,
                    ProjectId = (cmbProject.SelectedItem as ProjectDto)?.Id,
                    TaskId = null
                };

                var response = await _apiService.ChatAsync(request);
                
                AppendMessage($"AI: {response.Response}", Color.Green);
                
                if (response.Sources.Any())
                {
                    AppendMessage($"Sources: {string.Join(", ", response.Sources)}", Color.Gray);
                }
            }
            catch (Exception ex)
            {
                AppendMessage($"Error: {ex.Message}", Color.Red);
            }
            finally
            {
                btnSendMessage.Enabled = true;
                txtMessage.Focus();
            }
        }

        private async Task EmbedCurrentProjectContext()
        {
            if (cmbProject.SelectedItem is not ProjectDto selectedProject) return;

            try
            {
                // Embed project info
                var projectContent = $"Project: {selectedProject.Name}\nDescription: {selectedProject.Description}\nCreated: {selectedProject.CreatedAt}";
                await _apiService.AddKnowledgeAsync($"Project: {selectedProject.Name}", projectContent, selectedProject.Id, null);

                // Embed tasks for this project
                foreach (var task in _currentProjectTasks)
                {
                    var taskContent = $"Task: {task.Title}\nDescription: {task.Description}\nStatus: {task.Status}\nPriority: {task.Priority}\nCreated: {task.CreatedAt}\nAssigned to: {task.AssignedUserName ?? "Unassigned"}\nProject: {selectedProject.Name}";
                    await _apiService.AddKnowledgeAsync($"Task: {task.Title}", taskContent, selectedProject.Id, task.Id);
                }
            }
            catch
            {
                // Ignore embedding errors - knowledge might already exist
            }
        }

        private void AppendMessage(string message, Color color)
        {
            var formattedMessage = $"{DateTime.Now:HH:mm} - {message}\n\n";
            _chatHistory += formattedMessage;
            
            rtbChat.SelectionStart = rtbChat.TextLength;
            rtbChat.SelectionLength = 0;
            rtbChat.SelectionColor = color;
            rtbChat.AppendText(formattedMessage);
            rtbChat.SelectionColor = rtbChat.ForeColor;
            rtbChat.ScrollToCaret();
        }

        private async void btnAddKnowledge_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKnowledgeTitle.Text) || string.IsNullOrWhiteSpace(txtKnowledgeContent.Text))
            {
                MessageBox.Show("Please fill in both title and content.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var id = await _apiService.AddKnowledgeAsync(
                    txtKnowledgeTitle.Text.Trim(), 
                    txtKnowledgeContent.Text.Trim(), 
                    (cmbProject.SelectedItem as ProjectDto)?.Id,
                    null);
                
                MessageBox.Show($"Knowledge added successfully! ID: {id}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txtKnowledgeTitle.Clear();
                txtKnowledgeContent.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding knowledge: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void AIAssistantForm_Load(object sender, EventArgs e)
        {
            try
            {
                _projects = await _apiService.GetProjectsAsync();
                cmbProject.DataSource = _projects;
                cmbProject.DisplayMember = "Name";
                cmbProject.ValueMember = "Id";
                cmbProject.SelectedIndex = -1;
                
                // Restore chat history
                if (!string.IsNullOrEmpty(_chatHistory))
                {
                    rtbChat.Text = _chatHistory;
                    rtbChat.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading projects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cmbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProject.SelectedItem is ProjectDto selectedProject)
            {
                try
                {
                    _currentProjectTasks = await _apiService.GetTasksByProjectAsync(selectedProject.Id);
                    AppendMessage($"Project selected: {selectedProject.Name} ({_currentProjectTasks.Count} tasks loaded)", Color.DarkBlue);
                }
                catch (Exception ex)
                {
                    AppendMessage($"Error loading project tasks: {ex.Message}", Color.Red);
                }
            }
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.Handled = true;
                btnSendMessage_Click(sender, e);
            }
        }
    }
}
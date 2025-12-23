using ProjectManagement.WinForms.Services;
using ProjectManagement.Entities.Models.DTOs;

namespace ProjectManagement.WinForms
{
    public partial class AIAssistantForm : Form
    {
        private readonly ApiService _apiService;

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
                var request = new ChatRequest
                {
                    Message = message,
                    ProjectId = (int?)cmbProject.SelectedValue,
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

        private void AppendMessage(string message, Color color)
        {
            rtbChat.SelectionStart = rtbChat.TextLength;
            rtbChat.SelectionLength = 0;
            rtbChat.SelectionColor = color;
            rtbChat.AppendText($"{DateTime.Now:HH:mm} - {message}\n\n");
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
                var request = new
                {
                    Title = txtKnowledgeTitle.Text.Trim(),
                    Content = txtKnowledgeContent.Text.Trim(),
                    ProjectId = (int?)cmbProject.SelectedValue
                };

                var id = await _apiService.AddKnowledgeAsync(request.Title, request.Content, request.ProjectId);
                
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
                var projects = await _apiService.GetProjectsAsync();
                cmbProject.DataSource = projects;
                cmbProject.DisplayMember = "Name";
                cmbProject.ValueMember = "Id";
                cmbProject.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading projects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class ChatForm : Form
    {
        private readonly ApiService _apiService;

        public ChatForm(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            var message = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(message)) return;

            // Add user message to chat
            AddMessageToChat("You", message, true);
            txtMessage.Clear();

            try
            {
                // Call AI API
                var response = await _apiService.SendChatMessageAsync(message);
                AddMessageToChat("AI Assistant", response, false);
            }
            catch (Exception ex)
            {
                AddMessageToChat("System", $"Error: {ex.Message}", false);
            }
        }

        private void AddMessageToChat(string sender, string message, bool isUser)
        {
            var messageText = $"{sender}: {message}\n\n";
            
            if (rtbChat.InvokeRequired)
            {
                rtbChat.Invoke(() => {
                    rtbChat.AppendText(messageText);
                    rtbChat.ScrollToCaret();
                });
            }
            else
            {
                rtbChat.AppendText(messageText);
                rtbChat.ScrollToCaret();
            }
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.Handled = true;
                btnSend_Click(sender, e);
            }
        }
    }
}
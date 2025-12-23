namespace ProjectManagement.WinForms
{
    partial class ChatForm
    {
        private System.ComponentModel.IContainer components = null;
        private RichTextBox rtbChat;
        private TextBox txtMessage;
        private Button btnSend;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            rtbChat = new RichTextBox();
            txtMessage = new TextBox();
            btnSend = new Button();
            SuspendLayout();
            // 
            // rtbChat
            // 
            rtbChat.BackColor = Color.White;
            rtbChat.BorderStyle = BorderStyle.FixedSingle;
            rtbChat.Font = new Font("Segoe UI", 9F);
            rtbChat.Location = new Point(12, 12);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.Size = new Size(560, 400);
            rtbChat.TabIndex = 0;
            // 
            // txtMessage
            // 
            txtMessage.Font = new Font("Segoe UI", 10F);
            txtMessage.Location = new Point(12, 430);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.PlaceholderText = "Type your message...";
            txtMessage.Size = new Size(470, 60);
            txtMessage.TabIndex = 1;
            txtMessage.KeyDown += txtMessage_KeyDown;
            // 
            // btnSend
            // 
            btnSend.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSend.Location = new Point(497, 430);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 60);
            btnSend.TabIndex = 2;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // ChatForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 511);
            Controls.Add(btnSend);
            Controls.Add(txtMessage);
            Controls.Add(rtbChat);
            Name = "ChatForm";
            Text = "AI Chat Assistant";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
namespace ProjectManagement.WinForms
{
    partial class AIAssistantForm
    {
        private System.ComponentModel.IContainer components = null;
        private RichTextBox rtbChat;
        private TextBox txtMessage;
        private Button btnSendMessage;
        private TextBox txtKnowledgeTitle;
        private TextBox txtKnowledgeContent;
        private Button btnAddKnowledge;
        private ComboBox cmbProject;
        private Label lblProject;
        private Label lblKnowledgeTitle;
        private Label lblKnowledgeContent;
        private GroupBox gbChat;
        private GroupBox gbKnowledge;

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
            btnSendMessage = new Button();
            txtKnowledgeTitle = new TextBox();
            txtKnowledgeContent = new TextBox();
            btnAddKnowledge = new Button();
            cmbProject = new ComboBox();
            lblProject = new Label();
            lblKnowledgeTitle = new Label();
            lblKnowledgeContent = new Label();
            gbChat = new GroupBox();
            gbKnowledge = new GroupBox();
            gbChat.SuspendLayout();
            gbKnowledge.SuspendLayout();
            SuspendLayout();
            // 
            // rtbChat
            // 
            rtbChat.Location = new Point(6, 26);
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            rtbChat.Size = new Size(560, 300);
            rtbChat.TabIndex = 0;
            rtbChat.Text = "";
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(6, 332);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.PlaceholderText = "Type your message...";
            txtMessage.Size = new Size(460, 60);
            txtMessage.TabIndex = 1;
            txtMessage.KeyDown += txtMessage_KeyDown;
            // 
            // btnSendMessage
            // 
            btnSendMessage.Location = new Point(472, 332);
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Size = new Size(94, 60);
            btnSendMessage.TabIndex = 2;
            btnSendMessage.Text = "Send";
            btnSendMessage.UseVisualStyleBackColor = true;
            btnSendMessage.Click += btnSendMessage_Click;
            // 
            // lblProject
            // 
            lblProject.AutoSize = true;
            lblProject.Location = new Point(12, 15);
            lblProject.Name = "lblProject";
            lblProject.Size = new Size(58, 20);
            lblProject.TabIndex = 3;
            lblProject.Text = "Project:";
            // 
            // cmbProject
            // 
            cmbProject.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProject.Location = new Point(76, 12);
            cmbProject.Name = "cmbProject";
            cmbProject.Size = new Size(200, 28);
            cmbProject.TabIndex = 4;
            cmbProject.SelectedIndexChanged += cmbProject_SelectedIndexChanged;
            // 
            // gbChat
            // 
            gbChat.Controls.Add(rtbChat);
            gbChat.Controls.Add(txtMessage);
            gbChat.Controls.Add(btnSendMessage);
            gbChat.Location = new Point(12, 50);
            gbChat.Name = "gbChat";
            gbChat.Size = new Size(580, 410);
            gbChat.TabIndex = 5;
            gbChat.TabStop = false;
            gbChat.Text = "AI Chat";
            // 
            // lblKnowledgeTitle
            // 
            lblKnowledgeTitle.AutoSize = true;
            lblKnowledgeTitle.Location = new Point(6, 30);
            lblKnowledgeTitle.Name = "lblKnowledgeTitle";
            lblKnowledgeTitle.Size = new Size(41, 20);
            lblKnowledgeTitle.TabIndex = 6;
            lblKnowledgeTitle.Text = "Title:";
            // 
            // txtKnowledgeTitle
            // 
            txtKnowledgeTitle.Location = new Point(6, 53);
            txtKnowledgeTitle.Name = "txtKnowledgeTitle";
            txtKnowledgeTitle.PlaceholderText = "Knowledge title...";
            txtKnowledgeTitle.Size = new Size(560, 27);
            txtKnowledgeTitle.TabIndex = 7;
            // 
            // lblKnowledgeContent
            // 
            lblKnowledgeContent.AutoSize = true;
            lblKnowledgeContent.Location = new Point(6, 90);
            lblKnowledgeContent.Name = "lblKnowledgeContent";
            lblKnowledgeContent.Size = new Size(65, 20);
            lblKnowledgeContent.TabIndex = 8;
            lblKnowledgeContent.Text = "Content:";
            // 
            // txtKnowledgeContent
            // 
            txtKnowledgeContent.Location = new Point(6, 113);
            txtKnowledgeContent.Multiline = true;
            txtKnowledgeContent.Name = "txtKnowledgeContent";
            txtKnowledgeContent.PlaceholderText = "Knowledge content...";
            txtKnowledgeContent.ScrollBars = ScrollBars.Vertical;
            txtKnowledgeContent.Size = new Size(560, 120);
            txtKnowledgeContent.TabIndex = 9;
            // 
            // btnAddKnowledge
            // 
            btnAddKnowledge.Location = new Point(472, 239);
            btnAddKnowledge.Name = "btnAddKnowledge";
            btnAddKnowledge.Size = new Size(94, 30);
            btnAddKnowledge.TabIndex = 10;
            btnAddKnowledge.Text = "Add";
            btnAddKnowledge.UseVisualStyleBackColor = true;
            btnAddKnowledge.Click += btnAddKnowledge_Click;
            // 
            // gbKnowledge
            // 
            gbKnowledge.Controls.Add(lblKnowledgeTitle);
            gbKnowledge.Controls.Add(txtKnowledgeTitle);
            gbKnowledge.Controls.Add(lblKnowledgeContent);
            gbKnowledge.Controls.Add(txtKnowledgeContent);
            gbKnowledge.Controls.Add(btnAddKnowledge);
            gbKnowledge.Location = new Point(12, 470);
            gbKnowledge.Name = "gbKnowledge";
            gbKnowledge.Size = new Size(580, 280);
            gbKnowledge.TabIndex = 11;
            gbKnowledge.TabStop = false;
            gbKnowledge.Text = "Add Knowledge";
            // 
            // AIAssistantForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(604, 761);
            Controls.Add(lblProject);
            Controls.Add(cmbProject);
            Controls.Add(gbChat);
            Controls.Add(gbKnowledge);
            Name = "AIAssistantForm";
            Text = "AI Assistant";
            Load += AIAssistantForm_Load;
            gbChat.ResumeLayout(false);
            gbChat.PerformLayout();
            gbKnowledge.ResumeLayout(false);
            gbKnowledge.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
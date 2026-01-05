namespace ProjectManagement.WinForms
{
    partial class AttachmentForm
    {
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flpAttachments;
        private Button btnUpload;
        private Button btnClose;

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
            flpAttachments = new FlowLayoutPanel();
            btnUpload = new Button();
            btnClose = new Button();
            SuspendLayout();
            
            // flpAttachments
            flpAttachments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpAttachments.AutoScroll = true;
            flpAttachments.FlowDirection = FlowDirection.TopDown;
            flpAttachments.Location = new Point(12, 50);
            flpAttachments.Size = new Size(560, 350);
            flpAttachments.WrapContents = false;
            
            // btnUpload
            btnUpload.Location = new Point(12, 12);
            btnUpload.Size = new Size(100, 30);
            btnUpload.Text = "📎 Upload";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            
            // btnClose
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Location = new Point(472, 12);
            btnClose.Size = new Size(100, 30);
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            
            // AttachmentForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 411);
            Controls.Add(flpAttachments);
            Controls.Add(btnUpload);
            Controls.Add(btnClose);
            MinimumSize = new Size(600, 450);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Task Attachments";
            Load += AttachmentForm_Load;
            
            ResumeLayout(false);
        }
    }
}
namespace ProjectManagement.WinForms
{
    public partial class ImageViewerForm : Form
    {
        private PictureBox pictureBox;
        private Button btnClose;

        public ImageViewerForm(Image image, string title)
        {
            InitializeComponent();
            this.Text = title;
            this.WindowState = FormWindowState.Maximized;
            this.KeyPreview = true;
            
            pictureBox.Image = image;
            
            // Close on Escape key
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); };
            
            // Close on click
            pictureBox.Click += (s, e) => this.Close();
            btnClose.Click += (s, e) => this.Close();
        }

        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            btnClose = new Button();
            SuspendLayout();
            
            // pictureBox
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BackColor = Color.Black;
            
            // btnClose
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.Red;
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(this.Width - 50, 10);
            btnClose.Size = new Size(40, 30);
            btnClose.Text = "✕";
            btnClose.UseVisualStyleBackColor = false;
            
            // ImageViewerForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 600);
            Controls.Add(pictureBox);
            Controls.Add(btnClose);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Image Viewer";
            
            ResumeLayout(false);
        }
    }
}
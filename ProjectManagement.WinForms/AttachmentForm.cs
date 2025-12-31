using ProjectManagement.Entities.Models.DTOs;
using ProjectManagement.WinForms.Services;

namespace ProjectManagement.WinForms
{
    public partial class AttachmentForm : Form
    {
        private readonly ApiService _apiService;
        private readonly int _taskId;
        private List<TaskAttachmentDto> _attachments = new();

        public AttachmentForm(ApiService apiService, int taskId)
        {
            InitializeComponent();
            _apiService = apiService;
            _taskId = taskId;
        }

        private async void AttachmentForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Task #{_taskId} - Attachments";
            await LoadAttachments();
        }

        private async Task LoadAttachments()
        {
            try
            {
                _attachments = await _apiService.GetTaskAttachmentsAsync(_taskId);
                DisplayAttachments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading attachments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayAttachments()
        {
            flpAttachments.Controls.Clear();

            foreach (var attachment in _attachments)
            {
                var panel = new Panel
                {
                    Width = flpAttachments.Width - 20,
                    Height = attachment.FileType == "image" ? 200 : 60,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5)
                };

                var lblName = new Label
                {
                    Text = attachment.FileName,
                    Location = new Point(10, 10),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };

                var lblInfo = new Label
                {
                    Text = $"{attachment.FileType.ToUpper()} • {FormatFileSize(attachment.FileSize)} • {attachment.UploadedByUserName} • {attachment.UploadedAt:MM/dd HH:mm}",
                    Location = new Point(10, 30),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = Color.Gray
                };

                var btnDownload = new Button
                {
                    Text = "📥",
                    Size = new Size(30, 25),
                    Location = new Point(panel.Width - 70, 15),
                    Anchor = AnchorStyles.Right
                };
                btnDownload.Click += (s, e) => DownloadAttachment(attachment.Id, attachment.FileName);

                var btnDelete = new Button
                {
                    Text = "🗑️",
                    Size = new Size(30, 25),
                    Location = new Point(panel.Width - 35, 15),
                    Anchor = AnchorStyles.Right
                };
                btnDelete.Click += (s, e) => DeleteAttachment(attachment.Id);

                panel.Controls.AddRange(new Control[] { lblName, lblInfo, btnDownload, btnDelete });

                // Add image preview for images
                if (attachment.FileType == "image")
                {
                    var pictureBox = new PictureBox
                    {
                        Location = new Point(10, 50),
                        Size = new Size(panel.Width - 20, 140),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BorderStyle = BorderStyle.FixedSingle,
                        Anchor = AnchorStyles.Left | AnchorStyles.Right,
                        Cursor = Cursors.Hand
                    };
                    
                    // Load image asynchronously
                    LoadImageAsync(pictureBox, attachment.Id, attachment.FileName);
                    panel.Controls.Add(pictureBox);
                }
                else if (attachment.FileType == "video")
                {
                    var lblVideo = new Label
                    {
                        Text = "🎬 Video File - Click Download to view",
                        Location = new Point(10, 50),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                        ForeColor = Color.Blue
                    };
                    panel.Controls.Add(lblVideo);
                }

                flpAttachments.Controls.Add(panel);
            }

            if (_attachments.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = "No attachments yet. Click 'Upload' to add files.",
                    AutoSize = true,
                    ForeColor = Color.Gray,
                    Margin = new Padding(10)
                };
                flpAttachments.Controls.Add(lblEmpty);
            }
        }

        private async void LoadImageAsync(PictureBox pictureBox, int attachmentId, string fileName)
        {
            try
            {
                var tempPath = Path.GetTempFileName();
                var success = await _apiService.DownloadAttachmentAsync(attachmentId, tempPath);
                if (success && File.Exists(tempPath))
                {
                    using (var stream = new FileStream(tempPath, FileMode.Open, FileAccess.Read))
                    {
                        var image = Image.FromStream(stream);
                        pictureBox.Image = image;
                        
                        // Add click event to view full size
                        pictureBox.Click += (s, e) => {
                            var viewer = new ImageViewerForm((Image)image.Clone(), fileName);
                            viewer.ShowDialog();
                        };
                    }
                    File.Delete(tempPath); // Clean up temp file
                }
                else
                {
                    pictureBox.BackColor = Color.LightGray;
                    var lblError = new Label
                    {
                        Text = "Failed to load image",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Red
                    };
                    pictureBox.Controls.Add(lblError);
                }
            }
            catch (Exception ex)
            {
                pictureBox.BackColor = Color.LightGray;
                var lblError = new Label
                {
                    Text = $"Error: {ex.Message}",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Red
                };
                pictureBox.Controls.Add(lblError);
            }
        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Images and Videos|*.jpg;*.jpeg;*.png;*.gif;*.mp4;*.avi;*.mov;*.wmv",
                Title = "Select Image or Video"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    btnUpload.Enabled = false;
                    btnUpload.Text = "Uploading...";

                    var success = await _apiService.UploadAttachmentAsync(_taskId, openFileDialog.FileName);
                    if (success)
                    {
                        MessageBox.Show("File uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadAttachments();
                    }
                    else
                    {
                        MessageBox.Show("Failed to upload file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnUpload.Enabled = true;
                    btnUpload.Text = "📎 Upload";
                }
            }
        }

        private async void DownloadAttachment(int attachmentId, string fileName)
        {
            using var saveFileDialog = new SaveFileDialog
            {
                FileName = fileName,
                Title = "Save Attachment"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var success = await _apiService.DownloadAttachmentAsync(attachmentId, saveFileDialog.FileName);
                    if (success)
                    {
                        MessageBox.Show("File downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to download file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error downloading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void DeleteAttachment(int attachmentId)
        {
            var result = MessageBox.Show("Are you sure you want to delete this attachment?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var success = await _apiService.DeleteAttachmentAsync(attachmentId);
                    if (success)
                    {
                        MessageBox.Show("Attachment deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadAttachments();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete attachment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting attachment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
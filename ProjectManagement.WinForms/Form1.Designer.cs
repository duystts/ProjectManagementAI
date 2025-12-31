namespace ProjectManagement.WinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tableLayoutPanel;
        private FlowLayoutPanel flpTodo, flpProgress, flpPendingReview, flpDone;
        private Button btnAddTask;
        private Button btnBack;

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
            tableLayoutPanel = new TableLayoutPanel();
            flpTodo = new FlowLayoutPanel();
            flpProgress = new FlowLayoutPanel();
            flpPendingReview = new FlowLayoutPanel();
            flpDone = new FlowLayoutPanel();
            btnAddTask = new Button();
            btnBack = new Button();
            
            var lblTodo = new Label();
            var lblProgress = new Label();
            var lblPendingReview = new Label();
            var lblDone = new Label();
            
            SuspendLayout();
            
            // tableLayoutPanel
            tableLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel.ColumnCount = 4;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel.Location = new Point(12, 50);
            tableLayoutPanel.Size = new Size(1076, 538); // ClientSize.Width - 24, ClientSize.Height - 62
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            
            // Labels
            lblTodo.Text = "TO DO";
            lblTodo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTodo.TextAlign = ContentAlignment.MiddleCenter;
            lblTodo.Dock = DockStyle.Fill;
            lblTodo.BackColor = Color.LightGray;
            
            lblProgress.Text = "IN PROGRESS";
            lblProgress.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblProgress.TextAlign = ContentAlignment.MiddleCenter;
            lblProgress.Dock = DockStyle.Fill;
            lblProgress.BackColor = Color.LightBlue;

            lblPendingReview.Text = "PENDING REVIEW";
            lblPendingReview.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPendingReview.TextAlign = ContentAlignment.MiddleCenter;
            lblPendingReview.Dock = DockStyle.Fill;
            lblPendingReview.BackColor = Color.Khaki;
            
            lblDone.Text = "DONE";
            lblDone.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDone.TextAlign = ContentAlignment.MiddleCenter;
            lblDone.Dock = DockStyle.Fill;
            lblDone.BackColor = Color.LightGreen;
            
            // FlowLayoutPanels
            flpTodo.Dock = DockStyle.Fill;
            flpTodo.AutoScroll = true;
            flpTodo.FlowDirection = FlowDirection.TopDown;
            flpTodo.WrapContents = false;
            flpTodo.BackColor = Color.FromArgb(245, 245, 245);
            
            flpProgress.Dock = DockStyle.Fill;
            flpProgress.AutoScroll = true;
            flpProgress.FlowDirection = FlowDirection.TopDown;
            flpProgress.WrapContents = false;
            flpProgress.BackColor = Color.FromArgb(240, 248, 255);

            flpPendingReview.Dock = DockStyle.Fill;
            flpPendingReview.AutoScroll = true;
            flpPendingReview.FlowDirection = FlowDirection.TopDown;
            flpPendingReview.WrapContents = false;
            flpPendingReview.BackColor = Color.FromArgb(255, 255, 224); // LightYellow
            
            flpDone.Dock = DockStyle.Fill;
            flpDone.AutoScroll = true;
            flpDone.FlowDirection = FlowDirection.TopDown;
            flpDone.WrapContents = false;
            flpDone.BackColor = Color.FromArgb(240, 255, 240);
            
            // Add controls to table
            tableLayoutPanel.Controls.Add(lblTodo, 0, 0);
            tableLayoutPanel.Controls.Add(lblProgress, 1, 0);
            tableLayoutPanel.Controls.Add(lblPendingReview, 2, 0);
            tableLayoutPanel.Controls.Add(lblDone, 3, 0);
            tableLayoutPanel.Controls.Add(flpTodo, 0, 1);
            tableLayoutPanel.Controls.Add(flpProgress, 1, 1);
            tableLayoutPanel.Controls.Add(flpPendingReview, 2, 1);
            tableLayoutPanel.Controls.Add(flpDone, 3, 1);
            
            // btnAddTask
            btnAddTask.Location = new Point(12, 12);
            btnAddTask.Name = "btnAddTask";
            btnAddTask.Size = new Size(100, 30);
            btnAddTask.TabIndex = 0;
            btnAddTask.Text = "Add Task";
            btnAddTask.UseVisualStyleBackColor = true;
            btnAddTask.Click += new EventHandler(btnAddTask_Click);
            
            // btnBack
            btnBack.Location = new Point(120, 12);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(100, 30);
            btnBack.TabIndex = 1;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += new EventHandler(btnBack_Click);
            
            // Form1
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 600);
            Controls.Add(btnAddTask);
            Controls.Add(btnBack);
            Controls.Add(tableLayoutPanel);
            Name = "Form1";
            Text = "Project Management - Kanban Board";
            Load += Form1_Load;
            
            ResumeLayout(false);
        }
    }
}
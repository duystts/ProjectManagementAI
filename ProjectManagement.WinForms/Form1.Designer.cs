namespace ProjectManagement.WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                _apiService?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            flpTodo = new FlowLayoutPanel();
            flpProgress = new FlowLayoutPanel();
            flpDone = new FlowLayoutPanel();
            
            var lblTodo = new Label();
            var lblProgress = new Label();
            var lblDone = new Label();
            
            SuspendLayout();
            
            // tableLayoutPanel
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tableLayoutPanel.Dock = DockStyle.Fill;
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
            
            flpDone.Dock = DockStyle.Fill;
            flpDone.AutoScroll = true;
            flpDone.FlowDirection = FlowDirection.TopDown;
            flpDone.WrapContents = false;
            flpDone.BackColor = Color.FromArgb(240, 255, 240);
            
            // Add controls to table
            tableLayoutPanel.Controls.Add(lblTodo, 0, 0);
            tableLayoutPanel.Controls.Add(lblProgress, 1, 0);
            tableLayoutPanel.Controls.Add(lblDone, 2, 0);
            tableLayoutPanel.Controls.Add(flpTodo, 0, 1);
            tableLayoutPanel.Controls.Add(flpProgress, 1, 1);
            tableLayoutPanel.Controls.Add(flpDone, 2, 1);
            
            // Form1
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(tableLayoutPanel);
            Name = "Form1";
            Text = "Project Management - Kanban Board";
            Load += Form1_Load;
            
            ResumeLayout(false);
        }

        #endregion
    }
}

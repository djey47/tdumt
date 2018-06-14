namespace TDUModdingTools.gui.dbeditor.db
{
    partial class DbCheckDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbCheckDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.problemsListView = new System.Windows.Forms.ListView();
            this.lineColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cellColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.corruptionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.topicColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cultureColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.loadButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.problemsToolStrip = new System.Windows.Forms.ToolStrip();
            this.checkAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.unCheckAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.fixButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.backupLocationTextBox = new System.Windows.Forms.TextBox();
            this.browseDBButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.problemsToolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(514, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "This tool can detect many corrupted parts in your database and fix them. A clean " +
                "database backup is needed.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // problemsListView
            // 
            this.problemsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.problemsListView.CheckBoxes = true;
            this.problemsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lineColumnHeader,
            this.cellColumnHeader,
            this.corruptionColumnHeader,
            this.topicColumnHeader,
            this.valueColumnHeader,
            this.cultureColumnHeader,
            this.addColumnHeader});
            this.problemsListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            listViewGroup1.Header = "ListViewGroup";
            listViewGroup1.Name = "listViewGroup1";
            this.problemsListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.problemsListView.Location = new System.Drawing.Point(6, 21);
            this.problemsListView.MultiSelect = false;
            this.problemsListView.Name = "problemsListView";
            this.problemsListView.Size = new System.Drawing.Size(588, 232);
            this.problemsListView.TabIndex = 0;
            this.problemsListView.UseCompatibleStateImageBehavior = false;
            this.problemsListView.View = System.Windows.Forms.View.Details;
            // 
            // lineColumnHeader
            // 
            this.lineColumnHeader.Text = "Line#";
            this.lineColumnHeader.Width = 75;
            // 
            // cellColumnHeader
            // 
            this.cellColumnHeader.Text = "Column";
            this.cellColumnHeader.Width = 150;
            // 
            // corruptionColumnHeader
            // 
            this.corruptionColumnHeader.Text = "Corruption";
            this.corruptionColumnHeader.Width = 150;
            // 
            // topicColumnHeader
            // 
            this.topicColumnHeader.Text = "Target topic";
            this.topicColumnHeader.Width = 125;
            // 
            // valueColumnHeader
            // 
            this.valueColumnHeader.Text = "Corrupted value";
            this.valueColumnHeader.Width = 100;
            // 
            // cultureColumnHeader
            // 
            this.cultureColumnHeader.Text = "Culture";
            // 
            // addColumnHeader
            // 
            this.addColumnHeader.Text = "Additional information";
            this.addColumnHeader.Width = 300;
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.loadButton.Location = new System.Drawing.Point(532, 12);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(80, 58);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Check";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.problemsToolStrip);
            this.groupBox1.Controls.Add(this.problemsListView);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 284);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Problems";
            // 
            // problemsToolStrip
            // 
            this.problemsToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.problemsToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.problemsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.problemsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllToolStripButton,
            this.unCheckAllToolStripButton});
            this.problemsToolStrip.Location = new System.Drawing.Point(3, 256);
            this.problemsToolStrip.Name = "problemsToolStrip";
            this.problemsToolStrip.Size = new System.Drawing.Size(594, 25);
            this.problemsToolStrip.TabIndex = 1;
            // 
            // checkAllToolStripButton
            // 
            this.checkAllToolStripButton.AutoToolTip = false;
            this.checkAllToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.checkAllToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("checkAllToolStripButton.Image")));
            this.checkAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.checkAllToolStripButton.Name = "checkAllToolStripButton";
            this.checkAllToolStripButton.Size = new System.Drawing.Size(58, 22);
            this.checkAllToolStripButton.Text = "Check all";
            this.checkAllToolStripButton.Click += new System.EventHandler(this.checkAllToolStripButton_Click);
            // 
            // unCheckAllToolStripButton
            // 
            this.unCheckAllToolStripButton.AutoToolTip = false;
            this.unCheckAllToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.unCheckAllToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("unCheckAllToolStripButton.Image")));
            this.unCheckAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.unCheckAllToolStripButton.Name = "unCheckAllToolStripButton";
            this.unCheckAllToolStripButton.Size = new System.Drawing.Size(72, 22);
            this.unCheckAllToolStripButton.Text = "Uncheck all";
            this.unCheckAllToolStripButton.Click += new System.EventHandler(this.unCheckAllToolStripButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 424);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(624, 22);
            this.statusStrip.TabIndex = 7;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel.Text = "Ready.";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBar.Step = 1;
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // fixButton
            // 
            this.fixButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fixButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fixButton.Location = new System.Drawing.Point(481, 372);
            this.fixButton.Name = "fixButton";
            this.fixButton.Size = new System.Drawing.Size(131, 49);
            this.fixButton.TabIndex = 6;
            this.fixButton.Text = "Fix selected issues";
            this.fixButton.UseVisualStyleBackColor = true;
            this.fixButton.Click += new System.EventHandler(this.fixButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 376);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "*Clean* backup location:";
            // 
            // backupLocationTextBox
            // 
            this.backupLocationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.backupLocationTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backupLocationTextBox.Location = new System.Drawing.Point(27, 393);
            this.backupLocationTextBox.Name = "backupLocationTextBox";
            this.backupLocationTextBox.Size = new System.Drawing.Size(400, 22);
            this.backupLocationTextBox.TabIndex = 4;
            // 
            // browseDBButton
            // 
            this.browseDBButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browseDBButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseDBButton.Location = new System.Drawing.Point(433, 392);
            this.browseDBButton.Name = "browseDBButton";
            this.browseDBButton.Size = new System.Drawing.Size(27, 23);
            this.browseDBButton.TabIndex = 5;
            this.browseDBButton.Text = "...";
            this.browseDBButton.UseVisualStyleBackColor = true;
            this.browseDBButton.Click += new System.EventHandler(this.browseDBButton_Click);
            // 
            // DbCheckDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 446);
            this.Controls.Add(this.browseDBButton);
            this.Controls.Add(this.backupLocationTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fixButton);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DbCheckDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check & fix database...";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DbCheckDialog_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.problemsToolStrip.ResumeLayout(false);
            this.problemsToolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView problemsListView;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip problemsToolStrip;
        private System.Windows.Forms.ToolStripButton checkAllToolStripButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Button fixButton;
        private System.Windows.Forms.ToolStripButton unCheckAllToolStripButton;
        private System.Windows.Forms.ColumnHeader lineColumnHeader;
        private System.Windows.Forms.ColumnHeader corruptionColumnHeader;
        private System.Windows.Forms.ColumnHeader topicColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private System.Windows.Forms.ColumnHeader cultureColumnHeader;
        private System.Windows.Forms.ColumnHeader addColumnHeader;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox backupLocationTextBox;
        private System.Windows.Forms.Button browseDBButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ColumnHeader cellColumnHeader;
    }
}
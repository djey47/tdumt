namespace TDUModdingTools.gui.dbeditor
{
    partial class DbEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbEditorForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.topicInfoLabel = new System.Windows.Forms.Label();
            this.topicTabControl = new System.Windows.Forms.TabControl();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.searchToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.editResourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigateToReferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.selectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllMarksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.checkDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dbSourceFolderRadioButton = new System.Windows.Forms.RadioButton();
            this.dbSourceCurrentRadioButton = new System.Windows.Forms.RadioButton();
            this.inputFolderPath = new System.Windows.Forms.TextBox();
            this.browseDBButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 599);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.statusStrip);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.settingsButton);
            this.panel1.Controls.Add(this.loadButton);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 593);
            this.panel1.TabIndex = 0;
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 571);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(786, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.topicInfoLabel);
            this.groupBox1.Controls.Add(this.topicTabControl);
            this.groupBox1.Controls.Add(this.mainToolStrip);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 462);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contents";
            // 
            // topicInfoLabel
            // 
            this.topicInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.topicInfoLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.topicInfoLabel.Location = new System.Drawing.Point(9, 18);
            this.topicInfoLabel.Name = "topicInfoLabel";
            this.topicInfoLabel.Size = new System.Drawing.Size(753, 18);
            this.topicInfoLabel.TabIndex = 7;
            this.topicInfoLabel.Text = "<Table information>";
            this.topicInfoLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // topicTabControl
            // 
            this.topicTabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.topicTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.topicTabControl.Location = new System.Drawing.Point(9, 39);
            this.topicTabControl.Multiline = true;
            this.topicTabControl.Name = "topicTabControl";
            this.topicTabControl.SelectedIndex = 0;
            this.topicTabControl.Size = new System.Drawing.Size(753, 392);
            this.topicTabControl.TabIndex = 3;
            this.topicTabControl.SelectedIndexChanged += new System.EventHandler(this.topicTabControl_SelectedIndexChanged);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripButton,
            this.toolStripSeparator1,
            this.editToolStripDropDownButton,
            this.markToolStripDropDownButton,
            this.toolStripSeparator2,
            this.toolsToolStripDropDownButton});
            this.mainToolStrip.Location = new System.Drawing.Point(3, 434);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(762, 25);
            this.mainToolStrip.TabIndex = 2;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // searchToolStripButton
            // 
            this.searchToolStripButton.AutoToolTip = false;
            this.searchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.searchToolStripButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("searchToolStripButton.Image")));
            this.searchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchToolStripButton.Name = "searchToolStripButton";
            this.searchToolStripButton.Size = new System.Drawing.Size(60, 22);
            this.searchToolStripButton.Text = "Search...";
            this.searchToolStripButton.ToolTipText = "Retrieves values in the list";
            this.searchToolStripButton.Click += new System.EventHandler(this.searchToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // editToolStripDropDownButton
            // 
            this.editToolStripDropDownButton.AutoToolTip = false;
            this.editToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.toolStripSeparator4,
            this.editResourceToolStripMenuItem,
            this.navigateToReferenceToolStripMenuItem});
            this.editToolStripDropDownButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripDropDownButton.Image")));
            this.editToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolStripDropDownButton.Name = "editToolStripDropDownButton";
            this.editToolStripDropDownButton.Size = new System.Drawing.Size(41, 22);
            this.editToolStripDropDownButton.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.ToolTipText = "Copy selected cell(s) value(s) to clipboard";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(232, 6);
            // 
            // editResourceToolStripMenuItem
            // 
            this.editResourceToolStripMenuItem.Name = "editResourceToolStripMenuItem";
            this.editResourceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.editResourceToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.editResourceToolStripMenuItem.Text = "Modify resource...";
            this.editResourceToolStripMenuItem.ToolTipText = "Allows to quickly modify selected resource value";
            this.editResourceToolStripMenuItem.Click += new System.EventHandler(this.editResourceToolStripMenuItem_Click);
            // 
            // navigateToReferenceToolStripMenuItem
            // 
            this.navigateToReferenceToolStripMenuItem.Name = "navigateToReferenceToolStripMenuItem";
            this.navigateToReferenceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.navigateToReferenceToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.navigateToReferenceToolStripMenuItem.Text = "Navigate to reference";
            this.navigateToReferenceToolStripMenuItem.Click += new System.EventHandler(this.navigateToReferenceToolStripMenuItem_Click);
            // 
            // markToolStripDropDownButton
            // 
            this.markToolStripDropDownButton.AutoToolTip = false;
            this.markToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.markToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedToolStripMenuItem,
            this.toolStripSeparator3,
            this.clearAllMarksToolStripMenuItem});
            this.markToolStripDropDownButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.markToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("markToolStripDropDownButton.Image")));
            this.markToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.markToolStripDropDownButton.Name = "markToolStripDropDownButton";
            this.markToolStripDropDownButton.Size = new System.Drawing.Size(45, 22);
            this.markToolStripDropDownButton.Text = "Mark";
            // 
            // selectedToolStripMenuItem
            // 
            this.selectedToolStripMenuItem.Name = "selectedToolStripMenuItem";
            this.selectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.selectedToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.selectedToolStripMenuItem.Text = "Selected";
            this.selectedToolStripMenuItem.ToolTipText = "Highlights selected cell(s)";
            this.selectedToolStripMenuItem.Click += new System.EventHandler(this.selectedToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(187, 6);
            // 
            // clearAllMarksToolStripMenuItem
            // 
            this.clearAllMarksToolStripMenuItem.Name = "clearAllMarksToolStripMenuItem";
            this.clearAllMarksToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.clearAllMarksToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.clearAllMarksToolStripMenuItem.Text = "Clear all marks";
            this.clearAllMarksToolStripMenuItem.ToolTipText = "Remove highlight on all cells (displayed table)";
            this.clearAllMarksToolStripMenuItem.Click += new System.EventHandler(this.clearAllMarksToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolsToolStripDropDownButton
            // 
            this.toolsToolStripDropDownButton.AutoToolTip = false;
            this.toolsToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolsToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkDatabaseToolStripMenuItem});
            this.toolsToolStripDropDownButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolsToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("toolsToolStripDropDownButton.Image")));
            this.toolsToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsToolStripDropDownButton.Name = "toolsToolStripDropDownButton";
            this.toolsToolStripDropDownButton.Size = new System.Drawing.Size(49, 22);
            this.toolsToolStripDropDownButton.Text = "Tools";
            // 
            // checkDatabaseToolStripMenuItem
            // 
            this.checkDatabaseToolStripMenuItem.Name = "checkDatabaseToolStripMenuItem";
            this.checkDatabaseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)));
            this.checkDatabaseToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.checkDatabaseToolStripMenuItem.Text = "Check database...";
            this.checkDatabaseToolStripMenuItem.ToolTipText = "Allows to check and fix database integrity";
            this.checkDatabaseToolStripMenuItem.Click += new System.EventHandler(this.checkDatabaseToolStripMenuItem_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsButton.Location = new System.Drawing.Point(697, 59);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(80, 35);
            this.settingsButton.TabIndex = 4;
            this.settingsButton.Text = "Settings...";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.Location = new System.Drawing.Point(697, 15);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(80, 35);
            this.loadButton.TabIndex = 3;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.dbSourceFolderRadioButton);
            this.groupBox4.Controls.Add(this.dbSourceCurrentRadioButton);
            this.groupBox4.Controls.Add(this.inputFolderPath);
            this.groupBox4.Controls.Add(this.browseDBButton);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(9, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(682, 86);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Database";
            // 
            // dbSourceFolderRadioButton
            // 
            this.dbSourceFolderRadioButton.AutoSize = true;
            this.dbSourceFolderRadioButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbSourceFolderRadioButton.Location = new System.Drawing.Point(9, 54);
            this.dbSourceFolderRadioButton.Name = "dbSourceFolderRadioButton";
            this.dbSourceFolderRadioButton.Size = new System.Drawing.Size(91, 18);
            this.dbSourceFolderRadioButton.TabIndex = 4;
            this.dbSourceFolderRadioButton.Text = "From folder:";
            this.dbSourceFolderRadioButton.UseVisualStyleBackColor = true;
            this.dbSourceFolderRadioButton.CheckedChanged += new System.EventHandler(this.openRadioButton_CheckedChanged);
            // 
            // dbSourceCurrentRadioButton
            // 
            this.dbSourceCurrentRadioButton.AutoSize = true;
            this.dbSourceCurrentRadioButton.Checked = true;
            this.dbSourceCurrentRadioButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbSourceCurrentRadioButton.Location = new System.Drawing.Point(9, 21);
            this.dbSourceCurrentRadioButton.Name = "dbSourceCurrentRadioButton";
            this.dbSourceCurrentRadioButton.Size = new System.Drawing.Size(66, 18);
            this.dbSourceCurrentRadioButton.TabIndex = 1;
            this.dbSourceCurrentRadioButton.TabStop = true;
            this.dbSourceCurrentRadioButton.Text = "Current";
            this.dbSourceCurrentRadioButton.UseVisualStyleBackColor = true;
            this.dbSourceCurrentRadioButton.CheckedChanged += new System.EventHandler(this.dbSourceRadioButton_CheckedChanged);
            // 
            // inputFolderPath
            // 
            this.inputFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.inputFolderPath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputFolderPath.Location = new System.Drawing.Point(106, 53);
            this.inputFolderPath.Name = "inputFolderPath";
            this.inputFolderPath.Size = new System.Drawing.Size(537, 22);
            this.inputFolderPath.TabIndex = 5;
            // 
            // browseDBButton
            // 
            this.browseDBButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseDBButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseDBButton.Location = new System.Drawing.Point(649, 52);
            this.browseDBButton.Name = "browseDBButton";
            this.browseDBButton.Size = new System.Drawing.Size(27, 23);
            this.browseDBButton.TabIndex = 6;
            this.browseDBButton.Text = "...";
            this.browseDBButton.UseVisualStyleBackColor = true;
            this.browseDBButton.Click += new System.EventHandler(this.browseDBButton_Click);
            // 
            // DbEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 599);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DbEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TDUMT - Database Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DbEditorForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton dbSourceFolderRadioButton;
        private System.Windows.Forms.RadioButton dbSourceCurrentRadioButton;
        private System.Windows.Forms.TextBox inputFolderPath;
        private System.Windows.Forms.Button browseDBButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton searchToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.TabControl topicTabControl;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolStripDropDownButton editToolStripDropDownButton;
        private System.Windows.Forms.ToolStripDropDownButton markToolStripDropDownButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton toolsToolStripDropDownButton;
        private System.Windows.Forms.Label topicInfoLabel;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem clearAllMarksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editResourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem navigateToReferenceToolStripMenuItem;
    }
}
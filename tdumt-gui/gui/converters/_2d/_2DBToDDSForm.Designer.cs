namespace TDUModdingTools.gui.converters._2d
{
    partial class _2DBToDDSForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.source2DBFilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.newDDSFilePath = new System.Windows.Forms.TextBox();
            this.GoButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.sourceBrowseButton = new System.Windows.Forms.Button();
            this.newBrowseButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.converterTabControl = new System.Windows.Forms.TabControl();
            this.singleTabPage = new System.Windows.Forms.TabPage();
            this.file2InfoButton = new System.Windows.Forms.Button();
            this.file1InfoButton = new System.Windows.Forms.Button();
            this.manyTabPage = new System.Windows.Forms.TabPage();
            this.file4InfoButton = new System.Windows.Forms.Button();
            this._2DBLocationLink = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.targetBrowseButton = new System.Windows.Forms.Button();
            this.targetFolderTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.sourceBNKBrowseButton = new System.Windows.Forms.Button();
            this.sourceBNKTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.propertiesListView = new System.Windows.Forms.ListView();
            this.nameHeader = new System.Windows.Forms.ColumnHeader();
            this.valueHeader = new System.Windows.Forms.ColumnHeader();
            this.fileTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.converterTabControl.SuspendLayout();
            this.singleTabPage.SuspendLayout();
            this.manyTabPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.fileTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Source 2DB File:";
            // 
            // source2DBFilePath
            // 
            this.source2DBFilePath.AllowDrop = true;
            this.source2DBFilePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.source2DBFilePath.Location = new System.Drawing.Point(114, 66);
            this.source2DBFilePath.Name = "source2DBFilePath";
            this.source2DBFilePath.Size = new System.Drawing.Size(258, 22);
            this.source2DBFilePath.TabIndex = 2;
            this.source2DBFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.source2DBFilePath_DragDrop);
            this.source2DBFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.source2DBFilePath_DragEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "New DDS File:";
            // 
            // newDDSFilePath
            // 
            this.newDDSFilePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newDDSFilePath.Location = new System.Drawing.Point(114, 106);
            this.newDDSFilePath.Name = "newDDSFilePath";
            this.newDDSFilePath.Size = new System.Drawing.Size(258, 22);
            this.newDDSFilePath.TabIndex = 6;
            // 
            // GoButton
            // 
            this.GoButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.GoButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GoButton.Location = new System.Drawing.Point(12, 268);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(728, 23);
            this.GoButton.TabIndex = 3;
            this.GoButton.Text = "GO";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            this.openFileDialog1.Title = "Browse for file...";
            // 
            // sourceBrowseButton
            // 
            this.sourceBrowseButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceBrowseButton.Location = new System.Drawing.Point(378, 65);
            this.sourceBrowseButton.Name = "sourceBrowseButton";
            this.sourceBrowseButton.Size = new System.Drawing.Size(27, 23);
            this.sourceBrowseButton.TabIndex = 3;
            this.sourceBrowseButton.Text = "...";
            this.sourceBrowseButton.UseVisualStyleBackColor = true;
            this.sourceBrowseButton.Click += new System.EventHandler(this.sourceBrowseButton_Click);
            // 
            // newBrowseButton
            // 
            this.newBrowseButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newBrowseButton.Location = new System.Drawing.Point(378, 105);
            this.newBrowseButton.Name = "newBrowseButton";
            this.newBrowseButton.Size = new System.Drawing.Size(27, 23);
            this.newBrowseButton.TabIndex = 7;
            this.newBrowseButton.Text = "...";
            this.newBrowseButton.UseVisualStyleBackColor = true;
            this.newBrowseButton.Click += new System.EventHandler(this.newBrowseButton_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.CreatePrompt = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 45);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tip";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(375, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "You may drag and drop a 2DB file to \'Source 2DB File\' field directly.";
            // 
            // converterTabControl
            // 
            this.converterTabControl.Controls.Add(this.singleTabPage);
            this.converterTabControl.Controls.Add(this.manyTabPage);
            this.converterTabControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.converterTabControl.Location = new System.Drawing.Point(12, 12);
            this.converterTabControl.Name = "converterTabControl";
            this.converterTabControl.SelectedIndex = 0;
            this.converterTabControl.Size = new System.Drawing.Size(458, 250);
            this.converterTabControl.TabIndex = 0;
            this.converterTabControl.SelectedIndexChanged += new System.EventHandler(this.converterTabControl_SelectedIndexChanged);
            // 
            // singleTabPage
            // 
            this.singleTabPage.Controls.Add(this.file2InfoButton);
            this.singleTabPage.Controls.Add(this.file1InfoButton);
            this.singleTabPage.Controls.Add(this.newBrowseButton);
            this.singleTabPage.Controls.Add(this.source2DBFilePath);
            this.singleTabPage.Controls.Add(this.label2);
            this.singleTabPage.Controls.Add(this.label3);
            this.singleTabPage.Controls.Add(this.sourceBrowseButton);
            this.singleTabPage.Controls.Add(this.newDDSFilePath);
            this.singleTabPage.Controls.Add(this.groupBox1);
            this.singleTabPage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleTabPage.Location = new System.Drawing.Point(4, 22);
            this.singleTabPage.Name = "singleTabPage";
            this.singleTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.singleTabPage.Size = new System.Drawing.Size(450, 224);
            this.singleTabPage.TabIndex = 0;
            this.singleTabPage.Text = "Single";
            this.singleTabPage.UseVisualStyleBackColor = true;
            // 
            // file2InfoButton
            // 
            this.file2InfoButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file2InfoButton.Image = global::TDUModdingTools.Properties.Resources.arrow_forward_16;
            this.file2InfoButton.Location = new System.Drawing.Point(411, 105);
            this.file2InfoButton.Name = "file2InfoButton";
            this.file2InfoButton.Size = new System.Drawing.Size(27, 23);
            this.file2InfoButton.TabIndex = 8;
            this.file2InfoButton.UseVisualStyleBackColor = true;
            this.file2InfoButton.Click += new System.EventHandler(this.file2InfoButton_Click);
            // 
            // file1InfoButton
            // 
            this.file1InfoButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file1InfoButton.Image = global::TDUModdingTools.Properties.Resources.arrow_forward_16;
            this.file1InfoButton.Location = new System.Drawing.Point(411, 65);
            this.file1InfoButton.Name = "file1InfoButton";
            this.file1InfoButton.Size = new System.Drawing.Size(27, 23);
            this.file1InfoButton.TabIndex = 4;
            this.file1InfoButton.UseVisualStyleBackColor = true;
            this.file1InfoButton.Click += new System.EventHandler(this.file1InfoButton_Click);
            // 
            // manyTabPage
            // 
            this.manyTabPage.Controls.Add(this.file4InfoButton);
            this.manyTabPage.Controls.Add(this._2DBLocationLink);
            this.manyTabPage.Controls.Add(this.label8);
            this.manyTabPage.Controls.Add(this.targetBrowseButton);
            this.manyTabPage.Controls.Add(this.targetFolderTextBox);
            this.manyTabPage.Controls.Add(this.label6);
            this.manyTabPage.Controls.Add(this.label7);
            this.manyTabPage.Controls.Add(this.sourceBNKBrowseButton);
            this.manyTabPage.Controls.Add(this.sourceBNKTextBox);
            this.manyTabPage.Controls.Add(this.groupBox2);
            this.manyTabPage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manyTabPage.Location = new System.Drawing.Point(4, 22);
            this.manyTabPage.Name = "manyTabPage";
            this.manyTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.manyTabPage.Size = new System.Drawing.Size(450, 224);
            this.manyTabPage.TabIndex = 1;
            this.manyTabPage.Text = "Many";
            this.manyTabPage.UseVisualStyleBackColor = true;
            // 
            // file4InfoButton
            // 
            this.file4InfoButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file4InfoButton.Image = global::TDUModdingTools.Properties.Resources.arrow_forward_16;
            this.file4InfoButton.Location = new System.Drawing.Point(411, 65);
            this.file4InfoButton.Name = "file4InfoButton";
            this.file4InfoButton.Size = new System.Drawing.Size(27, 23);
            this.file4InfoButton.TabIndex = 9;
            this.file4InfoButton.UseVisualStyleBackColor = true;
            this.file4InfoButton.Click += new System.EventHandler(this.fileInfo4Button_Click);
            // 
            // _2DBLocationLink
            // 
            this._2DBLocationLink.Location = new System.Drawing.Point(113, 150);
            this._2DBLocationLink.Name = "_2DBLocationLink";
            this._2DBLocationLink.Size = new System.Drawing.Size(325, 14);
            this._2DBLocationLink.TabIndex = 8;
            this._2DBLocationLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._2DBLocationLink_LinkClicked);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 14);
            this.label8.TabIndex = 7;
            this.label8.Text = "2DB files location:";
            // 
            // targetBrowseButton
            // 
            this.targetBrowseButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.targetBrowseButton.Location = new System.Drawing.Point(378, 105);
            this.targetBrowseButton.Name = "targetBrowseButton";
            this.targetBrowseButton.Size = new System.Drawing.Size(27, 23);
            this.targetBrowseButton.TabIndex = 6;
            this.targetBrowseButton.Text = "...";
            this.targetBrowseButton.UseVisualStyleBackColor = true;
            this.targetBrowseButton.Click += new System.EventHandler(this.targetBrowseButton_Click);
            // 
            // targetFolderTextBox
            // 
            this.targetFolderTextBox.AllowDrop = true;
            this.targetFolderTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.targetFolderTextBox.Location = new System.Drawing.Point(114, 106);
            this.targetFolderTextBox.Name = "targetFolderTextBox";
            this.targetFolderTextBox.Size = new System.Drawing.Size(258, 22);
            this.targetFolderTextBox.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 14);
            this.label6.TabIndex = 1;
            this.label6.Text = "Source BNK File:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 14);
            this.label7.TabIndex = 4;
            this.label7.Text = "Target folder:";
            // 
            // sourceBNKBrowseButton
            // 
            this.sourceBNKBrowseButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceBNKBrowseButton.Location = new System.Drawing.Point(378, 65);
            this.sourceBNKBrowseButton.Name = "sourceBNKBrowseButton";
            this.sourceBNKBrowseButton.Size = new System.Drawing.Size(27, 23);
            this.sourceBNKBrowseButton.TabIndex = 3;
            this.sourceBNKBrowseButton.Text = "...";
            this.sourceBNKBrowseButton.UseVisualStyleBackColor = true;
            this.sourceBNKBrowseButton.Click += new System.EventHandler(this.sourceBNKBrowseButton_Click);
            // 
            // sourceBNKTextBox
            // 
            this.sourceBNKTextBox.AllowDrop = true;
            this.sourceBNKTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceBNKTextBox.Location = new System.Drawing.Point(114, 66);
            this.sourceBNKTextBox.Name = "sourceBNKTextBox";
            this.sourceBNKTextBox.Size = new System.Drawing.Size(258, 22);
            this.sourceBNKTextBox.TabIndex = 2;
            this.sourceBNKTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.sourceBNKTextBox_DragDrop);
            this.sourceBNKTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.sourceBNKTextBox_DragEnter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(438, 45);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tip";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Teal;
            this.label5.Location = new System.Drawing.Point(6, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(375, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "You may drag and drop a BNK file to \'Source BNK File\' field directly.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Teal;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(375, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "You may drag and drop a 2DB file to \'Source 2DB File\' field directly.";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 294);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(752, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // propertiesListView
            // 
            this.propertiesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.valueHeader});
            this.propertiesListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertiesListView.FullRowSelect = true;
            this.propertiesListView.GridLines = true;
            this.propertiesListView.Location = new System.Drawing.Point(6, 6);
            this.propertiesListView.MultiSelect = false;
            this.propertiesListView.Name = "propertiesListView";
            this.propertiesListView.Size = new System.Drawing.Size(244, 212);
            this.propertiesListView.TabIndex = 2;
            this.propertiesListView.UseCompatibleStateImageBehavior = false;
            this.propertiesListView.View = System.Windows.Forms.View.Details;
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "";
            this.nameHeader.Width = 125;
            // 
            // valueHeader
            // 
            this.valueHeader.Text = "";
            this.valueHeader.Width = 250;
            // 
            // fileTabControl
            // 
            this.fileTabControl.Controls.Add(this.tabPage1);
            this.fileTabControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileTabControl.Location = new System.Drawing.Point(476, 12);
            this.fileTabControl.Name = "fileTabControl";
            this.fileTabControl.SelectedIndex = 0;
            this.fileTabControl.Size = new System.Drawing.Size(264, 250);
            this.fileTabControl.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.propertiesListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(256, 224);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "File properties";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _2DBToDDSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 316);
            this.Controls.Add(this.fileTabControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.converterTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "_2DBToDDSForm";
            this.Text = "TDUMT - 2DB To DDS Converter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this._2DBToDDSForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.converterTabControl.ResumeLayout(false);
            this.singleTabPage.ResumeLayout(false);
            this.singleTabPage.PerformLayout();
            this.manyTabPage.ResumeLayout(false);
            this.manyTabPage.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.fileTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox source2DBFilePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox newDDSFilePath;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button sourceBrowseButton;
        private System.Windows.Forms.Button newBrowseButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl converterTabControl;
        private System.Windows.Forms.TabPage singleTabPage;
        private System.Windows.Forms.TabPage manyTabPage;
        private System.Windows.Forms.Button targetBrowseButton;
        private System.Windows.Forms.TextBox targetFolderTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button sourceBNKBrowseButton;
        private System.Windows.Forms.TextBox sourceBNKTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.LinkLabel _2DBLocationLink;
        private System.Windows.Forms.ListView propertiesListView;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader valueHeader;
        private System.Windows.Forms.Button file2InfoButton;
        private System.Windows.Forms.Button file1InfoButton;
        private System.Windows.Forms.Button file4InfoButton;
        private System.Windows.Forms.TabControl fileTabControl;
        private System.Windows.Forms.TabPage tabPage1;
    }
}
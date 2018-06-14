namespace TDUModdingTools.gui.converters._2d
{
    partial class DDSTo2DBForm
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
            this.original2DBFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sourceDDSFilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.new2DBFilePath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textureNameTextBox = new System.Windows.Forms.TextBox();
            this.overrideNameCheckBox = new System.Windows.Forms.CheckBox();
            this.file3InfoButton = new System.Windows.Forms.Button();
            this.keepHeaderCheckbox = new System.Windows.Forms.CheckBox();
            this.originalBrowseButton = new System.Windows.Forms.Button();
            this.GoButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.sourceBrowseButton = new System.Windows.Forms.Button();
            this.newBrowseButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.propertiesListView = new System.Windows.Forms.ListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.file1InfoButton = new System.Windows.Forms.Button();
            this.file2InfoButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.mipMapTextBox = new System.Windows.Forms.TextBox();
            this.forceMipmapCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // original2DBFilePath
            // 
            this.original2DBFilePath.AllowDrop = true;
            this.original2DBFilePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.original2DBFilePath.Location = new System.Drawing.Point(179, 19);
            this.original2DBFilePath.Name = "original2DBFilePath";
            this.original2DBFilePath.Size = new System.Drawing.Size(183, 22);
            this.original2DBFilePath.TabIndex = 1;
            this.original2DBFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.original2DBFilePath_DragDrop);
            this.original2DBFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.filePath_DragEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Source DDS File:";
            // 
            // sourceDDSFilePath
            // 
            this.sourceDDSFilePath.AllowDrop = true;
            this.sourceDDSFilePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceDDSFilePath.Location = new System.Drawing.Point(125, 82);
            this.sourceDDSFilePath.Name = "sourceDDSFilePath";
            this.sourceDDSFilePath.Size = new System.Drawing.Size(268, 22);
            this.sourceDDSFilePath.TabIndex = 2;
            this.sourceDDSFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.sourceDDSFilePath_DragDrop);
            this.sourceDDSFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.filePath_DragEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "New 2DB File:";
            // 
            // new2DBFilePath
            // 
            this.new2DBFilePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new2DBFilePath.Location = new System.Drawing.Point(125, 113);
            this.new2DBFilePath.Name = "new2DBFilePath";
            this.new2DBFilePath.Size = new System.Drawing.Size(268, 22);
            this.new2DBFilePath.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mipMapTextBox);
            this.groupBox1.Controls.Add(this.forceMipmapCheckBox);
            this.groupBox1.Controls.Add(this.textureNameTextBox);
            this.groupBox1.Controls.Add(this.overrideNameCheckBox);
            this.groupBox1.Controls.Add(this.file3InfoButton);
            this.groupBox1.Controls.Add(this.keepHeaderCheckbox);
            this.groupBox1.Controls.Add(this.originalBrowseButton);
            this.groupBox1.Controls.Add(this.original2DBFilePath);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(25, 155);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 98);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // textureNameTextBox
            // 
            this.textureNameTextBox.AllowDrop = true;
            this.textureNameTextBox.Enabled = false;
            this.textureNameTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textureNameTextBox.Location = new System.Drawing.Point(179, 43);
            this.textureNameTextBox.MaxLength = 16;
            this.textureNameTextBox.Name = "textureNameTextBox";
            this.textureNameTextBox.Size = new System.Drawing.Size(183, 22);
            this.textureNameTextBox.TabIndex = 5;
            // 
            // overrideNameCheckBox
            // 
            this.overrideNameCheckBox.AutoSize = true;
            this.overrideNameCheckBox.Location = new System.Drawing.Point(13, 45);
            this.overrideNameCheckBox.Name = "overrideNameCheckBox";
            this.overrideNameCheckBox.Size = new System.Drawing.Size(156, 18);
            this.overrideNameCheckBox.TabIndex = 4;
            this.overrideNameCheckBox.Text = "Texture name override:";
            this.overrideNameCheckBox.UseVisualStyleBackColor = true;
            this.overrideNameCheckBox.CheckedChanged += new System.EventHandler(this.overrideNameCheckBox_CheckedChanged);
            // 
            // file3InfoButton
            // 
            this.file3InfoButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file3InfoButton.Image = global::TDUModdingTools.Properties.Resources.arrow_forward_16;
            this.file3InfoButton.Location = new System.Drawing.Point(401, 18);
            this.file3InfoButton.Name = "file3InfoButton";
            this.file3InfoButton.Size = new System.Drawing.Size(27, 23);
            this.file3InfoButton.TabIndex = 3;
            this.file3InfoButton.UseVisualStyleBackColor = true;
            this.file3InfoButton.Click += new System.EventHandler(this.file3InfoButton_Click);
            // 
            // keepHeaderCheckbox
            // 
            this.keepHeaderCheckbox.AutoSize = true;
            this.keepHeaderCheckbox.Checked = true;
            this.keepHeaderCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.keepHeaderCheckbox.Location = new System.Drawing.Point(13, 21);
            this.keepHeaderCheckbox.Name = "keepHeaderCheckbox";
            this.keepHeaderCheckbox.Size = new System.Drawing.Size(160, 18);
            this.keepHeaderCheckbox.TabIndex = 0;
            this.keepHeaderCheckbox.Text = "Keep original file header:";
            this.keepHeaderCheckbox.UseVisualStyleBackColor = true;
            this.keepHeaderCheckbox.CheckedChanged += new System.EventHandler(this.keepHeaderCheckbox_CheckedChanged);
            // 
            // originalBrowseButton
            // 
            this.originalBrowseButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.originalBrowseButton.Location = new System.Drawing.Point(368, 18);
            this.originalBrowseButton.Name = "originalBrowseButton";
            this.originalBrowseButton.Size = new System.Drawing.Size(27, 23);
            this.originalBrowseButton.TabIndex = 2;
            this.originalBrowseButton.Text = "...";
            this.originalBrowseButton.UseVisualStyleBackColor = true;
            this.originalBrowseButton.Click += new System.EventHandler(this.browseOriginalButton_Click);
            // 
            // GoButton
            // 
            this.GoButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GoButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GoButton.Location = new System.Drawing.Point(13, 260);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(725, 23);
            this.GoButton.TabIndex = 10;
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
            this.sourceBrowseButton.Location = new System.Drawing.Point(399, 81);
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
            this.newBrowseButton.Location = new System.Drawing.Point(399, 112);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(456, 54);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tip";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Teal;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(441, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "You may drag and drop 2DB and DDS files to corresponding fields directly.";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 292);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(749, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // propertiesListView
            // 
            this.propertiesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.valueHeader});
            this.propertiesListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertiesListView.FullRowSelect = true;
            this.propertiesListView.GridLines = true;
            this.propertiesListView.Location = new System.Drawing.Point(6, 17);
            this.propertiesListView.MultiSelect = false;
            this.propertiesListView.Name = "propertiesListView";
            this.propertiesListView.Size = new System.Drawing.Size(252, 219);
            this.propertiesListView.TabIndex = 0;
            this.propertiesListView.UseCompatibleStateImageBehavior = false;
            this.propertiesListView.View = System.Windows.Forms.View.Details;
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "";
            this.nameHeader.Width = 133;
            // 
            // valueHeader
            // 
            this.valueHeader.Text = "";
            this.valueHeader.Width = 250;
            // 
            // file1InfoButton
            // 
            this.file1InfoButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file1InfoButton.Image = global::TDUModdingTools.Properties.Resources.arrow_forward_16;
            this.file1InfoButton.Location = new System.Drawing.Point(432, 81);
            this.file1InfoButton.Name = "file1InfoButton";
            this.file1InfoButton.Size = new System.Drawing.Size(27, 23);
            this.file1InfoButton.TabIndex = 4;
            this.file1InfoButton.UseVisualStyleBackColor = true;
            this.file1InfoButton.Click += new System.EventHandler(this.file1InfoButton_Click);
            // 
            // file2InfoButton
            // 
            this.file2InfoButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file2InfoButton.Image = global::TDUModdingTools.Properties.Resources.arrow_forward_16;
            this.file2InfoButton.Location = new System.Drawing.Point(432, 112);
            this.file2InfoButton.Name = "file2InfoButton";
            this.file2InfoButton.Size = new System.Drawing.Size(27, 23);
            this.file2InfoButton.TabIndex = 8;
            this.file2InfoButton.UseVisualStyleBackColor = true;
            this.file2InfoButton.Click += new System.EventHandler(this.file2InfoButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.propertiesListView);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(474, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(264, 242);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File properties";
            // 
            // mipMapTextBox
            // 
            this.mipMapTextBox.AllowDrop = true;
            this.mipMapTextBox.Enabled = false;
            this.mipMapTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mipMapTextBox.Location = new System.Drawing.Point(179, 67);
            this.mipMapTextBox.MaxLength = 16;
            this.mipMapTextBox.Name = "mipMapTextBox";
            this.mipMapTextBox.Size = new System.Drawing.Size(35, 22);
            this.mipMapTextBox.TabIndex = 7;
            // 
            // forceMipmapCheckBox
            // 
            this.forceMipmapCheckBox.AutoSize = true;
            this.forceMipmapCheckBox.Location = new System.Drawing.Point(13, 69);
            this.forceMipmapCheckBox.Name = "forceMipmapCheckBox";
            this.forceMipmapCheckBox.Size = new System.Drawing.Size(142, 18);
            this.forceMipmapCheckBox.TabIndex = 6;
            this.forceMipmapCheckBox.Text = "Force mipmap count:";
            this.forceMipmapCheckBox.UseVisualStyleBackColor = true;
            this.forceMipmapCheckBox.CheckedChanged += new System.EventHandler(this.forceMipmapCheckBox_CheckedChanged);
            // 
            // DDSTo2DBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 314);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.file2InfoButton);
            this.Controls.Add(this.file1InfoButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.newBrowseButton);
            this.Controls.Add(this.sourceBrowseButton);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.new2DBFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sourceDDSFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "DDSTo2DBForm";
            this.Text = "TDUMT - DDS To 2DB Converter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DDSTo2DBForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox original2DBFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sourceDDSFilePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox new2DBFilePath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.Button originalBrowseButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button sourceBrowseButton;
        private System.Windows.Forms.Button newBrowseButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.CheckBox keepHeaderCheckbox;
        private System.Windows.Forms.ListView propertiesListView;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader valueHeader;
        private System.Windows.Forms.Button file3InfoButton;
        private System.Windows.Forms.Button file1InfoButton;
        private System.Windows.Forms.Button file2InfoButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textureNameTextBox;
        private System.Windows.Forms.CheckBox overrideNameCheckBox;
        private System.Windows.Forms.TextBox mipMapTextBox;
        private System.Windows.Forms.CheckBox forceMipmapCheckBox;
    }
}
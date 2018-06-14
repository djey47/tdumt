namespace TDUModdingTools.gui.maptool
{
    partial class FileListingForm
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.keyPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tduPath = new System.Windows.Forms.TextBox();
            this.analyzeButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.exportOKResultsButton = new System.Windows.Forms.Button();
            this.fileCountLabel = new System.Windows.Forms.Label();
            this.fileList = new System.Windows.Forms.ListView();
            this.numHeader = new System.Windows.Forms.ColumnHeader();
            this.resultHeader = new System.Windows.Forms.ColumnHeader();
            this.fileNameHeader = new System.Windows.Forms.ColumnHeader();
            this.idHeader = new System.Windows.Forms.ColumnHeader();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.copyButton = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.keyPath);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.tduPath);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(12, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(467, 81);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Locations";
            // 
            // keyPath
            // 
            this.keyPath.Location = new System.Drawing.Point(47, 49);
            this.keyPath.Name = "keyPath";
            this.keyPath.ReadOnly = true;
            this.keyPath.Size = new System.Drawing.Size(414, 22);
            this.keyPath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "KEY:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "TDU:";
            // 
            // tduPath
            // 
            this.tduPath.Location = new System.Drawing.Point(47, 21);
            this.tduPath.Name = "tduPath";
            this.tduPath.ReadOnly = true;
            this.tduPath.Size = new System.Drawing.Size(414, 22);
            this.tduPath.TabIndex = 1;
            // 
            // analyzeButton
            // 
            this.analyzeButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analyzeButton.Location = new System.Drawing.Point(485, 12);
            this.analyzeButton.Name = "analyzeButton";
            this.analyzeButton.Size = new System.Drawing.Size(76, 76);
            this.analyzeButton.TabIndex = 1;
            this.analyzeButton.Text = "Analyze !";
            this.analyzeButton.UseVisualStyleBackColor = true;
            this.analyzeButton.Click += new System.EventHandler(this.analyzeButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.copyButton);
            this.groupBox1.Controls.Add(this.searchButton);
            this.groupBox1.Controls.Add(this.exportOKResultsButton);
            this.groupBox1.Controls.Add(this.fileCountLabel);
            this.groupBox1.Controls.Add(this.fileList);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(549, 406);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Results";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(6, 21);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 0;
            this.searchButton.Text = "Search...";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // exportOKResultsButton
            // 
            this.exportOKResultsButton.Location = new System.Drawing.Point(277, 377);
            this.exportOKResultsButton.Name = "exportOKResultsButton";
            this.exportOKResultsButton.Size = new System.Drawing.Size(113, 23);
            this.exportOKResultsButton.TabIndex = 4;
            this.exportOKResultsButton.Text = "Generate KEY...";
            this.exportOKResultsButton.UseVisualStyleBackColor = true;
            this.exportOKResultsButton.Click += new System.EventHandler(this.exportOKResultsButton_Click);
            // 
            // fileCountLabel
            // 
            this.fileCountLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileCountLabel.ForeColor = System.Drawing.Color.Teal;
            this.fileCountLabel.Location = new System.Drawing.Point(87, 25);
            this.fileCountLabel.Name = "fileCountLabel";
            this.fileCountLabel.Size = new System.Drawing.Size(455, 14);
            this.fileCountLabel.TabIndex = 1;
            this.fileCountLabel.Text = "<> files, <> ids.";
            this.fileCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fileList
            // 
            this.fileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.numHeader,
            this.resultHeader,
            this.fileNameHeader,
            this.idHeader});
            this.fileList.FullRowSelect = true;
            this.fileList.GridLines = true;
            this.fileList.HideSelection = false;
            this.fileList.Location = new System.Drawing.Point(5, 50);
            this.fileList.MultiSelect = false;
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(537, 321);
            this.fileList.TabIndex = 2;
            this.fileList.UseCompatibleStateImageBehavior = false;
            this.fileList.View = System.Windows.Forms.View.Details;
            // 
            // numHeader
            // 
            this.numHeader.Text = "#";
            this.numHeader.Width = 50;
            // 
            // resultHeader
            // 
            this.resultHeader.Text = "Result";
            this.resultHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.resultHeader.Width = 45;
            // 
            // fileNameHeader
            // 
            this.fileNameHeader.Text = "File Name";
            this.fileNameHeader.Width = 297;
            // 
            // idHeader
            // 
            this.idHeader.Text = "MAP Identifier(s)";
            this.idHeader.Width = 250;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 512);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(574, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // copyButton
            // 
            this.copyButton.Location = new System.Drawing.Point(158, 377);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(113, 23);
            this.copyButton.TabIndex = 3;
            this.copyButton.Text = "Copy Ids";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // FileListingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 534);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.analyzeButton);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileListingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "KEY Generator";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tduPath;
        private System.Windows.Forms.Button analyzeButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label fileCountLabel;
        private System.Windows.Forms.ListView fileList;
        private System.Windows.Forms.ColumnHeader numHeader;
        private System.Windows.Forms.ColumnHeader fileNameHeader;
        private System.Windows.Forms.ColumnHeader idHeader;
        private System.Windows.Forms.ColumnHeader resultHeader;
        private System.Windows.Forms.Button exportOKResultsButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox keyPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button copyButton;
    }
}
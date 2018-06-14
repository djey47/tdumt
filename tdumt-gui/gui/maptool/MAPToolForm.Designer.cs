namespace TDUModdingTools.gui.maptool
{
    partial class MAPToolForm
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
            this.refreshButton = new System.Windows.Forms.Button();
            this.browseMAPButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fileNamesButton = new System.Windows.Forms.Button();
            this.browseKEYButton = new System.Windows.Forms.Button();
            this.inputKEYFilePath = new System.Windows.Forms.TextBox();
            this.inputFilePath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.entryList = new System.Windows.Forms.ListView();
            this.numHeader = new System.Windows.Forms.ColumnHeader();
            this.idHeader = new System.Windows.Forms.ColumnHeader();
            this.size1Header = new System.Windows.Forms.ColumnHeader();
            this.size2Header = new System.Windows.Forms.ColumnHeader();
            this.addressHeader = new System.Windows.Forms.ColumnHeader();
            this.fileHeader = new System.Windows.Forms.ColumnHeader();
            this.fileSizeHeader = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fixButton = new System.Windows.Forms.Button();
            this.modifyButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.entryCountLabel = new System.Windows.Forms.Label();
            this.fixAllButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // refreshButton
            // 
            this.refreshButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.Location = new System.Drawing.Point(558, 12);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(78, 34);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // browseMAPButton
            // 
            this.browseMAPButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseMAPButton.Location = new System.Drawing.Point(424, 15);
            this.browseMAPButton.Name = "browseMAPButton";
            this.browseMAPButton.Size = new System.Drawing.Size(27, 23);
            this.browseMAPButton.TabIndex = 1;
            this.browseMAPButton.Text = "...";
            this.browseMAPButton.UseVisualStyleBackColor = true;
            this.browseMAPButton.Click += new System.EventHandler(this.browseMAPButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.fileNamesButton);
            this.groupBox4.Controls.Add(this.browseKEYButton);
            this.groupBox4.Controls.Add(this.inputKEYFilePath);
            this.groupBox4.Controls.Add(this.inputFilePath);
            this.groupBox4.Controls.Add(this.browseMAPButton);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(12, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(540, 77);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Files";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "KEY (optional):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "MAP:";
            // 
            // fileNamesButton
            // 
            this.fileNamesButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileNamesButton.Location = new System.Drawing.Point(457, 43);
            this.fileNamesButton.Name = "fileNamesButton";
            this.fileNamesButton.Size = new System.Drawing.Size(75, 23);
            this.fileNamesButton.TabIndex = 2;
            this.fileNamesButton.Text = "Get KEY...";
            this.fileNamesButton.UseVisualStyleBackColor = true;
            this.fileNamesButton.Click += new System.EventHandler(this.fileNamesButton_Click);
            // 
            // browseKEYButton
            // 
            this.browseKEYButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseKEYButton.Location = new System.Drawing.Point(424, 43);
            this.browseKEYButton.Name = "browseKEYButton";
            this.browseKEYButton.Size = new System.Drawing.Size(27, 23);
            this.browseKEYButton.TabIndex = 1;
            this.browseKEYButton.Text = "...";
            this.browseKEYButton.UseVisualStyleBackColor = true;
            this.browseKEYButton.Click += new System.EventHandler(this.browseKEYButton_Click);
            // 
            // inputKEYFilePath
            // 
            this.inputKEYFilePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputKEYFilePath.Location = new System.Drawing.Point(102, 43);
            this.inputKEYFilePath.Name = "inputKEYFilePath";
            this.inputKEYFilePath.Size = new System.Drawing.Size(316, 22);
            this.inputKEYFilePath.TabIndex = 0;
            // 
            // inputFilePath
            // 
            this.inputFilePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputFilePath.Location = new System.Drawing.Point(102, 15);
            this.inputFilePath.Name = "inputFilePath";
            this.inputFilePath.Size = new System.Drawing.Size(316, 22);
            this.inputFilePath.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // entryList
            // 
            this.entryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.numHeader,
            this.idHeader,
            this.size1Header,
            this.size2Header,
            this.addressHeader,
            this.fileHeader,
            this.fileSizeHeader});
            this.entryList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryList.FullRowSelect = true;
            this.entryList.GridLines = true;
            this.entryList.HideSelection = false;
            this.entryList.Location = new System.Drawing.Point(6, 50);
            this.entryList.MultiSelect = false;
            this.entryList.Name = "entryList";
            this.entryList.Size = new System.Drawing.Size(612, 410);
            this.entryList.TabIndex = 4;
            this.entryList.UseCompatibleStateImageBehavior = false;
            this.entryList.View = System.Windows.Forms.View.Details;
            this.entryList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.entryList_MouseDoubleClick);
            // 
            // numHeader
            // 
            this.numHeader.Text = "#";
            this.numHeader.Width = 50;
            // 
            // idHeader
            // 
            this.idHeader.Text = "File Id";
            this.idHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.idHeader.Width = 85;
            // 
            // size1Header
            // 
            this.size1Header.Text = "Size 1 (bytes)";
            this.size1Header.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.size1Header.Width = 90;
            // 
            // size2Header
            // 
            this.size2Header.Text = "Size 2 (bytes)";
            this.size2Header.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.size2Header.Width = 90;
            // 
            // addressHeader
            // 
            this.addressHeader.Text = "@ddress";
            this.addressHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.addressHeader.Width = 0;
            // 
            // fileHeader
            // 
            this.fileHeader.Text = "File name (from KEY file)";
            this.fileHeader.Width = 400;
            // 
            // fileSizeHeader
            // 
            this.fileSizeHeader.Text = "Actual size (bytes)";
            this.fileSizeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.fileSizeHeader.Width = 120;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fixButton);
            this.groupBox1.Controls.Add(this.modifyButton);
            this.groupBox1.Controls.Add(this.searchButton);
            this.groupBox1.Controls.Add(this.entryCountLabel);
            this.groupBox1.Controls.Add(this.entryList);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(624, 466);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contents";
            // 
            // fixButton
            // 
            this.fixButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fixButton.Location = new System.Drawing.Point(183, 21);
            this.fixButton.Name = "fixButton";
            this.fixButton.Size = new System.Drawing.Size(75, 23);
            this.fixButton.TabIndex = 3;
            this.fixButton.Text = "Fix entry";
            this.fixButton.UseVisualStyleBackColor = true;
            this.fixButton.Click += new System.EventHandler(this.fixButton_Click);
            // 
            // modifyButton
            // 
            this.modifyButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifyButton.Location = new System.Drawing.Point(102, 21);
            this.modifyButton.Name = "modifyButton";
            this.modifyButton.Size = new System.Drawing.Size(75, 23);
            this.modifyButton.TabIndex = 2;
            this.modifyButton.Text = "Change...";
            this.modifyButton.UseVisualStyleBackColor = true;
            this.modifyButton.Click += new System.EventHandler(this.modifyButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.Location = new System.Drawing.Point(6, 21);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search...";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // entryCountLabel
            // 
            this.entryCountLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryCountLabel.ForeColor = System.Drawing.Color.Teal;
            this.entryCountLabel.Location = new System.Drawing.Point(425, 25);
            this.entryCountLabel.Name = "entryCountLabel";
            this.entryCountLabel.Size = new System.Drawing.Size(193, 14);
            this.entryCountLabel.TabIndex = 0;
            this.entryCountLabel.Text = "<> entries in file.";
            this.entryCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fixAllButton
            // 
            this.fixAllButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fixAllButton.Location = new System.Drawing.Point(558, 52);
            this.fixAllButton.Name = "fixAllButton";
            this.fixAllButton.Size = new System.Drawing.Size(78, 32);
            this.fixAllButton.TabIndex = 3;
            this.fixAllButton.Text = "Fix all";
            this.fixAllButton.UseVisualStyleBackColor = true;
            this.fixAllButton.Click += new System.EventHandler(this.fixAllButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.fixAllButton);
            this.panel1.Controls.Add(this.refreshButton);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.statusStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 591);
            this.panel1.TabIndex = 5;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 569);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(640, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(648, 599);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // MAPToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 599);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MAPToolForm";
            this.Text = "TDUMT - MAP Tool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MAPToolForm_FormClosed);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button browseMAPButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox inputFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListView entryList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader idHeader;
        private System.Windows.Forms.ColumnHeader size1Header;
        private System.Windows.Forms.ColumnHeader size2Header;
        private System.Windows.Forms.ColumnHeader numHeader;
        private System.Windows.Forms.ColumnHeader addressHeader;
        private System.Windows.Forms.ColumnHeader fileHeader;
        private System.Windows.Forms.Label entryCountLabel;
        private System.Windows.Forms.Button modifyButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button fileNamesButton;
        private System.Windows.Forms.TextBox inputKEYFilePath;
        private System.Windows.Forms.Button browseKEYButton;
        private System.Windows.Forms.ColumnHeader fileSizeHeader;
        private System.Windows.Forms.Button fixAllButton;
        private System.Windows.Forms.Button fixButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}
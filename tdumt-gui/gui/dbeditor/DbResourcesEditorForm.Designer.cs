namespace TDUModdingTools.gui.dbeditor
{
    partial class DbResourcesEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbResourcesEditorForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.settingsButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.catComboBox = new System.Windows.Forms.ComboBox();
            this.openRadioButton = new System.Windows.Forms.RadioButton();
            this.catRadioButton = new System.Windows.Forms.RadioButton();
            this.inputFilePath = new System.Windows.Forms.TextBox();
            this.browseDBButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.lineLabel = new System.Windows.Forms.Label();
            this.editOKButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.searchToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.changeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.insertToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.entryCountLabel = new System.Windows.Forms.Label();
            this.entryList = new System.Windows.Forms.ListView();
            this.numHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 599);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.settingsButton);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.refreshButton);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.statusStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 591);
            this.panel1.TabIndex = 0;
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsButton.Location = new System.Drawing.Point(696, 58);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(80, 35);
            this.settingsButton.TabIndex = 2;
            this.settingsButton.Text = "Settings...";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.saveButton);
            this.groupBox4.Controls.Add(this.catComboBox);
            this.groupBox4.Controls.Add(this.openRadioButton);
            this.groupBox4.Controls.Add(this.catRadioButton);
            this.groupBox4.Controls.Add(this.inputFilePath);
            this.groupBox4.Controls.Add(this.browseDBButton);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(8, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(682, 86);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Database resources";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(599, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "(auto-saved)";
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(491, 19);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(102, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save changes";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // catComboBox
            // 
            this.catComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.catComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.catComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catComboBox.FormattingEnabled = true;
            this.catComboBox.Location = new System.Drawing.Point(184, 20);
            this.catComboBox.Name = "catComboBox";
            this.catComboBox.Size = new System.Drawing.Size(301, 22);
            this.catComboBox.TabIndex = 2;
            // 
            // openRadioButton
            // 
            this.openRadioButton.AutoSize = true;
            this.openRadioButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openRadioButton.Location = new System.Drawing.Point(9, 54);
            this.openRadioButton.Name = "openRadioButton";
            this.openRadioButton.Size = new System.Drawing.Size(129, 18);
            this.openRadioButton.TabIndex = 4;
            this.openRadioButton.Text = "Open resource file:";
            this.openRadioButton.UseVisualStyleBackColor = true;
            this.openRadioButton.CheckedChanged += new System.EventHandler(this.openRadioButton_CheckedChanged);
            // 
            // catRadioButton
            // 
            this.catRadioButton.AutoSize = true;
            this.catRadioButton.Checked = true;
            this.catRadioButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catRadioButton.Location = new System.Drawing.Point(9, 21);
            this.catRadioButton.Name = "catRadioButton";
            this.catRadioButton.Size = new System.Drawing.Size(169, 18);
            this.catRadioButton.TabIndex = 1;
            this.catRadioButton.TabStop = true;
            this.catRadioButton.Text = "Topic in current database:";
            this.catRadioButton.UseVisualStyleBackColor = true;
            this.catRadioButton.CheckedChanged += new System.EventHandler(this.catRadioButton_CheckedChanged);
            // 
            // inputFilePath
            // 
            this.inputFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.inputFilePath.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputFilePath.Location = new System.Drawing.Point(184, 53);
            this.inputFilePath.Name = "inputFilePath";
            this.inputFilePath.Size = new System.Drawing.Size(376, 22);
            this.inputFilePath.TabIndex = 5;
            // 
            // browseDBButton
            // 
            this.browseDBButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseDBButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseDBButton.Location = new System.Drawing.Point(566, 52);
            this.browseDBButton.Name = "browseDBButton";
            this.browseDBButton.Size = new System.Drawing.Size(27, 23);
            this.browseDBButton.TabIndex = 6;
            this.browseDBButton.Text = "...";
            this.browseDBButton.UseVisualStyleBackColor = true;
            this.browseDBButton.Click += new System.EventHandler(this.browseDBButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.Location = new System.Drawing.Point(696, 14);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(80, 35);
            this.refreshButton.TabIndex = 1;
            this.refreshButton.Text = "Load";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.valueTextBox);
            this.groupBox2.Controls.Add(this.idTextBox);
            this.groupBox2.Controls.Add(this.lineLabel);
            this.groupBox2.Controls.Add(this.editOKButton);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 495);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(768, 71);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entry Editor";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(162, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(553, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Value";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Id";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // valueTextBox
            // 
            this.valueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.valueTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueTextBox.Location = new System.Drawing.Point(162, 25);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(553, 22);
            this.valueTextBox.TabIndex = 2;
            // 
            // idTextBox
            // 
            this.idTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idTextBox.Location = new System.Drawing.Point(65, 25);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(91, 22);
            this.idTextBox.TabIndex = 1;
            this.idTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lineLabel
            // 
            this.lineLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineLabel.ForeColor = System.Drawing.Color.SeaGreen;
            this.lineLabel.Location = new System.Drawing.Point(6, 24);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(53, 22);
            this.lineLabel.TabIndex = 0;
            this.lineLabel.Text = "#";
            this.lineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editOKButton
            // 
            this.editOKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editOKButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editOKButton.Location = new System.Drawing.Point(721, 24);
            this.editOKButton.Name = "editOKButton";
            this.editOKButton.Size = new System.Drawing.Size(41, 40);
            this.editOKButton.TabIndex = 3;
            this.editOKButton.Text = "OK";
            this.editOKButton.UseVisualStyleBackColor = true;
            this.editOKButton.Click += new System.EventHandler(this.editOKButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.entryCountLabel);
            this.groupBox1.Controls.Add(this.entryList);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 380);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contents";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripButton,
            this.toolStripSeparator1,
            this.changeToolStripButton,
            this.insertToolStripButton,
            this.deleteToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 352);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(762, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
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
            this.searchToolStripButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // changeToolStripButton
            // 
            this.changeToolStripButton.AutoToolTip = false;
            this.changeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.changeToolStripButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("changeToolStripButton.Image")));
            this.changeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.changeToolStripButton.Name = "changeToolStripButton";
            this.changeToolStripButton.Size = new System.Drawing.Size(56, 22);
            this.changeToolStripButton.Text = "Change";
            this.changeToolStripButton.ToolTipText = "Allows to update selected entry (double click)";
            this.changeToolStripButton.Click += new System.EventHandler(this.modifyButton_Click);
            // 
            // insertToolStripButton
            // 
            this.insertToolStripButton.AutoToolTip = false;
            this.insertToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.insertToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.insertToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("insertToolStripButton.Image")));
            this.insertToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.insertToolStripButton.Name = "insertToolStripButton";
            this.insertToolStripButton.Size = new System.Drawing.Size(43, 22);
            this.insertToolStripButton.Text = "Insert";
            this.insertToolStripButton.ToolTipText = "Adds a new entry at selected location (Ins)";
            this.insertToolStripButton.Click += new System.EventHandler(this.insertToolStripButton_Click);
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.AutoToolTip = false;
            this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripButton.Image")));
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(47, 22);
            this.deleteToolStripButton.Text = "Delete";
            this.deleteToolStripButton.ToolTipText = "Deletes selected entry (Del)";
            this.deleteToolStripButton.Click += new System.EventHandler(this.deleteToolStripButton_Click);
            // 
            // entryCountLabel
            // 
            this.entryCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.entryCountLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryCountLabel.ForeColor = System.Drawing.Color.Teal;
            this.entryCountLabel.Location = new System.Drawing.Point(6, 18);
            this.entryCountLabel.Name = "entryCountLabel";
            this.entryCountLabel.Size = new System.Drawing.Size(756, 19);
            this.entryCountLabel.TabIndex = 0;
            this.entryCountLabel.Text = "<>";
            this.entryCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // entryList
            // 
            this.entryList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.entryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.numHeader,
            this.idHeader,
            this.valueHeader});
            this.entryList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryList.FullRowSelect = true;
            this.entryList.GridLines = true;
            this.entryList.HideSelection = false;
            this.entryList.Location = new System.Drawing.Point(6, 40);
            this.entryList.MultiSelect = false;
            this.entryList.Name = "entryList";
            this.entryList.Size = new System.Drawing.Size(756, 311);
            this.entryList.TabIndex = 1;
            this.entryList.UseCompatibleStateImageBehavior = false;
            this.entryList.View = System.Windows.Forms.View.Details;
            this.entryList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.entryList_KeyDown);
            this.entryList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.entryList_MouseDoubleClick);
            // 
            // numHeader
            // 
            this.numHeader.Text = "#";
            // 
            // idHeader
            // 
            this.idHeader.Text = "Id";
            this.idHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.idHeader.Width = 90;
            // 
            // valueHeader
            // 
            this.valueHeader.Text = "Value";
            this.valueHeader.Width = 2000;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 569);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // DbResourcesEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 599);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DbResourcesEditorForm";
            this.Text = "TDUMT - DB Resources Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBEditorForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DBEditorForm_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label entryCountLabel;
        private System.Windows.Forms.ListView entryList;
        private System.Windows.Forms.ColumnHeader numHeader;
        private System.Windows.Forms.ColumnHeader idHeader;
        private System.Windows.Forms.ColumnHeader valueHeader;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox inputFilePath;
        private System.Windows.Forms.Button browseDBButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.RadioButton openRadioButton;
        private System.Windows.Forms.RadioButton catRadioButton;
        private System.Windows.Forms.ComboBox catComboBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label lineLabel;
        private System.Windows.Forms.Button editOKButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton searchToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton changeToolStripButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton insertToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
    }
}
namespace TDUModdingTools.gui.wizards.patcheditor
{
    partial class PatchEditorForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatchEditorForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.actionInstrToolStrip = new System.Windows.Forms.ToolStrip();
            this.newInstructionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteInstructionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.duplicateInstructionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripSeparator();
            this.moveUpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.moveDownToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.instructionListView = new System.Windows.Forms.ListView();
            this.numColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.actionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.failColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.commentColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.actionArgsToolStrip = new System.Windows.Forms.ToolStrip();
            this.saveArgsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.clearReportToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.argsTabPage = new System.Windows.Forms.TabPage();
            this.aboutInstructionLinkLabel = new System.Windows.Forms.LinkLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.commentTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.installGroupComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.enabledCheckbox = new System.Windows.Forms.CheckBox();
            this.failOnErrorCheckbox = new System.Windows.Forms.CheckBox();
            this.parametersListView = new System.Windows.Forms.ListView();
            this.paramNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.paramValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.paramHintHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reportTabPage = new System.Windows.Forms.TabPage();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.debugTabPage = new System.Windows.Forms.TabPage();
            this.debugTextBox = new System.Windows.Forms.TextBox();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mainStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.fileToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.newPatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uninstallerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.savePatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePatchAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.addNewInstructionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteInstructionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateInstructionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.moveInstructionUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveInstructionDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.deployInstallerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.openLastDeployLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.runPatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.patchNameLabel = new System.Windows.Forms.ToolStripLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.testingProbeTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.actionInstrToolStrip.SuspendLayout();
            this.actionArgsToolStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.argsTabPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.reportTabPage.SuspendLayout();
            this.debugTabPage.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.mainPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1008, 730);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.splitContainer);
            this.mainPanel.Controls.Add(this.mainStatusStrip);
            this.mainPanel.Controls.Add(this.mainToolStrip);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(4, 4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1000, 722);
            this.mainPanel.TabIndex = 0;
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 25);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer.Panel1.Controls.Add(this.actionInstrToolStrip);
            this.splitContainer.Panel1.Controls.Add(this.instructionListView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer.Panel2.Controls.Add(this.actionArgsToolStrip);
            this.splitContainer.Panel2.Controls.Add(this.tabControl);
            this.splitContainer.Size = new System.Drawing.Size(1000, 675);
            this.splitContainer.SplitterDistance = 325;
            this.splitContainer.TabIndex = 1;
            // 
            // actionInstrToolStrip
            // 
            this.actionInstrToolStrip.AutoSize = false;
            this.actionInstrToolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.actionInstrToolStrip.GripMargin = new System.Windows.Forms.Padding(2, 4, 2, 10);
            this.actionInstrToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.actionInstrToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newInstructionToolStripButton,
            this.deleteInstructionToolStripButton,
            this.duplicateInstructionToolStripButton,
            this.toolStripButton6,
            this.moveUpToolStripButton,
            this.moveDownToolStripButton});
            this.actionInstrToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.actionInstrToolStrip.Location = new System.Drawing.Point(0, 0);
            this.actionInstrToolStrip.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.actionInstrToolStrip.Name = "actionInstrToolStrip";
            this.actionInstrToolStrip.Size = new System.Drawing.Size(28, 321);
            this.actionInstrToolStrip.TabIndex = 0;
            // 
            // newInstructionToolStripButton
            // 
            this.newInstructionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newInstructionToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newInstructionToolStripButton.Image")));
            this.newInstructionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newInstructionToolStripButton.Name = "newInstructionToolStripButton";
            this.newInstructionToolStripButton.Size = new System.Drawing.Size(26, 20);
            this.newInstructionToolStripButton.ToolTipText = "Adds an instruction";
            this.newInstructionToolStripButton.Click += new System.EventHandler(this.newInstructionToolStripButton_Click);
            // 
            // deleteInstructionToolStripButton
            // 
            this.deleteInstructionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteInstructionToolStripButton.Image = global::TDUModdingTools.Properties.Resources.trash_16;
            this.deleteInstructionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteInstructionToolStripButton.Name = "deleteInstructionToolStripButton";
            this.deleteInstructionToolStripButton.Size = new System.Drawing.Size(26, 20);
            this.deleteInstructionToolStripButton.Text = "toolStripButton5";
            this.deleteInstructionToolStripButton.ToolTipText = "Deletes selected instruction";
            this.deleteInstructionToolStripButton.Click += new System.EventHandler(this.deleteInstructionToolStripButton_Click);
            // 
            // duplicateInstructionToolStripButton
            // 
            this.duplicateInstructionToolStripButton.AutoToolTip = false;
            this.duplicateInstructionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.duplicateInstructionToolStripButton.Image = global::TDUModdingTools.Properties.Resources.copy_16;
            this.duplicateInstructionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.duplicateInstructionToolStripButton.Name = "duplicateInstructionToolStripButton";
            this.duplicateInstructionToolStripButton.Size = new System.Drawing.Size(26, 20);
            this.duplicateInstructionToolStripButton.ToolTipText = "Duplicates selected instruction";
            this.duplicateInstructionToolStripButton.Click += new System.EventHandler(this.duplicateInstructionToolStripButton_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(26, 6);
            // 
            // moveUpToolStripButton
            // 
            this.moveUpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveUpToolStripButton.Image = global::TDUModdingTools.Properties.Resources.arrow_up_16;
            this.moveUpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveUpToolStripButton.Name = "moveUpToolStripButton";
            this.moveUpToolStripButton.Size = new System.Drawing.Size(26, 20);
            this.moveUpToolStripButton.ToolTipText = "Moves selected instruction up";
            this.moveUpToolStripButton.Click += new System.EventHandler(this.moveUpToolStripButton_Click);
            // 
            // moveDownToolStripButton
            // 
            this.moveDownToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveDownToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("moveDownToolStripButton.Image")));
            this.moveDownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveDownToolStripButton.Name = "moveDownToolStripButton";
            this.moveDownToolStripButton.Size = new System.Drawing.Size(26, 20);
            this.moveDownToolStripButton.ToolTipText = "Moves selected instruction down";
            this.moveDownToolStripButton.Click += new System.EventHandler(this.moveDownToolStripButton_Click);
            // 
            // instructionListView
            // 
            this.instructionListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.instructionListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.instructionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.numColumnHeader,
            this.actionColumnHeader,
            this.failColumnHeader,
            this.groupColumnHeader,
            this.commentColumnHeader});
            this.instructionListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionListView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.instructionListView.FullRowSelect = true;
            this.instructionListView.GridLines = true;
            this.instructionListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.instructionListView.HideSelection = false;
            this.instructionListView.Location = new System.Drawing.Point(25, -2);
            this.instructionListView.Name = "instructionListView";
            this.instructionListView.Size = new System.Drawing.Size(968, 321);
            this.instructionListView.TabIndex = 1;
            this.instructionListView.UseCompatibleStateImageBehavior = false;
            this.instructionListView.View = System.Windows.Forms.View.Details;
            this.instructionListView.SelectedIndexChanged += new System.EventHandler(this.instructionListView_SelectedIndexChanged);
            // 
            // numColumnHeader
            // 
            this.numColumnHeader.Text = "#";
            this.numColumnHeader.Width = 45;
            // 
            // actionColumnHeader
            // 
            this.actionColumnHeader.Text = "Action";
            this.actionColumnHeader.Width = 300;
            // 
            // failColumnHeader
            // 
            this.failColumnHeader.Text = "Fail on error?";
            this.failColumnHeader.Width = 85;
            // 
            // groupColumnHeader
            // 
            this.groupColumnHeader.Text = "Group";
            this.groupColumnHeader.Width = 175;
            // 
            // commentColumnHeader
            // 
            this.commentColumnHeader.Text = "Comment";
            this.commentColumnHeader.Width = 500;
            // 
            // actionArgsToolStrip
            // 
            this.actionArgsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.actionArgsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveArgsToolStripButton,
            this.toolStripSeparator5,
            this.clearReportToolStripButton});
            this.actionArgsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.actionArgsToolStrip.Name = "actionArgsToolStrip";
            this.actionArgsToolStrip.Size = new System.Drawing.Size(996, 25);
            this.actionArgsToolStrip.TabIndex = 1;
            // 
            // saveArgsToolStripButton
            // 
            this.saveArgsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveArgsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveArgsToolStripButton.Image")));
            this.saveArgsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveArgsToolStripButton.Name = "saveArgsToolStripButton";
            this.saveArgsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveArgsToolStripButton.ToolTipText = "Saves current instruction";
            this.saveArgsToolStripButton.Click += new System.EventHandler(this.saveArgsToolStripButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // clearReportToolStripButton
            // 
            this.clearReportToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearReportToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("clearReportToolStripButton.Image")));
            this.clearReportToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearReportToolStripButton.Name = "clearReportToolStripButton";
            this.clearReportToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.clearReportToolStripButton.ToolTipText = "Clears displayed report";
            this.clearReportToolStripButton.Click += new System.EventHandler(this.clearReportToolStripButton_Click);
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.argsTabPage);
            this.tabControl.Controls.Add(this.reportTabPage);
            this.tabControl.Controls.Add(this.debugTabPage);
            this.tabControl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 25);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(996, 319);
            this.tabControl.TabIndex = 0;
            // 
            // argsTabPage
            // 
            this.argsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.argsTabPage.Controls.Add(this.aboutInstructionLinkLabel);
            this.argsTabPage.Controls.Add(this.groupBox3);
            this.argsTabPage.Controls.Add(this.groupBox1);
            this.argsTabPage.Controls.Add(this.parametersListView);
            this.argsTabPage.Controls.Add(this.typesComboBox);
            this.argsTabPage.Controls.Add(this.label1);
            this.argsTabPage.Location = new System.Drawing.Point(25, 4);
            this.argsTabPage.Name = "argsTabPage";
            this.argsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.argsTabPage.Size = new System.Drawing.Size(967, 311);
            this.argsTabPage.TabIndex = 0;
            this.argsTabPage.Text = "Properties";
            // 
            // aboutInstructionLinkLabel
            // 
            this.aboutInstructionLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutInstructionLinkLabel.AutoSize = true;
            this.aboutInstructionLinkLabel.LinkColor = System.Drawing.SystemColors.Highlight;
            this.aboutInstructionLinkLabel.Location = new System.Drawing.Point(455, 9);
            this.aboutInstructionLinkLabel.Name = "aboutInstructionLinkLabel";
            this.aboutInstructionLinkLabel.Size = new System.Drawing.Size(127, 14);
            this.aboutInstructionLinkLabel.TabIndex = 2;
            this.aboutInstructionLinkLabel.TabStop = true;
            this.aboutInstructionLinkLabel.Text = "Help ! What\'s this ?";
            this.aboutInstructionLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.aboutInstructionLinkLabel_LinkClicked);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.commentTextBox);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(748, 97);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(213, 208);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Comment";
            // 
            // commentTextBox
            // 
            this.commentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.commentTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.commentTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentTextBox.Location = new System.Drawing.Point(6, 21);
            this.commentTextBox.Multiline = true;
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commentTextBox.Size = new System.Drawing.Size(201, 181);
            this.commentTextBox.TabIndex = 0;
            this.commentTextBox.TextChanged += new System.EventHandler(this.commentTextBox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.installGroupComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.enabledCheckbox);
            this.groupBox1.Controls.Add(this.failOnErrorCheckbox);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(748, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 85);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Behaviour";
            // 
            // installGroupComboBox
            // 
            this.installGroupComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installGroupComboBox.FormattingEnabled = true;
            this.installGroupComboBox.Location = new System.Drawing.Point(56, 19);
            this.installGroupComboBox.Name = "installGroupComboBox";
            this.installGroupComboBox.Size = new System.Drawing.Size(151, 22);
            this.installGroupComboBox.TabIndex = 1;
            this.installGroupComboBox.SelectedIndexChanged += new System.EventHandler(this.installGroupComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Group:";
            // 
            // enabledCheckbox
            // 
            this.enabledCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enabledCheckbox.AutoSize = true;
            this.enabledCheckbox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enabledCheckbox.Location = new System.Drawing.Point(18, 57);
            this.enabledCheckbox.Name = "enabledCheckbox";
            this.enabledCheckbox.Size = new System.Drawing.Size(69, 18);
            this.enabledCheckbox.TabIndex = 2;
            this.enabledCheckbox.Text = "Enabled";
            this.enabledCheckbox.UseVisualStyleBackColor = true;
            this.enabledCheckbox.CheckedChanged += new System.EventHandler(this.enabledCheckbox_CheckedChanged);
            // 
            // failOnErrorCheckbox
            // 
            this.failOnErrorCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.failOnErrorCheckbox.AutoSize = true;
            this.failOnErrorCheckbox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failOnErrorCheckbox.Location = new System.Drawing.Point(103, 57);
            this.failOnErrorCheckbox.Name = "failOnErrorCheckbox";
            this.failOnErrorCheckbox.Size = new System.Drawing.Size(95, 18);
            this.failOnErrorCheckbox.TabIndex = 3;
            this.failOnErrorCheckbox.Text = "Halt on error";
            this.failOnErrorCheckbox.UseVisualStyleBackColor = true;
            this.failOnErrorCheckbox.CheckedChanged += new System.EventHandler(this.failOnErrorCheckbox_CheckedChanged);
            // 
            // parametersListView
            // 
            this.parametersListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.parametersListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.parametersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.paramNameHeader,
            this.paramValueHeader,
            this.paramHintHeader});
            this.parametersListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parametersListView.FullRowSelect = true;
            this.parametersListView.GridLines = true;
            this.parametersListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.parametersListView.LabelWrap = false;
            this.parametersListView.Location = new System.Drawing.Point(9, 34);
            this.parametersListView.MultiSelect = false;
            this.parametersListView.Name = "parametersListView";
            this.parametersListView.Size = new System.Drawing.Size(733, 271);
            this.parametersListView.TabIndex = 3;
            this.parametersListView.UseCompatibleStateImageBehavior = false;
            this.parametersListView.View = System.Windows.Forms.View.Details;
            this.parametersListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.parametersListView_MouseDoubleClick);
            // 
            // paramNameHeader
            // 
            this.paramNameHeader.Text = "Parameter";
            this.paramNameHeader.Width = 125;
            // 
            // paramValueHeader
            // 
            this.paramValueHeader.Text = "Value";
            this.paramValueHeader.Width = 266;
            // 
            // paramHintHeader
            // 
            this.paramHintHeader.Text = "?";
            this.paramHintHeader.Width = 550;
            // 
            // typesComboBox
            // 
            this.typesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.typesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typesComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typesComboBox.FormattingEnabled = true;
            this.typesComboBox.Location = new System.Drawing.Point(124, 6);
            this.typesComboBox.Name = "typesComboBox";
            this.typesComboBox.Size = new System.Drawing.Size(325, 22);
            this.typesComboBox.TabIndex = 1;
            this.typesComboBox.SelectedIndexChanged += new System.EventHandler(this.typesComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Instruction type:";
            // 
            // reportTabPage
            // 
            this.reportTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.reportTabPage.Controls.Add(this.logTextBox);
            this.reportTabPage.Location = new System.Drawing.Point(25, 4);
            this.reportTabPage.Name = "reportTabPage";
            this.reportTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.reportTabPage.Size = new System.Drawing.Size(967, 311);
            this.reportTabPage.TabIndex = 1;
            this.reportTabPage.Text = "Report";
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.logTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logTextBox.Location = new System.Drawing.Point(3, 3);
            this.logTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextBox.Size = new System.Drawing.Size(961, 305);
            this.logTextBox.TabIndex = 0;
            // 
            // debugTabPage
            // 
            this.debugTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.debugTabPage.Controls.Add(this.debugTextBox);
            this.debugTabPage.Location = new System.Drawing.Point(25, 4);
            this.debugTabPage.Name = "debugTabPage";
            this.debugTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.debugTabPage.Size = new System.Drawing.Size(967, 311);
            this.debugTabPage.TabIndex = 2;
            this.debugTabPage.Text = "Debug";
            // 
            // debugTextBox
            // 
            this.debugTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.debugTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.debugTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugTextBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.debugTextBox.HideSelection = false;
            this.debugTextBox.Location = new System.Drawing.Point(3, 3);
            this.debugTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.debugTextBox.Multiline = true;
            this.debugTextBox.Name = "debugTextBox";
            this.debugTextBox.ReadOnly = true;
            this.debugTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.debugTextBox.Size = new System.Drawing.Size(961, 305);
            this.debugTextBox.TabIndex = 1;
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainStatusLabel,
            this.mainProgressBar});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 700);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(1000, 22);
            this.mainStatusStrip.TabIndex = 2;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // mainStatusLabel
            // 
            this.mainStatusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainStatusLabel.Name = "mainStatusLabel";
            this.mainStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(250, 16);
            this.mainProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripDropDownButton,
            this.EditToolStripDropDownButton,
            this.generateToolStripDropDownButton,
            this.testToolStripDropDownButton,
            this.toolsToolStripDropDownButton,
            this.helpToolStripButton,
            this.toolStripSeparator4,
            this.patchNameLabel});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(1000, 25);
            this.mainToolStrip.TabIndex = 0;
            // 
            // fileToolStripDropDownButton
            // 
            this.fileToolStripDropDownButton.AutoToolTip = false;
            this.fileToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPatchToolStripMenuItem,
            this.openPatchToolStripMenuItem,
            this.toolStripMenuItem1,
            this.savePatchToolStripMenuItem,
            this.savePatchAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.propertiesToolStripMenuItem,
            this.toolStripMenuItem2,
            this.closeToolStripMenuItem});
            this.fileToolStripDropDownButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripDropDownButton.Image")));
            this.fileToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileToolStripDropDownButton.Name = "fileToolStripDropDownButton";
            this.fileToolStripDropDownButton.Size = new System.Drawing.Size(37, 22);
            this.fileToolStripDropDownButton.Text = "File";
            // 
            // newPatchToolStripMenuItem
            // 
            this.newPatchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.installerToolStripMenuItem,
            this.uninstallerToolStripMenuItem});
            this.newPatchToolStripMenuItem.Name = "newPatchToolStripMenuItem";
            this.newPatchToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.newPatchToolStripMenuItem.Text = "New patch";
            // 
            // installerToolStripMenuItem
            // 
            this.installerToolStripMenuItem.Name = "installerToolStripMenuItem";
            this.installerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.installerToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.installerToolStripMenuItem.Text = "Installer...";
            this.installerToolStripMenuItem.Click += new System.EventHandler(this.installerToolStripMenuItem_Click);
            // 
            // uninstallerToolStripMenuItem
            // 
            this.uninstallerToolStripMenuItem.Name = "uninstallerToolStripMenuItem";
            this.uninstallerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.N)));
            this.uninstallerToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.uninstallerToolStripMenuItem.Text = "Uninstaller...";
            this.uninstallerToolStripMenuItem.Click += new System.EventHandler(this.uninstallerToolStripMenuItem_Click);
            // 
            // openPatchToolStripMenuItem
            // 
            this.openPatchToolStripMenuItem.Name = "openPatchToolStripMenuItem";
            this.openPatchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openPatchToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.openPatchToolStripMenuItem.Text = "Open patch...";
            this.openPatchToolStripMenuItem.Click += new System.EventHandler(this.openPatchToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 6);
            // 
            // savePatchToolStripMenuItem
            // 
            this.savePatchToolStripMenuItem.Name = "savePatchToolStripMenuItem";
            this.savePatchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.savePatchToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.savePatchToolStripMenuItem.Text = "Save patch";
            this.savePatchToolStripMenuItem.Click += new System.EventHandler(this.savePatchToolStripMenuItem_Click);
            // 
            // savePatchAsToolStripMenuItem
            // 
            this.savePatchAsToolStripMenuItem.Name = "savePatchAsToolStripMenuItem";
            this.savePatchAsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.savePatchAsToolStripMenuItem.Text = "Save patch as...";
            this.savePatchAsToolStripMenuItem.Click += new System.EventHandler(this.savePatchAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.propertiesToolStripMenuItem.Text = "Properties...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(190, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // EditToolStripDropDownButton
            // 
            this.EditToolStripDropDownButton.AutoToolTip = false;
            this.EditToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EditToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewInstructionToolStripMenuItem,
            this.deleteInstructionToolStripMenuItem,
            this.duplicateInstructionToolStripMenuItem,
            this.toolStripSeparator3,
            this.moveInstructionUpToolStripMenuItem,
            this.moveInstructionDownToolStripMenuItem,
            this.toolStripSeparator7,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.EditToolStripDropDownButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("EditToolStripDropDownButton.Image")));
            this.EditToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditToolStripDropDownButton.Name = "EditToolStripDropDownButton";
            this.EditToolStripDropDownButton.Size = new System.Drawing.Size(41, 22);
            this.EditToolStripDropDownButton.Text = "Edit";
            // 
            // addNewInstructionToolStripMenuItem
            // 
            this.addNewInstructionToolStripMenuItem.Name = "addNewInstructionToolStripMenuItem";
            this.addNewInstructionToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.addNewInstructionToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.addNewInstructionToolStripMenuItem.Text = "Add new instruction";
            this.addNewInstructionToolStripMenuItem.Click += new System.EventHandler(this.addNewInstructionToolStripMenuItem_Click);
            // 
            // deleteInstructionToolStripMenuItem
            // 
            this.deleteInstructionToolStripMenuItem.Name = "deleteInstructionToolStripMenuItem";
            this.deleteInstructionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.deleteInstructionToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.deleteInstructionToolStripMenuItem.Text = "Delete instruction";
            this.deleteInstructionToolStripMenuItem.Click += new System.EventHandler(this.deleteInstructionToolStripMenuItem_Click);
            // 
            // duplicateInstructionToolStripMenuItem
            // 
            this.duplicateInstructionToolStripMenuItem.Name = "duplicateInstructionToolStripMenuItem";
            this.duplicateInstructionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.duplicateInstructionToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.duplicateInstructionToolStripMenuItem.Text = "Duplicate instruction";
            this.duplicateInstructionToolStripMenuItem.Click += new System.EventHandler(this.duplicateInstructionToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(261, 6);
            // 
            // moveInstructionUpToolStripMenuItem
            // 
            this.moveInstructionUpToolStripMenuItem.Name = "moveInstructionUpToolStripMenuItem";
            this.moveInstructionUpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.moveInstructionUpToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.moveInstructionUpToolStripMenuItem.Text = "Move instruction up";
            this.moveInstructionUpToolStripMenuItem.Click += new System.EventHandler(this.moveInstructionUpToolStripMenuItem_Click);
            // 
            // moveInstructionDownToolStripMenuItem
            // 
            this.moveInstructionDownToolStripMenuItem.Name = "moveInstructionDownToolStripMenuItem";
            this.moveInstructionDownToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.moveInstructionDownToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.moveInstructionDownToolStripMenuItem.Text = "Move instruction down";
            this.moveInstructionDownToolStripMenuItem.Click += new System.EventHandler(this.moveInstructionDownToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(261, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.importToolStripMenuItem.Text = "Import instruction(s)...";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.exportToolStripMenuItem.Text = "Export instruction(s)...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // generateToolStripDropDownButton
            // 
            this.generateToolStripDropDownButton.AutoToolTip = false;
            this.generateToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.generateToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deployInstallerToolStripMenuItem,
            this.toolStripMenuItem4,
            this.toolStripSeparator6,
            this.openLastDeployLocationToolStripMenuItem});
            this.generateToolStripDropDownButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generateToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("generateToolStripDropDownButton.Image")));
            this.generateToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.generateToolStripDropDownButton.Name = "generateToolStripDropDownButton";
            this.generateToolStripDropDownButton.Size = new System.Drawing.Size(71, 22);
            this.generateToolStripDropDownButton.Text = "Generate";
            // 
            // deployInstallerToolStripMenuItem
            // 
            this.deployInstallerToolStripMenuItem.Name = "deployInstallerToolStripMenuItem";
            this.deployInstallerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.deployInstallerToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.deployInstallerToolStripMenuItem.Text = "Installer...";
            this.deployInstallerToolStripMenuItem.Click += new System.EventHandler(this.installerToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F12)));
            this.toolStripMenuItem4.Size = new System.Drawing.Size(212, 22);
            this.toolStripMenuItem4.Text = "Uninstaller...";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.uninstallerToolStripMenuItem1_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(209, 6);
            // 
            // openLastDeployLocationToolStripMenuItem
            // 
            this.openLastDeployLocationToolStripMenuItem.Name = "openLastDeployLocationToolStripMenuItem";
            this.openLastDeployLocationToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.openLastDeployLocationToolStripMenuItem.Text = "Open last deploy location";
            this.openLastDeployLocationToolStripMenuItem.Click += new System.EventHandler(this.openLastDeployLocationToolStripMenuItem_Click);
            // 
            // testToolStripDropDownButton
            // 
            this.testToolStripDropDownButton.AutoToolTip = false;
            this.testToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.testToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runPatchToolStripMenuItem,
            this.toolStripSeparator2,
            this.clearReportToolStripMenuItem});
            this.testToolStripDropDownButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("testToolStripDropDownButton.Image")));
            this.testToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.testToolStripDropDownButton.Name = "testToolStripDropDownButton";
            this.testToolStripDropDownButton.Size = new System.Drawing.Size(45, 22);
            this.testToolStripDropDownButton.Text = "Test";
            // 
            // runPatchToolStripMenuItem
            // 
            this.runPatchToolStripMenuItem.Name = "runPatchToolStripMenuItem";
            this.runPatchToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.runPatchToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.runPatchToolStripMenuItem.Text = "Run patch";
            this.runPatchToolStripMenuItem.Click += new System.EventHandler(this.runPatchToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(179, 6);
            // 
            // clearReportToolStripMenuItem
            // 
            this.clearReportToolStripMenuItem.Name = "clearReportToolStripMenuItem";
            this.clearReportToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.clearReportToolStripMenuItem.Text = "Clear current report";
            this.clearReportToolStripMenuItem.Click += new System.EventHandler(this.clearReportToolStripMenuItem_Click);
            // 
            // toolsToolStripDropDownButton
            // 
            this.toolsToolStripDropDownButton.AutoToolTip = false;
            this.toolsToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolsToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.toolsToolStripDropDownButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolsToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("toolsToolStripDropDownButton.Image")));
            this.toolsToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsToolStripDropDownButton.Name = "toolsToolStripDropDownButton";
            this.toolsToolStripDropDownButton.Size = new System.Drawing.Size(49, 22);
            this.toolsToolStripDropDownButton.Text = "Tools";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.AutoToolTip = false;
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.helpToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(35, 22);
            this.helpToolStripButton.Text = "Help";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // patchNameLabel
            // 
            this.patchNameLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patchNameLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.patchNameLabel.LinkColor = System.Drawing.SystemColors.Highlight;
            this.patchNameLabel.Name = "patchNameLabel";
            this.patchNameLabel.Size = new System.Drawing.Size(100, 22);
            this.patchNameLabel.Text = "No patch loaded.";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // testingProbeTimer
            // 
            this.testingProbeTimer.Tick += new System.EventHandler(this.testingProbeTimer_Tick);
            // 
            // PatchEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PatchEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TDUMT - Patch Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatchEditorForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.actionInstrToolStrip.ResumeLayout(false);
            this.actionInstrToolStrip.PerformLayout();
            this.actionArgsToolStrip.ResumeLayout(false);
            this.actionArgsToolStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.argsTabPage.ResumeLayout(false);
            this.argsTabPage.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.reportTabPage.ResumeLayout(false);
            this.reportTabPage.PerformLayout();
            this.debugTabPage.ResumeLayout(false);
            this.debugTabPage.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton fileToolStripDropDownButton;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem newPatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem savePatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePatchAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolsToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton testToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem runPatchToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripLabel patchNameLabel;
        private System.Windows.Forms.ToolStripStatusLabel mainStatusLabel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem clearReportToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage argsTabPage;
        private System.Windows.Forms.TabPage reportTabPage;
        private System.Windows.Forms.ListView instructionListView;
        private System.Windows.Forms.ToolStripDropDownButton EditToolStripDropDownButton;
        private System.Windows.Forms.ColumnHeader actionColumnHeader;
        private System.Windows.Forms.ColumnHeader numColumnHeader;
        private System.Windows.Forms.ToolStrip actionInstrToolStrip;
        private System.Windows.Forms.ColumnHeader failColumnHeader;
        private System.Windows.Forms.ColumnHeader groupColumnHeader;
        private System.Windows.Forms.ToolStripButton newInstructionToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteInstructionToolStripButton;
        private System.Windows.Forms.ToolStripButton moveUpToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripButton6;
        private System.Windows.Forms.ToolStripButton moveDownToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem addNewInstructionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteInstructionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem moveInstructionUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveInstructionDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStrip actionArgsToolStrip;
        private System.Windows.Forms.ToolStripButton saveArgsToolStripButton;
        private System.Windows.Forms.ListView parametersListView;
        private System.Windows.Forms.ComboBox typesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox enabledCheckbox;
        private System.Windows.Forms.CheckBox failOnErrorCheckbox;
        private System.Windows.Forms.ColumnHeader paramNameHeader;
        private System.Windows.Forms.ColumnHeader paramValueHeader;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton clearReportToolStripButton;
        private System.Windows.Forms.ColumnHeader paramHintHeader;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolStripProgressBar mainProgressBar;
        private System.Windows.Forms.ToolStripMenuItem duplicateInstructionToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripButton duplicateInstructionToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem installerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uninstallerToolStripMenuItem;
        private System.Windows.Forms.LinkLabel aboutInstructionLinkLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox commentTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader commentColumnHeader;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ComboBox installGroupComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripDropDownButton generateToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem deployInstallerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.TabPage debugTabPage;
        private System.Windows.Forms.TextBox debugTextBox;
        private System.Windows.Forms.Timer testingProbeTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem openLastDeployLocationToolStripMenuItem;
    }
}
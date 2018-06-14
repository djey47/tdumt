namespace TDUModdingTools.gui.settings
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rootBrowseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rootTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.mainTabPage = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.clearRadialButton = new System.Windows.Forms.Button();
            this.fixRegistryButton = new System.Windows.Forms.Button();
            this.debugModeCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.languageList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.moduleList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.browserTabPage = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.editExtractBackupFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.editExtractShowFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.editNewFilesFolderHardRadioButton = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.editNewFilesFolderCurrentRadioButton = new System.Windows.Forms.RadioButton();
            this.editFolderNewFilesBrowseButton = new System.Windows.Forms.Button();
            this.editFolderNewFilesTextBox = new System.Windows.Forms.TextBox();
            this.launcherTabPage = new System.Windows.Forms.TabPage();
            this.configGroupBox = new System.Windows.Forms.GroupBox();
            this.posRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.runAfterBrowseButton = new System.Windows.Forms.Button();
            this.runAfterTextBox = new System.Windows.Forms.TextBox();
            this.runBeforeBrowseButton = new System.Windows.Forms.Button();
            this.runBeforeTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.standardRadioButton = new System.Windows.Forms.RadioButton();
            this.radialCheckBox = new System.Windows.Forms.CheckBox();
            this.fpsRadioButton = new System.Windows.Forms.RadioButton();
            this.windowedRadioButton = new System.Windows.Forms.RadioButton();
            this.removeConfigButton = new System.Windows.Forms.Button();
            this.saveConfigButton = new System.Windows.Forms.Button();
            this.configComboBox = new System.Windows.Forms.ComboBox();
            this.patchEditorTabPage = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.reportClearingCheckBox = new System.Windows.Forms.CheckBox();
            this.reportAutoScrollCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.trackPackTabPage = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.profilesComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.mainTabPage.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.browserTabPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.launcherTabPage.SuspendLayout();
            this.configGroupBox.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.patchEditorTabPage.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.trackPackTabPage.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rootBrowseButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rootTextBox);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Folders";
            // 
            // rootBrowseButton
            // 
            this.rootBrowseButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rootBrowseButton.Location = new System.Drawing.Point(317, 20);
            this.rootBrowseButton.Name = "rootBrowseButton";
            this.rootBrowseButton.Size = new System.Drawing.Size(27, 23);
            this.rootBrowseButton.TabIndex = 2;
            this.rootBrowseButton.Text = "...";
            this.rootBrowseButton.UseVisualStyleBackColor = true;
            this.rootBrowseButton.Click += new System.EventHandler(this.rootBrowseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "TDU root:";
            // 
            // rootTextBox
            // 
            this.rootTextBox.Location = new System.Drawing.Point(74, 21);
            this.rootTextBox.Name = "rootTextBox";
            this.rootTextBox.Size = new System.Drawing.Size(237, 22);
            this.rootTextBox.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(221, 311);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(307, 311);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.mainTabPage);
            this.tabControl.Controls.Add(this.browserTabPage);
            this.tabControl.Controls.Add(this.launcherTabPage);
            this.tabControl.Controls.Add(this.patchEditorTabPage);
            this.tabControl.Controls.Add(this.trackPackTabPage);
            this.tabControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(370, 293);
            this.tabControl.TabIndex = 0;
            // 
            // mainTabPage
            // 
            this.mainTabPage.Controls.Add(this.groupBox6);
            this.mainTabPage.Controls.Add(this.groupBox4);
            this.mainTabPage.Controls.Add(this.groupBox3);
            this.mainTabPage.Controls.Add(this.groupBox1);
            this.mainTabPage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabPage.Location = new System.Drawing.Point(4, 22);
            this.mainTabPage.Name = "mainTabPage";
            this.mainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mainTabPage.Size = new System.Drawing.Size(362, 267);
            this.mainTabPage.TabIndex = 0;
            this.mainTabPage.Text = "Main";
            this.mainTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.clearRadialButton);
            this.groupBox6.Controls.Add(this.fixRegistryButton);
            this.groupBox6.Controls.Add(this.debugModeCheckBox);
            this.groupBox6.Location = new System.Drawing.Point(6, 180);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(350, 81);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Advanced";
            // 
            // clearRadialButton
            // 
            this.clearRadialButton.Location = new System.Drawing.Point(101, 45);
            this.clearRadialButton.Name = "clearRadialButton";
            this.clearRadialButton.Size = new System.Drawing.Size(89, 30);
            this.clearRadialButton.TabIndex = 2;
            this.clearRadialButton.Text = "Clear radial";
            this.clearRadialButton.UseVisualStyleBackColor = true;
            this.clearRadialButton.Click += new System.EventHandler(this.clearRadialButton_Click);
            // 
            // fixRegistryButton
            // 
            this.fixRegistryButton.Image = global::TDUModdingTools.Properties.Resources.shield_16x16;
            this.fixRegistryButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fixRegistryButton.Location = new System.Drawing.Point(6, 45);
            this.fixRegistryButton.Name = "fixRegistryButton";
            this.fixRegistryButton.Size = new System.Drawing.Size(89, 30);
            this.fixRegistryButton.TabIndex = 1;
            this.fixRegistryButton.Text = "Fix registry";
            this.fixRegistryButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.fixRegistryButton.UseVisualStyleBackColor = true;
            this.fixRegistryButton.Click += new System.EventHandler(this.fixRegistryButton_Click);
            // 
            // debugModeCheckBox
            // 
            this.debugModeCheckBox.AutoSize = true;
            this.debugModeCheckBox.Location = new System.Drawing.Point(9, 21);
            this.debugModeCheckBox.Name = "debugModeCheckBox";
            this.debugModeCheckBox.Size = new System.Drawing.Size(227, 18);
            this.debugModeCheckBox.TabIndex = 0;
            this.debugModeCheckBox.Text = "Debug mode (slower, needs restart)";
            this.debugModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.languageList);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(6, 122);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(350, 52);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Database";
            // 
            // languageList
            // 
            this.languageList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageList.FormattingEnabled = true;
            this.languageList.Location = new System.Drawing.Point(74, 21);
            this.languageList.Name = "languageList";
            this.languageList.Size = new System.Drawing.Size(270, 22);
            this.languageList.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 14);
            this.label5.TabIndex = 1;
            this.label5.Text = "Culture:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.moduleList);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(6, 64);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 52);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Startup";
            // 
            // moduleList
            // 
            this.moduleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moduleList.FormattingEnabled = true;
            this.moduleList.Location = new System.Drawing.Point(74, 21);
            this.moduleList.Name = "moduleList";
            this.moduleList.Size = new System.Drawing.Size(270, 22);
            this.moduleList.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "Module:";
            // 
            // browserTabPage
            // 
            this.browserTabPage.Controls.Add(this.groupBox2);
            this.browserTabPage.Controls.Add(this.groupBox8);
            this.browserTabPage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browserTabPage.Location = new System.Drawing.Point(4, 22);
            this.browserTabPage.Name = "browserTabPage";
            this.browserTabPage.Size = new System.Drawing.Size(362, 267);
            this.browserTabPage.TabIndex = 1;
            this.browserTabPage.Text = "File Editing";
            this.browserTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.editExtractBackupFilesCheckBox);
            this.groupBox2.Controls.Add(this.editExtractShowFilesCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(3, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 81);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Extract";
            // 
            // editExtractBackupFilesCheckBox
            // 
            this.editExtractBackupFilesCheckBox.AutoSize = true;
            this.editExtractBackupFilesCheckBox.Location = new System.Drawing.Point(9, 45);
            this.editExtractBackupFilesCheckBox.Name = "editExtractBackupFilesCheckBox";
            this.editExtractBackupFilesCheckBox.Size = new System.Drawing.Size(191, 18);
            this.editExtractBackupFilesCheckBox.TabIndex = 0;
            this.editExtractBackupFilesCheckBox.Text = "Make a backup of existing files";
            this.editExtractBackupFilesCheckBox.UseVisualStyleBackColor = true;
            this.editExtractBackupFilesCheckBox.Visible = false;
            // 
            // editExtractShowFilesCheckBox
            // 
            this.editExtractShowFilesCheckBox.AutoSize = true;
            this.editExtractShowFilesCheckBox.Location = new System.Drawing.Point(9, 21);
            this.editExtractShowFilesCheckBox.Name = "editExtractShowFilesCheckBox";
            this.editExtractShowFilesCheckBox.Size = new System.Drawing.Size(274, 18);
            this.editExtractShowFilesCheckBox.TabIndex = 1;
            this.editExtractShowFilesCheckBox.Text = "After extracting, always show files in explorer";
            this.editExtractShowFilesCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.editNewFilesFolderHardRadioButton);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.editNewFilesFolderCurrentRadioButton);
            this.groupBox8.Controls.Add(this.editFolderNewFilesBrowseButton);
            this.groupBox8.Controls.Add(this.editFolderNewFilesTextBox);
            this.groupBox8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(3, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(350, 102);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Replace";
            // 
            // editNewFilesFolderHardRadioButton
            // 
            this.editNewFilesFolderHardRadioButton.AutoSize = true;
            this.editNewFilesFolderHardRadioButton.Location = new System.Drawing.Point(19, 59);
            this.editNewFilesFolderHardRadioButton.Name = "editNewFilesFolderHardRadioButton";
            this.editNewFilesFolderHardRadioButton.Size = new System.Drawing.Size(69, 18);
            this.editNewFilesFolderHardRadioButton.TabIndex = 2;
            this.editNewFilesFolderHardRadioButton.Text = "custom:";
            this.editNewFilesFolderHardRadioButton.UseVisualStyleBackColor = true;
            this.editNewFilesFolderHardRadioButton.CheckedChanged += new System.EventHandler(this.editNewFilesFolderHardRadioButton_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "Folder for new files:";
            // 
            // editNewFilesFolderCurrentRadioButton
            // 
            this.editNewFilesFolderCurrentRadioButton.AutoSize = true;
            this.editNewFilesFolderCurrentRadioButton.Checked = true;
            this.editNewFilesFolderCurrentRadioButton.Location = new System.Drawing.Point(19, 35);
            this.editNewFilesFolderCurrentRadioButton.Name = "editNewFilesFolderCurrentRadioButton";
            this.editNewFilesFolderCurrentRadioButton.Size = new System.Drawing.Size(143, 18);
            this.editNewFilesFolderCurrentRadioButton.TabIndex = 1;
            this.editNewFilesFolderCurrentRadioButton.TabStop = true;
            this.editNewFilesFolderCurrentRadioButton.Text = "current in FileBrowser";
            this.editNewFilesFolderCurrentRadioButton.UseVisualStyleBackColor = true;
            this.editNewFilesFolderCurrentRadioButton.CheckedChanged += new System.EventHandler(this.editNewFilesFolderCurrentRadioButton_CheckedChanged);
            // 
            // editFolderNewFilesBrowseButton
            // 
            this.editFolderNewFilesBrowseButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editFolderNewFilesBrowseButton.Location = new System.Drawing.Point(317, 55);
            this.editFolderNewFilesBrowseButton.Name = "editFolderNewFilesBrowseButton";
            this.editFolderNewFilesBrowseButton.Size = new System.Drawing.Size(27, 23);
            this.editFolderNewFilesBrowseButton.TabIndex = 4;
            this.editFolderNewFilesBrowseButton.Text = "...";
            this.editFolderNewFilesBrowseButton.UseVisualStyleBackColor = true;
            this.editFolderNewFilesBrowseButton.Click += new System.EventHandler(this.editFolderNewFilesBrowseButton_Click);
            // 
            // editFolderNewFilesTextBox
            // 
            this.editFolderNewFilesTextBox.Location = new System.Drawing.Point(94, 56);
            this.editFolderNewFilesTextBox.Name = "editFolderNewFilesTextBox";
            this.editFolderNewFilesTextBox.Size = new System.Drawing.Size(217, 22);
            this.editFolderNewFilesTextBox.TabIndex = 3;
            // 
            // launcherTabPage
            // 
            this.launcherTabPage.Controls.Add(this.configGroupBox);
            this.launcherTabPage.Location = new System.Drawing.Point(4, 22);
            this.launcherTabPage.Name = "launcherTabPage";
            this.launcherTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.launcherTabPage.Size = new System.Drawing.Size(362, 267);
            this.launcherTabPage.TabIndex = 2;
            this.launcherTabPage.Text = "TDU Launcher";
            this.launcherTabPage.UseVisualStyleBackColor = true;
            // 
            // configGroupBox
            // 
            this.configGroupBox.Controls.Add(this.posRadioButton);
            this.configGroupBox.Controls.Add(this.groupBox7);
            this.configGroupBox.Controls.Add(this.standardRadioButton);
            this.configGroupBox.Controls.Add(this.radialCheckBox);
            this.configGroupBox.Controls.Add(this.fpsRadioButton);
            this.configGroupBox.Controls.Add(this.windowedRadioButton);
            this.configGroupBox.Controls.Add(this.removeConfigButton);
            this.configGroupBox.Controls.Add(this.saveConfigButton);
            this.configGroupBox.Controls.Add(this.configComboBox);
            this.configGroupBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configGroupBox.Location = new System.Drawing.Point(6, 6);
            this.configGroupBox.Name = "configGroupBox";
            this.configGroupBox.Size = new System.Drawing.Size(350, 255);
            this.configGroupBox.TabIndex = 1;
            this.configGroupBox.TabStop = false;
            this.configGroupBox.Text = "Configurations";
            // 
            // posRadioButton
            // 
            this.posRadioButton.AutoSize = true;
            this.posRadioButton.Location = new System.Drawing.Point(16, 97);
            this.posRadioButton.Name = "posRadioButton";
            this.posRadioButton.Size = new System.Drawing.Size(188, 18);
            this.posRadioButton.TabIndex = 5;
            this.posRadioButton.TabStop = true;
            this.posRadioButton.Text = "Show coordinates information";
            this.posRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.runAfterBrowseButton);
            this.groupBox7.Controls.Add(this.runAfterTextBox);
            this.groupBox7.Controls.Add(this.runBeforeBrowseButton);
            this.groupBox7.Controls.Add(this.runBeforeTextBox);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Location = new System.Drawing.Point(16, 152);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(316, 91);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Run...";
            // 
            // runAfterBrowseButton
            // 
            this.runAfterBrowseButton.Font = new System.Drawing.Font("Tahoma", 7F);
            this.runAfterBrowseButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.runAfterBrowseButton.Location = new System.Drawing.Point(286, 53);
            this.runAfterBrowseButton.Name = "runAfterBrowseButton";
            this.runAfterBrowseButton.Size = new System.Drawing.Size(24, 24);
            this.runAfterBrowseButton.TabIndex = 5;
            this.runAfterBrowseButton.Text = "...";
            this.runAfterBrowseButton.UseVisualStyleBackColor = true;
            this.runAfterBrowseButton.Click += new System.EventHandler(this.runAfterBrowseButton_Click);
            // 
            // runAfterTextBox
            // 
            this.runAfterTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runAfterTextBox.Location = new System.Drawing.Point(55, 55);
            this.runAfterTextBox.Name = "runAfterTextBox";
            this.runAfterTextBox.Size = new System.Drawing.Size(225, 21);
            this.runAfterTextBox.TabIndex = 4;
            // 
            // runBeforeBrowseButton
            // 
            this.runBeforeBrowseButton.Font = new System.Drawing.Font("Tahoma", 7F);
            this.runBeforeBrowseButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.runBeforeBrowseButton.Location = new System.Drawing.Point(286, 23);
            this.runBeforeBrowseButton.Name = "runBeforeBrowseButton";
            this.runBeforeBrowseButton.Size = new System.Drawing.Size(24, 24);
            this.runBeforeBrowseButton.TabIndex = 2;
            this.runBeforeBrowseButton.Text = "...";
            this.runBeforeBrowseButton.UseVisualStyleBackColor = true;
            this.runBeforeBrowseButton.Click += new System.EventHandler(this.runBeforeBrowseButton_Click);
            // 
            // runBeforeTextBox
            // 
            this.runBeforeTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runBeforeTextBox.Location = new System.Drawing.Point(55, 25);
            this.runBeforeTextBox.Name = "runBeforeTextBox";
            this.runBeforeTextBox.Size = new System.Drawing.Size(225, 21);
            this.runBeforeTextBox.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "After:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Before:";
            // 
            // standardRadioButton
            // 
            this.standardRadioButton.AutoSize = true;
            this.standardRadioButton.Location = new System.Drawing.Point(16, 121);
            this.standardRadioButton.Name = "standardRadioButton";
            this.standardRadioButton.Size = new System.Drawing.Size(151, 18);
            this.standardRadioButton.TabIndex = 6;
            this.standardRadioButton.TabStop = true;
            this.standardRadioButton.Text = "Standard mode (none)";
            this.standardRadioButton.UseVisualStyleBackColor = true;
            // 
            // radialCheckBox
            // 
            this.radialCheckBox.AutoSize = true;
            this.radialCheckBox.Location = new System.Drawing.Point(227, 49);
            this.radialCheckBox.Name = "radialCheckBox";
            this.radialCheckBox.Size = new System.Drawing.Size(117, 18);
            this.radialCheckBox.TabIndex = 7;
            this.radialCheckBox.Text = "Delete radial.cdb";
            this.radialCheckBox.UseVisualStyleBackColor = true;
            // 
            // fpsRadioButton
            // 
            this.fpsRadioButton.AutoSize = true;
            this.fpsRadioButton.Location = new System.Drawing.Point(16, 73);
            this.fpsRadioButton.Name = "fpsRadioButton";
            this.fpsRadioButton.Size = new System.Drawing.Size(127, 18);
            this.fpsRadioButton.TabIndex = 4;
            this.fpsRadioButton.TabStop = true;
            this.fpsRadioButton.Text = "Show FPS counter";
            this.fpsRadioButton.UseVisualStyleBackColor = true;
            // 
            // windowedRadioButton
            // 
            this.windowedRadioButton.AutoSize = true;
            this.windowedRadioButton.Location = new System.Drawing.Point(16, 49);
            this.windowedRadioButton.Name = "windowedRadioButton";
            this.windowedRadioButton.Size = new System.Drawing.Size(188, 18);
            this.windowedRadioButton.TabIndex = 3;
            this.windowedRadioButton.TabStop = true;
            this.windowedRadioButton.Text = "Windowed mode (1280x720)";
            this.windowedRadioButton.UseVisualStyleBackColor = true;
            // 
            // removeConfigButton
            // 
            this.removeConfigButton.Image = global::TDUModdingTools.Properties.Resources.trash_16;
            this.removeConfigButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.removeConfigButton.Location = new System.Drawing.Point(320, 19);
            this.removeConfigButton.Name = "removeConfigButton";
            this.removeConfigButton.Size = new System.Drawing.Size(24, 24);
            this.removeConfigButton.TabIndex = 2;
            this.removeConfigButton.UseVisualStyleBackColor = true;
            this.removeConfigButton.Click += new System.EventHandler(this.removeConfigButton_Click);
            // 
            // saveConfigButton
            // 
            this.saveConfigButton.Image = ((System.Drawing.Image)(resources.GetObject("saveConfigButton.Image")));
            this.saveConfigButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.saveConfigButton.Location = new System.Drawing.Point(292, 19);
            this.saveConfigButton.Name = "saveConfigButton";
            this.saveConfigButton.Size = new System.Drawing.Size(24, 24);
            this.saveConfigButton.TabIndex = 1;
            this.saveConfigButton.UseVisualStyleBackColor = true;
            this.saveConfigButton.Click += new System.EventHandler(this.saveConfigButton_Click);
            // 
            // configComboBox
            // 
            this.configComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.configComboBox.FormattingEnabled = true;
            this.configComboBox.Location = new System.Drawing.Point(6, 21);
            this.configComboBox.Name = "configComboBox";
            this.configComboBox.Size = new System.Drawing.Size(280, 22);
            this.configComboBox.TabIndex = 0;
            this.configComboBox.SelectedIndexChanged += new System.EventHandler(this.configComboBox_SelectedIndexChanged);
            // 
            // patchEditorTabPage
            // 
            this.patchEditorTabPage.Controls.Add(this.groupBox5);
            this.patchEditorTabPage.Location = new System.Drawing.Point(4, 22);
            this.patchEditorTabPage.Name = "patchEditorTabPage";
            this.patchEditorTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.patchEditorTabPage.Size = new System.Drawing.Size(362, 267);
            this.patchEditorTabPage.TabIndex = 3;
            this.patchEditorTabPage.Text = "Patch Editor";
            this.patchEditorTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.reportClearingCheckBox);
            this.groupBox5.Controls.Add(this.reportAutoScrollCheckBox);
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(350, 75);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Execution report";
            // 
            // reportClearingCheckBox
            // 
            this.reportClearingCheckBox.AutoSize = true;
            this.reportClearingCheckBox.Location = new System.Drawing.Point(9, 45);
            this.reportClearingCheckBox.Name = "reportClearingCheckBox";
            this.reportClearingCheckBox.Size = new System.Drawing.Size(210, 18);
            this.reportClearingCheckBox.TabIndex = 1;
            this.reportClearingCheckBox.Text = "Clear report before running patch";
            this.reportClearingCheckBox.UseVisualStyleBackColor = true;
            // 
            // reportAutoScrollCheckBox
            // 
            this.reportAutoScrollCheckBox.AutoSize = true;
            this.reportAutoScrollCheckBox.Location = new System.Drawing.Point(9, 21);
            this.reportAutoScrollCheckBox.Name = "reportAutoScrollCheckBox";
            this.reportAutoScrollCheckBox.Size = new System.Drawing.Size(166, 18);
            this.reportAutoScrollCheckBox.TabIndex = 0;
            this.reportAutoScrollCheckBox.Text = "Scroll report automatically";
            this.reportAutoScrollCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(317, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "TDU root:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(74, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(237, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(64, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(223, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "- Used when file Editing or Drag&&Drop -";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(15, 38);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(226, 18);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Enlarge/reduce BNK when necessary";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(15, 62);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(195, 18);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Maintain BNK size (old method)";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // trackPackTabPage
            // 
            this.trackPackTabPage.Controls.Add(this.groupBox9);
            this.trackPackTabPage.Location = new System.Drawing.Point(4, 22);
            this.trackPackTabPage.Name = "trackPackTabPage";
            this.trackPackTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.trackPackTabPage.Size = new System.Drawing.Size(362, 267);
            this.trackPackTabPage.TabIndex = 4;
            this.trackPackTabPage.Text = "Track Pack";
            this.trackPackTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.profilesComboBox);
            this.groupBox9.Controls.Add(this.label6);
            this.groupBox9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.Location = new System.Drawing.Point(6, 6);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(350, 52);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "TDU profiles";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "Custom tracks in:";
            // 
            // profilesComboBox
            // 
            this.profilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profilesComboBox.FormattingEnabled = true;
            this.profilesComboBox.Location = new System.Drawing.Point(113, 21);
            this.profilesComboBox.Name = "profilesComboBox";
            this.profilesComboBox.Size = new System.Drawing.Size(231, 22);
            this.profilesComboBox.TabIndex = 1;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 346);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.mainTabPage.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.browserTabPage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.launcherTabPage.ResumeLayout(false);
            this.configGroupBox.ResumeLayout(false);
            this.configGroupBox.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.patchEditorTabPage.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.trackPackTabPage.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rootTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button rootBrowseButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage mainTabPage;
        private System.Windows.Forms.TabPage browserTabPage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox moduleList;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox languageList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage launcherTabPage;
        private System.Windows.Forms.GroupBox configGroupBox;
        private System.Windows.Forms.Button saveConfigButton;
        private System.Windows.Forms.ComboBox configComboBox;
        private System.Windows.Forms.Button removeConfigButton;
        private System.Windows.Forms.CheckBox radialCheckBox;
        private System.Windows.Forms.RadioButton fpsRadioButton;
        private System.Windows.Forms.RadioButton windowedRadioButton;
        private System.Windows.Forms.RadioButton standardRadioButton;
        private System.Windows.Forms.TabPage patchEditorTabPage;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox reportAutoScrollCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox debugModeCheckBox;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button runBeforeBrowseButton;
        private System.Windows.Forms.TextBox runBeforeTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button runAfterBrowseButton;
        private System.Windows.Forms.TextBox runAfterTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button fixRegistryButton;
        private System.Windows.Forms.Button clearRadialButton;
        private System.Windows.Forms.Button editFolderNewFilesBrowseButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox editFolderNewFilesTextBox;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton editNewFilesFolderHardRadioButton;
        private System.Windows.Forms.RadioButton editNewFilesFolderCurrentRadioButton;
        private System.Windows.Forms.CheckBox reportClearingCheckBox;
        private System.Windows.Forms.CheckBox editExtractShowFilesCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox editExtractBackupFilesCheckBox;
        private System.Windows.Forms.RadioButton posRadioButton;
        private System.Windows.Forms.TabPage trackPackTabPage;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox profilesComboBox;
        private System.Windows.Forms.Label label6;
    }
}
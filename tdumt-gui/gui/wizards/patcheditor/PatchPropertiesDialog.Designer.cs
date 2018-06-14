namespace TDUModdingTools.gui.wizards.patcheditor
{
    partial class PatchPropertiesDialog
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.patchPropertiesTabControl = new System.Windows.Forms.TabControl();
            this.mainTabPage = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.installerFileNameTextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.browseSlotsButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.slotRefTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.freeCommentTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTextBox = new System.Windows.Forms.TextBox();
            this.versionTextBox = new System.Windows.Forms.TextBox();
            this.pictureTabPage = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.picPanel = new System.Windows.Forms.Panel();
            this.notGeneratedLabel = new System.Windows.Forms.Label();
            this.removePicButton = new System.Windows.Forms.Button();
            this.browsePicButton = new System.Windows.Forms.Button();
            this.creditsTabPage = new System.Windows.Forms.TabPage();
            this.setNameButton = new System.Windows.Forms.Button();
            this.setRoleButton = new System.Windows.Forms.Button();
            this.creditsListView = new System.Windows.Forms.ListView();
            this.roleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.authorTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupsTabPage = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.requiredNameTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupDeleteButton = new System.Windows.Forms.Button();
            this.excludedGroupsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.precheckedCheckBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.requiredGroupComboBox = new System.Windows.Forms.ComboBox();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label12 = new System.Windows.Forms.Label();
            this.infoWebsiteTextBox = new System.Windows.Forms.TextBox();
            this.patchPropertiesTabControl.SuspendLayout();
            this.mainTabPage.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pictureTabPage.SuspendLayout();
            this.picPanel.SuspendLayout();
            this.creditsTabPage.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupsTabPage.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(255, 265);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(336, 265);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // patchPropertiesTabControl
            // 
            this.patchPropertiesTabControl.Controls.Add(this.mainTabPage);
            this.patchPropertiesTabControl.Controls.Add(this.pictureTabPage);
            this.patchPropertiesTabControl.Controls.Add(this.creditsTabPage);
            this.patchPropertiesTabControl.Controls.Add(this.groupsTabPage);
            this.patchPropertiesTabControl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patchPropertiesTabControl.Location = new System.Drawing.Point(12, 12);
            this.patchPropertiesTabControl.Name = "patchPropertiesTabControl";
            this.patchPropertiesTabControl.SelectedIndex = 0;
            this.patchPropertiesTabControl.Size = new System.Drawing.Size(399, 247);
            this.patchPropertiesTabControl.TabIndex = 0;
            // 
            // mainTabPage
            // 
            this.mainTabPage.Controls.Add(this.panel6);
            this.mainTabPage.Controls.Add(this.panel2);
            this.mainTabPage.Controls.Add(this.panel1);
            this.mainTabPage.Location = new System.Drawing.Point(4, 23);
            this.mainTabPage.Name = "mainTabPage";
            this.mainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mainTabPage.Size = new System.Drawing.Size(391, 220);
            this.mainTabPage.TabIndex = 0;
            this.mainTabPage.Text = "Main";
            this.mainTabPage.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.installerFileNameTextBox);
            this.panel6.Location = new System.Drawing.Point(6, 178);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(379, 39);
            this.panel6.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 14);
            this.label10.TabIndex = 0;
            this.label10.Text = "Installer file name:";
            // 
            // installerFileNameTextBox
            // 
            this.installerFileNameTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installerFileNameTextBox.Location = new System.Drawing.Point(157, 8);
            this.installerFileNameTextBox.Name = "installerFileNameTextBox";
            this.installerFileNameTextBox.Size = new System.Drawing.Size(211, 22);
            this.installerFileNameTextBox.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.browseSlotsButton);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.slotRefTextBox);
            this.panel2.Location = new System.Drawing.Point(6, 133);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(379, 39);
            this.panel2.TabIndex = 1;
            // 
            // browseSlotsButton
            // 
            this.browseSlotsButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseSlotsButton.Location = new System.Drawing.Point(341, 8);
            this.browseSlotsButton.Name = "browseSlotsButton";
            this.browseSlotsButton.Size = new System.Drawing.Size(27, 21);
            this.browseSlotsButton.TabIndex = 2;
            this.browseSlotsButton.Text = "...";
            this.browseSlotsButton.UseVisualStyleBackColor = true;
            this.browseSlotsButton.Click += new System.EventHandler(this.browseSlotsButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "Slot Reference:";
            // 
            // slotRefTextBox
            // 
            this.slotRefTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slotRefTextBox.Location = new System.Drawing.Point(157, 8);
            this.slotRefTextBox.Name = "slotRefTextBox";
            this.slotRefTextBox.Size = new System.Drawing.Size(178, 22);
            this.slotRefTextBox.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.infoWebsiteTextBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.freeCommentTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nameTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dateTextBox);
            this.panel1.Controls.Add(this.versionTextBox);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 121);
            this.panel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "Free comment:";
            // 
            // freeCommentTextBox
            // 
            this.freeCommentTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.freeCommentTextBox.Location = new System.Drawing.Point(157, 66);
            this.freeCommentTextBox.Name = "freeCommentTextBox";
            this.freeCommentTextBox.Size = new System.Drawing.Size(211, 22);
            this.freeCommentTextBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Patch or project name:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTextBox.Location = new System.Drawing.Point(157, 7);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(211, 22);
            this.nameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Version/Date:";
            // 
            // dateTextBox
            // 
            this.dateTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTextBox.Location = new System.Drawing.Point(202, 37);
            this.dateTextBox.Name = "dateTextBox";
            this.dateTextBox.Size = new System.Drawing.Size(88, 22);
            this.dateTextBox.TabIndex = 4;
            // 
            // versionTextBox
            // 
            this.versionTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionTextBox.Location = new System.Drawing.Point(157, 37);
            this.versionTextBox.Name = "versionTextBox";
            this.versionTextBox.Size = new System.Drawing.Size(39, 22);
            this.versionTextBox.TabIndex = 3;
            // 
            // pictureTabPage
            // 
            this.pictureTabPage.Controls.Add(this.label11);
            this.pictureTabPage.Controls.Add(this.picPanel);
            this.pictureTabPage.Controls.Add(this.removePicButton);
            this.pictureTabPage.Controls.Add(this.browsePicButton);
            this.pictureTabPage.Location = new System.Drawing.Point(4, 23);
            this.pictureTabPage.Name = "pictureTabPage";
            this.pictureTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.pictureTabPage.Size = new System.Drawing.Size(391, 220);
            this.pictureTabPage.TabIndex = 3;
            this.pictureTabPage.Text = "Picture";
            this.pictureTabPage.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(229, 195);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 14);
            this.label11.TabIndex = 3;
            this.label11.Text = "< can\'t be undone!";
            // 
            // picPanel
            // 
            this.picPanel.BackColor = System.Drawing.Color.Transparent;
            this.picPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPanel.Controls.Add(this.notGeneratedLabel);
            this.picPanel.Location = new System.Drawing.Point(35, 6);
            this.picPanel.Name = "picPanel";
            this.picPanel.Size = new System.Drawing.Size(320, 180);
            this.picPanel.TabIndex = 0;
            // 
            // notGeneratedLabel
            // 
            this.notGeneratedLabel.AutoSize = true;
            this.notGeneratedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.notGeneratedLabel.Location = new System.Drawing.Point(31, 82);
            this.notGeneratedLabel.Name = "notGeneratedLabel";
            this.notGeneratedLabel.Size = new System.Drawing.Size(257, 14);
            this.notGeneratedLabel.TabIndex = 0;
            this.notGeneratedLabel.Text = "You have to generate current patch first";
            // 
            // removePicButton
            // 
            this.removePicButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removePicButton.Location = new System.Drawing.Point(133, 191);
            this.removePicButton.Name = "removePicButton";
            this.removePicButton.Size = new System.Drawing.Size(92, 23);
            this.removePicButton.TabIndex = 2;
            this.removePicButton.Text = "Remove";
            this.removePicButton.UseVisualStyleBackColor = true;
            this.removePicButton.Click += new System.EventHandler(this.removePicButton_Click);
            // 
            // browsePicButton
            // 
            this.browsePicButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browsePicButton.Location = new System.Drawing.Point(35, 191);
            this.browsePicButton.Name = "browsePicButton";
            this.browsePicButton.Size = new System.Drawing.Size(92, 23);
            this.browsePicButton.TabIndex = 1;
            this.browsePicButton.Text = "Browse...";
            this.browsePicButton.UseVisualStyleBackColor = true;
            this.browsePicButton.Click += new System.EventHandler(this.browsePicButton_Click);
            // 
            // creditsTabPage
            // 
            this.creditsTabPage.Controls.Add(this.setNameButton);
            this.creditsTabPage.Controls.Add(this.setRoleButton);
            this.creditsTabPage.Controls.Add(this.creditsListView);
            this.creditsTabPage.Controls.Add(this.panel3);
            this.creditsTabPage.Location = new System.Drawing.Point(4, 23);
            this.creditsTabPage.Name = "creditsTabPage";
            this.creditsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.creditsTabPage.Size = new System.Drawing.Size(391, 220);
            this.creditsTabPage.TabIndex = 1;
            this.creditsTabPage.Text = "Credits!";
            this.creditsTabPage.UseVisualStyleBackColor = true;
            // 
            // setNameButton
            // 
            this.setNameButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setNameButton.Location = new System.Drawing.Point(104, 191);
            this.setNameButton.Name = "setNameButton";
            this.setNameButton.Size = new System.Drawing.Size(92, 23);
            this.setNameButton.TabIndex = 3;
            this.setNameButton.Text = "Set name...";
            this.setNameButton.UseVisualStyleBackColor = true;
            this.setNameButton.Click += new System.EventHandler(this.setNameButton_Click);
            // 
            // setRoleButton
            // 
            this.setRoleButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setRoleButton.Location = new System.Drawing.Point(6, 191);
            this.setRoleButton.Name = "setRoleButton";
            this.setRoleButton.Size = new System.Drawing.Size(92, 23);
            this.setRoleButton.TabIndex = 2;
            this.setRoleButton.Text = "Set custom...";
            this.setRoleButton.UseVisualStyleBackColor = true;
            this.setRoleButton.Click += new System.EventHandler(this.setRoleButton_Click);
            // 
            // creditsListView
            // 
            this.creditsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.roleColumnHeader,
            this.nameColumnHeader});
            this.creditsListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creditsListView.FullRowSelect = true;
            this.creditsListView.GridLines = true;
            this.creditsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.creditsListView.HideSelection = false;
            this.creditsListView.Location = new System.Drawing.Point(6, 54);
            this.creditsListView.Name = "creditsListView";
            this.creditsListView.Size = new System.Drawing.Size(379, 131);
            this.creditsListView.TabIndex = 1;
            this.creditsListView.UseCompatibleStateImageBehavior = false;
            this.creditsListView.View = System.Windows.Forms.View.Details;
            // 
            // roleColumnHeader
            // 
            this.roleColumnHeader.Text = "Role";
            this.roleColumnHeader.Width = 135;
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 217;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.authorTextBox);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(6, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(379, 42);
            this.panel3.TabIndex = 0;
            // 
            // authorTextBox
            // 
            this.authorTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.authorTextBox.Location = new System.Drawing.Point(119, 9);
            this.authorTextBox.Name = "authorTextBox";
            this.authorTextBox.Size = new System.Drawing.Size(211, 22);
            this.authorTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(46, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Publisher:";
            // 
            // groupsTabPage
            // 
            this.groupsTabPage.Controls.Add(this.panel5);
            this.groupsTabPage.Controls.Add(this.panel4);
            this.groupsTabPage.Location = new System.Drawing.Point(4, 23);
            this.groupsTabPage.Name = "groupsTabPage";
            this.groupsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.groupsTabPage.Size = new System.Drawing.Size(391, 220);
            this.groupsTabPage.TabIndex = 2;
            this.groupsTabPage.Text = "Groups";
            this.groupsTabPage.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.requiredNameTextBox);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Location = new System.Drawing.Point(6, 174);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(379, 40);
            this.panel5.TabIndex = 1;
            // 
            // requiredNameTextBox
            // 
            this.requiredNameTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requiredNameTextBox.Location = new System.Drawing.Point(143, 9);
            this.requiredNameTextBox.Name = "requiredNameTextBox";
            this.requiredNameTextBox.Size = new System.Drawing.Size(222, 22);
            this.requiredNameTextBox.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "Default group name:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.groupDeleteButton);
            this.panel4.Controls.Add(this.excludedGroupsCheckedListBox);
            this.panel4.Controls.Add(this.precheckedCheckBox);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.requiredGroupComboBox);
            this.panel4.Controls.Add(this.groupComboBox);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Location = new System.Drawing.Point(6, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(379, 162);
            this.panel4.TabIndex = 0;
            // 
            // groupDeleteButton
            // 
            this.groupDeleteButton.Image = global::TDUModdingTools.Properties.Resources.trash_16;
            this.groupDeleteButton.Location = new System.Drawing.Point(350, 10);
            this.groupDeleteButton.Name = "groupDeleteButton";
            this.groupDeleteButton.Size = new System.Drawing.Size(24, 24);
            this.groupDeleteButton.TabIndex = 3;
            this.groupDeleteButton.UseVisualStyleBackColor = true;
            this.groupDeleteButton.Click += new System.EventHandler(this.groupDeleteButton_Click);
            // 
            // excludedGroupsCheckedListBox
            // 
            this.excludedGroupsCheckedListBox.CheckOnClick = true;
            this.excludedGroupsCheckedListBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.excludedGroupsCheckedListBox.FormattingEnabled = true;
            this.excludedGroupsCheckedListBox.Location = new System.Drawing.Point(71, 100);
            this.excludedGroupsCheckedListBox.Name = "excludedGroupsCheckedListBox";
            this.excludedGroupsCheckedListBox.Size = new System.Drawing.Size(303, 55);
            this.excludedGroupsCheckedListBox.TabIndex = 8;
            this.excludedGroupsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.excludedGroupsCheckedListBox_ItemCheck);
            // 
            // precheckedCheckBox
            // 
            this.precheckedCheckBox.AutoSize = true;
            this.precheckedCheckBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.precheckedCheckBox.Location = new System.Drawing.Point(11, 45);
            this.precheckedCheckBox.Name = "precheckedCheckBox";
            this.precheckedCheckBox.Size = new System.Drawing.Size(221, 18);
            this.precheckedCheckBox.TabIndex = 4;
            this.precheckedCheckBox.Text = "Pre-checked in ModAndPlay installer";
            this.precheckedCheckBox.UseVisualStyleBackColor = true;
            this.precheckedCheckBox.CheckedChanged += new System.EventHandler(this.precheckedCheckBox_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 14);
            this.label8.TabIndex = 7;
            this.label8.Text = "Excludes:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 14);
            this.label7.TabIndex = 5;
            this.label7.Text = "Requires:";
            // 
            // requiredGroupComboBox
            // 
            this.requiredGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.requiredGroupComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requiredGroupComboBox.FormattingEnabled = true;
            this.requiredGroupComboBox.Location = new System.Drawing.Point(71, 72);
            this.requiredGroupComboBox.Name = "requiredGroupComboBox";
            this.requiredGroupComboBox.Size = new System.Drawing.Size(303, 22);
            this.requiredGroupComboBox.TabIndex = 6;
            this.requiredGroupComboBox.SelectedIndexChanged += new System.EventHandler(this.requiredGroupComboBox_SelectedIndexChanged);
            // 
            // groupComboBox
            // 
            this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.Location = new System.Drawing.Point(99, 11);
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(245, 22);
            this.groupComboBox.TabIndex = 1;
            this.groupComboBox.SelectedIndexChanged += new System.EventHandler(this.groupComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "Install group:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 97);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(138, 14);
            this.label12.TabIndex = 7;
            this.label12.Text = "Information Website:";
            // 
            // infoWebsiteTextBox
            // 
            this.infoWebsiteTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoWebsiteTextBox.Location = new System.Drawing.Point(157, 94);
            this.infoWebsiteTextBox.Name = "infoWebsiteTextBox";
            this.infoWebsiteTextBox.Size = new System.Drawing.Size(211, 22);
            this.infoWebsiteTextBox.TabIndex = 8;
            // 
            // PatchPropertiesDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 300);
            this.Controls.Add(this.patchPropertiesTabControl);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatchPropertiesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patch properties...";
            this.patchPropertiesTabControl.ResumeLayout(false);
            this.mainTabPage.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pictureTabPage.ResumeLayout(false);
            this.pictureTabPage.PerformLayout();
            this.picPanel.ResumeLayout(false);
            this.picPanel.PerformLayout();
            this.creditsTabPage.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupsTabPage.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TabControl patchPropertiesTabControl;
        private System.Windows.Forms.TabPage mainTabPage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button browseSlotsButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox slotRefTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dateTextBox;
        private System.Windows.Forms.TextBox versionTextBox;
        private System.Windows.Forms.TabPage creditsTabPage;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox authorTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView creditsListView;
        private System.Windows.Forms.ColumnHeader roleColumnHeader;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.Button setNameButton;
        private System.Windows.Forms.Button setRoleButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox freeCommentTextBox;
        private System.Windows.Forms.TabPage groupsTabPage;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox requiredGroupComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox groupComboBox;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox requiredNameTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox precheckedCheckBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox excludedGroupsCheckedListBox;
        private System.Windows.Forms.Button groupDeleteButton;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox installerFileNameTextBox;
        private System.Windows.Forms.TabPage pictureTabPage;
        private System.Windows.Forms.Button removePicButton;
        private System.Windows.Forms.Button browsePicButton;
        private System.Windows.Forms.Panel picPanel;
        private System.Windows.Forms.Label notGeneratedLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox infoWebsiteTextBox;
    }
}
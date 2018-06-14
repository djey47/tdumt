namespace TDUModAndPlay.gui
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tduPathTextbox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.contribLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.browseTDULink = new System.Windows.Forms.LinkLabel();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.picPanel = new System.Windows.Forms.Panel();
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.progressPanel = new System.Windows.Forms.Panel();
            this.mainProgressBar = new System.Windows.Forms.ProgressBar();
            this.infoLabel = new System.Windows.Forms.Label();
            this.homePanel = new System.Windows.Forms.Panel();
            this.freeLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.installerBtn = new System.Windows.Forms.Button();
            this.uninstallButton = new System.Windows.Forms.Button();
            this.versionLabel = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.homeButtonPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.installButtonPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.author8Label = new System.Windows.Forms.Label();
            this.role8Label = new System.Windows.Forms.Label();
            this.author7Label = new System.Windows.Forms.Label();
            this.role7Label = new System.Windows.Forms.Label();
            this.author6Label = new System.Windows.Forms.Label();
            this.role6Label = new System.Windows.Forms.Label();
            this.author5Label = new System.Windows.Forms.Label();
            this.role5Label = new System.Windows.Forms.Label();
            this.author4Label = new System.Windows.Forms.Label();
            this.role4Label = new System.Windows.Forms.Label();
            this.author3Label = new System.Windows.Forms.Label();
            this.role3Label = new System.Windows.Forms.Label();
            this.author2Label = new System.Windows.Forms.Label();
            this.role2Label = new System.Windows.Forms.Label();
            this.author1Label = new System.Windows.Forms.Label();
            this.role1Label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tableLayoutPanel.SuspendLayout();
            this.picPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.controlPanel.SuspendLayout();
            this.progressPanel.SuspendLayout();
            this.homePanel.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.homeButtonPanel.SuspendLayout();
            this.installButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tduPathTextbox
            // 
            this.tduPathTextbox.BackColor = System.Drawing.SystemColors.Control;
            this.tduPathTextbox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tduPathTextbox.ForeColor = System.Drawing.Color.Maroon;
            this.tduPathTextbox.Location = new System.Drawing.Point(87, 38);
            this.tduPathTextbox.Multiline = true;
            this.tduPathTextbox.Name = "tduPathTextbox";
            this.tduPathTextbox.ReadOnly = true;
            this.tduPathTextbox.Size = new System.Drawing.Size(531, 22);
            this.tduPathTextbox.TabIndex = 3;
            // 
            // titleLabel
            // 
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.DarkCyan;
            this.titleLabel.Location = new System.Drawing.Point(11, 7);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(615, 31);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "<Mod title here>";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contribLabel
            // 
            this.contribLabel.BackColor = System.Drawing.Color.Transparent;
            this.contribLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contribLabel.ForeColor = System.Drawing.Color.Maroon;
            this.contribLabel.Location = new System.Drawing.Point(11, 37);
            this.contribLabel.Name = "contribLabel";
            this.contribLabel.Size = new System.Drawing.Size(615, 19);
            this.contribLabel.TabIndex = 2;
            this.contribLabel.Text = "<Author> - <Date>";
            this.contribLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(11, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "TDU install path:";
            // 
            // browseTDULink
            // 
            this.browseTDULink.AutoSize = true;
            this.browseTDULink.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseTDULink.LinkColor = System.Drawing.Color.Maroon;
            this.browseTDULink.Location = new System.Drawing.Point(22, 39);
            this.browseTDULink.Name = "browseTDULink";
            this.browseTDULink.Size = new System.Drawing.Size(59, 14);
            this.browseTDULink.TabIndex = 2;
            this.browseTDULink.TabStop = true;
            this.browseTDULink.Text = "browse...";
            this.browseTDULink.VisitedLinkColor = System.Drawing.Color.DimGray;
            this.browseTDULink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.parcourirLnk_LinkClicked);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Sélectionnez l\'emplacement où TDU est installé.";
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.picPanel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.controlPanel, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 360F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(642, 464);
            this.tableLayoutPanel.TabIndex = 6;
            // 
            // picPanel
            // 
            this.picPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picPanel.Controls.Add(this.mainPictureBox);
            this.picPanel.Controls.Add(this.webBrowser);
            this.picPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPanel.Location = new System.Drawing.Point(0, 0);
            this.picPanel.Margin = new System.Windows.Forms.Padding(0);
            this.picPanel.Name = "picPanel";
            this.picPanel.Size = new System.Drawing.Size(642, 360);
            this.picPanel.TabIndex = 0;
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.BackColor = System.Drawing.Color.Black;
            this.mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPictureBox.ErrorImage = null;
            this.mainPictureBox.InitialImage = null;
            this.mainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.mainPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(642, 360);
            this.mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.mainPictureBox.TabIndex = 2;
            this.mainPictureBox.TabStop = false;
            this.mainPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseClick);
            // 
            // controlPanel
            // 
            this.controlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.controlPanel.Controls.Add(this.progressPanel);
            this.controlPanel.Controls.Add(this.infoLabel);
            this.controlPanel.Controls.Add(this.homePanel);
            this.controlPanel.Controls.Add(this.tduPathTextbox);
            this.controlPanel.Controls.Add(this.label1);
            this.controlPanel.Controls.Add(this.browseTDULink);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(0, 360);
            this.controlPanel.Margin = new System.Windows.Forms.Padding(0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(642, 104);
            this.controlPanel.TabIndex = 1;
            // 
            // progressPanel
            // 
            this.progressPanel.Controls.Add(this.mainProgressBar);
            this.progressPanel.Location = new System.Drawing.Point(0, 77);
            this.progressPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(640, 26);
            this.progressPanel.TabIndex = 12;
            this.progressPanel.Visible = false;
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mainProgressBar.Location = new System.Drawing.Point(0, 4);
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(640, 22);
            this.mainProgressBar.TabIndex = 9;
            // 
            // infoLabel
            // 
            this.infoLabel.BackColor = System.Drawing.Color.Transparent;
            this.infoLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.infoLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel.ForeColor = System.Drawing.Color.Orange;
            this.infoLabel.Location = new System.Drawing.Point(0, 83);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(642, 21);
            this.infoLabel.TabIndex = 13;
            this.infoLabel.Text = "<INFO>";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.infoLabel.Visible = false;
            // 
            // homePanel
            // 
            this.homePanel.BackColor = System.Drawing.SystemColors.Window;
            this.homePanel.Controls.Add(this.freeLabel);
            this.homePanel.Controls.Add(this.contribLabel);
            this.homePanel.Controls.Add(this.titleLabel);
            this.homePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homePanel.Location = new System.Drawing.Point(0, 0);
            this.homePanel.Margin = new System.Windows.Forms.Padding(0);
            this.homePanel.Name = "homePanel";
            this.homePanel.Size = new System.Drawing.Size(642, 104);
            this.homePanel.TabIndex = 10;
            // 
            // freeLabel
            // 
            this.freeLabel.BackColor = System.Drawing.Color.Transparent;
            this.freeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.freeLabel.ForeColor = System.Drawing.Color.Black;
            this.freeLabel.Location = new System.Drawing.Point(11, 56);
            this.freeLabel.Name = "freeLabel";
            this.freeLabel.Size = new System.Drawing.Size(615, 18);
            this.freeLabel.TabIndex = 3;
            this.freeLabel.Text = "<Free field>";
            this.freeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Image = global::TDUModAndPlay.Properties.Resources.arrow_forward_32_blue;
            this.okButton.Location = new System.Drawing.Point(3, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(112, 57);
            this.okButton.TabIndex = 6;
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // installerBtn
            // 
            this.installerBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.installerBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installerBtn.ForeColor = System.Drawing.Color.DarkGreen;
            this.installerBtn.Image = global::TDUModAndPlay.Properties.Resources.arrow_forward_32;
            this.installerBtn.Location = new System.Drawing.Point(3, 3);
            this.installerBtn.Name = "installerBtn";
            this.installerBtn.Size = new System.Drawing.Size(112, 39);
            this.installerBtn.TabIndex = 4;
            this.installerBtn.Text = "NEXT";
            this.installerBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.installerBtn.UseVisualStyleBackColor = true;
            this.installerBtn.Click += new System.EventHandler(this.installerBtn_Click);
            // 
            // uninstallButton
            // 
            this.uninstallButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.uninstallButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uninstallButton.ForeColor = System.Drawing.Color.Tomato;
            this.uninstallButton.Image = global::TDUModAndPlay.Properties.Resources.arrow_back;
            this.uninstallButton.Location = new System.Drawing.Point(3, 44);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.Size = new System.Drawing.Size(112, 31);
            this.uninstallButton.TabIndex = 11;
            this.uninstallButton.Text = "UNINSTALL";
            this.uninstallButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.uninstallButton.UseVisualStyleBackColor = true;
            this.uninstallButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // versionLabel
            // 
            this.versionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.ForeColor = System.Drawing.Color.White;
            this.versionLabel.Location = new System.Drawing.Point(4, 60);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(111, 14);
            this.versionLabel.TabIndex = 7;
            this.versionLabel.Text = "V.x.y";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.White;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tableLayoutPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer.Panel2.Controls.Add(this.panel1);
            this.splitContainer.Panel2.Controls.Add(this.author8Label);
            this.splitContainer.Panel2.Controls.Add(this.role8Label);
            this.splitContainer.Panel2.Controls.Add(this.author7Label);
            this.splitContainer.Panel2.Controls.Add(this.role7Label);
            this.splitContainer.Panel2.Controls.Add(this.author6Label);
            this.splitContainer.Panel2.Controls.Add(this.role6Label);
            this.splitContainer.Panel2.Controls.Add(this.author5Label);
            this.splitContainer.Panel2.Controls.Add(this.role5Label);
            this.splitContainer.Panel2.Controls.Add(this.author4Label);
            this.splitContainer.Panel2.Controls.Add(this.role4Label);
            this.splitContainer.Panel2.Controls.Add(this.author3Label);
            this.splitContainer.Panel2.Controls.Add(this.role3Label);
            this.splitContainer.Panel2.Controls.Add(this.author2Label);
            this.splitContainer.Panel2.Controls.Add(this.role2Label);
            this.splitContainer.Panel2.Controls.Add(this.author1Label);
            this.splitContainer.Panel2.Controls.Add(this.role1Label);
            this.splitContainer.Panel2.Controls.Add(this.label3);
            this.splitContainer.Panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer.Size = new System.Drawing.Size(827, 464);
            this.splitContainer.SplitterDistance = 642;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkCyan;
            this.panel1.Controls.Add(this.homeButtonPanel);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.installButtonPanel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(1, 360);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(183, 104);
            this.panel1.TabIndex = 30;
            // 
            // homeButtonPanel
            // 
            this.homeButtonPanel.Controls.Add(this.okButton);
            this.homeButtonPanel.Controls.Add(this.versionLabel);
            this.homeButtonPanel.Location = new System.Drawing.Point(33, 5);
            this.homeButtonPanel.Name = "homeButtonPanel";
            this.homeButtonPanel.Size = new System.Drawing.Size(117, 81);
            this.homeButtonPanel.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(128, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 19);
            this.label4.TabIndex = 29;
            this.label4.Text = "Djey";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // installButtonPanel
            // 
            this.installButtonPanel.Controls.Add(this.installerBtn);
            this.installButtonPanel.Controls.Add(this.uninstallButton);
            this.installButtonPanel.Location = new System.Drawing.Point(33, 5);
            this.installButtonPanel.Name = "installButtonPanel";
            this.installButtonPanel.Size = new System.Drawing.Size(117, 81);
            this.installButtonPanel.TabIndex = 9;
            this.installButtonPanel.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gold;
            this.label5.Location = new System.Drawing.Point(17, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 20);
            this.label5.TabIndex = 28;
            this.label5.Text = "Installer program by";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // author8Label
            // 
            this.author8Label.BackColor = System.Drawing.Color.Transparent;
            this.author8Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author8Label.ForeColor = System.Drawing.Color.Black;
            this.author8Label.Location = new System.Drawing.Point(7, 327);
            this.author8Label.Name = "author8Label";
            this.author8Label.Size = new System.Drawing.Size(171, 25);
            this.author8Label.TabIndex = 27;
            this.author8Label.Text = "Author8";
            this.author8Label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // role8Label
            // 
            this.role8Label.BackColor = System.Drawing.Color.Transparent;
            this.role8Label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.role8Label.ForeColor = System.Drawing.Color.Maroon;
            this.role8Label.Location = new System.Drawing.Point(10, 310);
            this.role8Label.Name = "role8Label";
            this.role8Label.Size = new System.Drawing.Size(171, 20);
            this.role8Label.TabIndex = 26;
            this.role8Label.Text = "Role8:";
            this.role8Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // author7Label
            // 
            this.author7Label.BackColor = System.Drawing.Color.Transparent;
            this.author7Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author7Label.ForeColor = System.Drawing.Color.Black;
            this.author7Label.Location = new System.Drawing.Point(7, 288);
            this.author7Label.Name = "author7Label";
            this.author7Label.Size = new System.Drawing.Size(171, 25);
            this.author7Label.TabIndex = 25;
            this.author7Label.Text = "Author7";
            this.author7Label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // role7Label
            // 
            this.role7Label.BackColor = System.Drawing.Color.Transparent;
            this.role7Label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.role7Label.ForeColor = System.Drawing.Color.Maroon;
            this.role7Label.Location = new System.Drawing.Point(10, 271);
            this.role7Label.Name = "role7Label";
            this.role7Label.Size = new System.Drawing.Size(171, 20);
            this.role7Label.TabIndex = 24;
            this.role7Label.Text = "Role7:";
            this.role7Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // author6Label
            // 
            this.author6Label.BackColor = System.Drawing.Color.Transparent;
            this.author6Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author6Label.ForeColor = System.Drawing.Color.Black;
            this.author6Label.Location = new System.Drawing.Point(7, 249);
            this.author6Label.Name = "author6Label";
            this.author6Label.Size = new System.Drawing.Size(171, 25);
            this.author6Label.TabIndex = 23;
            this.author6Label.Text = "Author6";
            this.author6Label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // role6Label
            // 
            this.role6Label.BackColor = System.Drawing.Color.Transparent;
            this.role6Label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.role6Label.ForeColor = System.Drawing.Color.Maroon;
            this.role6Label.Location = new System.Drawing.Point(10, 232);
            this.role6Label.Name = "role6Label";
            this.role6Label.Size = new System.Drawing.Size(171, 20);
            this.role6Label.TabIndex = 22;
            this.role6Label.Text = "Role6:";
            this.role6Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // author5Label
            // 
            this.author5Label.BackColor = System.Drawing.Color.Transparent;
            this.author5Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author5Label.ForeColor = System.Drawing.Color.Black;
            this.author5Label.Location = new System.Drawing.Point(7, 210);
            this.author5Label.Name = "author5Label";
            this.author5Label.Size = new System.Drawing.Size(171, 25);
            this.author5Label.TabIndex = 21;
            this.author5Label.Text = "Author5";
            this.author5Label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // role5Label
            // 
            this.role5Label.BackColor = System.Drawing.Color.Transparent;
            this.role5Label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.role5Label.ForeColor = System.Drawing.Color.Maroon;
            this.role5Label.Location = new System.Drawing.Point(10, 193);
            this.role5Label.Name = "role5Label";
            this.role5Label.Size = new System.Drawing.Size(171, 20);
            this.role5Label.TabIndex = 20;
            this.role5Label.Text = "Role5:";
            this.role5Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // author4Label
            // 
            this.author4Label.BackColor = System.Drawing.Color.Transparent;
            this.author4Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author4Label.ForeColor = System.Drawing.Color.Black;
            this.author4Label.Location = new System.Drawing.Point(7, 171);
            this.author4Label.Name = "author4Label";
            this.author4Label.Size = new System.Drawing.Size(171, 25);
            this.author4Label.TabIndex = 19;
            this.author4Label.Text = "Author4";
            this.author4Label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // role4Label
            // 
            this.role4Label.BackColor = System.Drawing.Color.Transparent;
            this.role4Label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.role4Label.ForeColor = System.Drawing.Color.Maroon;
            this.role4Label.Location = new System.Drawing.Point(10, 154);
            this.role4Label.Name = "role4Label";
            this.role4Label.Size = new System.Drawing.Size(171, 20);
            this.role4Label.TabIndex = 18;
            this.role4Label.Text = "Role4:";
            this.role4Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // author3Label
            // 
            this.author3Label.BackColor = System.Drawing.Color.Transparent;
            this.author3Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author3Label.ForeColor = System.Drawing.Color.Black;
            this.author3Label.Location = new System.Drawing.Point(7, 132);
            this.author3Label.Name = "author3Label";
            this.author3Label.Size = new System.Drawing.Size(171, 25);
            this.author3Label.TabIndex = 17;
            this.author3Label.Text = "Author3";
            this.author3Label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // role3Label
            // 
            this.role3Label.BackColor = System.Drawing.Color.Transparent;
            this.role3Label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.role3Label.ForeColor = System.Drawing.Color.Maroon;
            this.role3Label.Location = new System.Drawing.Point(10, 115);
            this.role3Label.Name = "role3Label";
            this.role3Label.Size = new System.Drawing.Size(171, 20);
            this.role3Label.TabIndex = 16;
            this.role3Label.Text = "Role3:";
            this.role3Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // author2Label
            // 
            this.author2Label.BackColor = System.Drawing.Color.Transparent;
            this.author2Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author2Label.ForeColor = System.Drawing.Color.Black;
            this.author2Label.Location = new System.Drawing.Point(7, 93);
            this.author2Label.Name = "author2Label";
            this.author2Label.Size = new System.Drawing.Size(171, 25);
            this.author2Label.TabIndex = 15;
            this.author2Label.Text = "Author2";
            this.author2Label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // role2Label
            // 
            this.role2Label.BackColor = System.Drawing.Color.Transparent;
            this.role2Label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.role2Label.ForeColor = System.Drawing.Color.Maroon;
            this.role2Label.Location = new System.Drawing.Point(10, 76);
            this.role2Label.Name = "role2Label";
            this.role2Label.Size = new System.Drawing.Size(171, 20);
            this.role2Label.TabIndex = 14;
            this.role2Label.Text = "Role2:";
            this.role2Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // author1Label
            // 
            this.author1Label.BackColor = System.Drawing.Color.Transparent;
            this.author1Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author1Label.ForeColor = System.Drawing.Color.Black;
            this.author1Label.Location = new System.Drawing.Point(7, 54);
            this.author1Label.Name = "author1Label";
            this.author1Label.Size = new System.Drawing.Size(171, 25);
            this.author1Label.TabIndex = 13;
            this.author1Label.Text = "Author1";
            this.author1Label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // role1Label
            // 
            this.role1Label.BackColor = System.Drawing.Color.Transparent;
            this.role1Label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.role1Label.ForeColor = System.Drawing.Color.Maroon;
            this.role1Label.Location = new System.Drawing.Point(10, 37);
            this.role1Label.Name = "role1Label";
            this.role1Label.Size = new System.Drawing.Size(171, 20);
            this.role1Label.TabIndex = 10;
            this.role1Label.Text = "Role1:";
            this.role1Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkCyan;
            this.label3.Location = new System.Drawing.Point(13, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "Credits";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // webBrowser
            // 
            this.webBrowser.AllowWebBrowserDrop = false;
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScrollBarsEnabled = false;
            this.webBrowser.Size = new System.Drawing.Size(642, 360);
            this.webBrowser.TabIndex = 3;
            this.webBrowser.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 464);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TDU Mod And Play! - ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.tableLayoutPanel.ResumeLayout(false);
            this.picPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.progressPanel.ResumeLayout(false);
            this.homePanel.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.homeButtonPanel.ResumeLayout(false);
            this.installButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tduPathTextbox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.Button installerBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel browseTDULink;
        private System.Windows.Forms.Label contribLabel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel picPanel;
        private System.Windows.Forms.ProgressBar mainProgressBar;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Panel homePanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Panel progressPanel;
        private System.Windows.Forms.Label infoLabel;
        public System.Windows.Forms.Button uninstallButton;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel homeButtonPanel;
        private System.Windows.Forms.Panel installButtonPanel;
        private System.Windows.Forms.Label author1Label;
        private System.Windows.Forms.Label role1Label;
        private System.Windows.Forms.Label author3Label;
        private System.Windows.Forms.Label role3Label;
        private System.Windows.Forms.Label author2Label;
        private System.Windows.Forms.Label role2Label;
        private System.Windows.Forms.Label author8Label;
        private System.Windows.Forms.Label role8Label;
        private System.Windows.Forms.Label author7Label;
        private System.Windows.Forms.Label role7Label;
        private System.Windows.Forms.Label author6Label;
        private System.Windows.Forms.Label role6Label;
        private System.Windows.Forms.Label author5Label;
        private System.Windows.Forms.Label role5Label;
        private System.Windows.Forms.Label author4Label;
        private System.Windows.Forms.Label role4Label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label freeLabel;
        private System.Windows.Forms.WebBrowser webBrowser;
    }
}


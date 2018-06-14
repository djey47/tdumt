namespace TDUModdingTools.gui
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
            this.DDS2DBButton = new System.Windows.Forms.Button();
            this.dbResButton = new System.Windows.Forms.Button();
            this.fileBrowserButton = new System.Windows.Forms.Button();
            this.DBDDSButton = new System.Windows.Forms.Button();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuidetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.getUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchEditorButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lineTwoPanel = new System.Windows.Forms.Panel();
            this.launchConfButton = new System.Windows.Forms.Button();
            this.lineOnePanel = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.trackPackButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dbButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.vehicleManagerButton = new System.Windows.Forms.Button();
            this.mainMenuStrip.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.lineTwoPanel.SuspendLayout();
            this.lineOnePanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DDS2DBButton
            // 
            this.DDS2DBButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DDS2DBButton.Location = new System.Drawing.Point(87, 3);
            this.DDS2DBButton.Name = "DDS2DBButton";
            this.DDS2DBButton.Size = new System.Drawing.Size(75, 50);
            this.DDS2DBButton.TabIndex = 1;
            this.DDS2DBButton.Text = "DDS->2DB";
            this.DDS2DBButton.UseVisualStyleBackColor = true;
            this.DDS2DBButton.Click += new System.EventHandler(this.DDS2DBButton_Click);
            // 
            // dbResButton
            // 
            this.dbResButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbResButton.Location = new System.Drawing.Point(87, 3);
            this.dbResButton.Name = "dbResButton";
            this.dbResButton.Size = new System.Drawing.Size(75, 50);
            this.dbResButton.TabIndex = 1;
            this.dbResButton.Text = "DB Resources";
            this.dbResButton.UseVisualStyleBackColor = true;
            this.dbResButton.Visible = false;
            this.dbResButton.Click += new System.EventHandler(this.DBButton_Click);
            // 
            // fileBrowserButton
            // 
            this.fileBrowserButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileBrowserButton.Location = new System.Drawing.Point(3, 3);
            this.fileBrowserButton.Name = "fileBrowserButton";
            this.fileBrowserButton.Size = new System.Drawing.Size(75, 50);
            this.fileBrowserButton.TabIndex = 0;
            this.fileBrowserButton.Text = "File Browser";
            this.fileBrowserButton.UseVisualStyleBackColor = true;
            this.fileBrowserButton.Click += new System.EventHandler(this.fileBrowserButton_Click);
            // 
            // DBDDSButton
            // 
            this.DBDDSButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DBDDSButton.Location = new System.Drawing.Point(3, 3);
            this.DBDDSButton.Name = "DBDDSButton";
            this.DBDDSButton.Size = new System.Drawing.Size(75, 50);
            this.DBDDSButton.TabIndex = 0;
            this.DBDDSButton.Text = "2DB->DDS";
            this.DBDDSButton.UseVisualStyleBackColor = true;
            this.DBDDSButton.Click += new System.EventHandler(this.DBDDSButton_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(501, 24);
            this.mainMenuStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(128, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Checked = true;
            this.viewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.basicToolStripMenuItem,
            this.advancedToolStripMenuItem});
            this.viewToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // basicToolStripMenuItem
            // 
            this.basicToolStripMenuItem.Checked = true;
            this.basicToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.basicToolStripMenuItem.Name = "basicToolStripMenuItem";
            this.basicToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.basicToolStripMenuItem.Text = "Basic";
            this.basicToolStripMenuItem.Click += new System.EventHandler(this.basicToolStripMenuItem_Click);
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.advancedToolStripMenuItem.Text = "Advanced";
            this.advancedToolStripMenuItem.Click += new System.EventHandler(this.advancedToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userGuidetoolStripMenuItem,
            this.toolStripSeparator2,
            this.getUpdatesToolStripMenuItem,
            this.toolStripSeparator3,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // userGuidetoolStripMenuItem
            // 
            this.userGuidetoolStripMenuItem.Name = "userGuidetoolStripMenuItem";
            this.userGuidetoolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.userGuidetoolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.userGuidetoolStripMenuItem.Text = "Download modding documentation (PDF)";
            this.userGuidetoolStripMenuItem.Click += new System.EventHandler(this.userGuidetoolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(318, 6);
            // 
            // getUpdatesToolStripMenuItem
            // 
            this.getUpdatesToolStripMenuItem.Name = "getUpdatesToolStripMenuItem";
            this.getUpdatesToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.getUpdatesToolStripMenuItem.Text = "Get updates (web site)";
            this.getUpdatesToolStripMenuItem.Click += new System.EventHandler(this.getUpdatesToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(318, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Enabled = false;
            this.windowToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.windowToolStripMenuItem.Text = "Window";
            this.windowToolStripMenuItem.Visible = false;
            // 
            // patchEditorButton
            // 
            this.patchEditorButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patchEditorButton.Location = new System.Drawing.Point(87, 62);
            this.patchEditorButton.Name = "patchEditorButton";
            this.patchEditorButton.Size = new System.Drawing.Size(75, 50);
            this.patchEditorButton.TabIndex = 0;
            this.patchEditorButton.Text = "Patches";
            this.patchEditorButton.UseVisualStyleBackColor = true;
            this.patchEditorButton.Click += new System.EventHandler(this.patchEditorButton_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.lineTwoPanel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.lineOnePanel, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 286F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(501, 327);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // lineTwoPanel
            // 
            this.lineTwoPanel.Controls.Add(this.launchConfButton);
            this.lineTwoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineTwoPanel.Location = new System.Drawing.Point(4, 291);
            this.lineTwoPanel.Name = "lineTwoPanel";
            this.lineTwoPanel.Size = new System.Drawing.Size(493, 33);
            this.lineTwoPanel.TabIndex = 2;
            // 
            // launchConfButton
            // 
            this.launchConfButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.launchConfButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.launchConfButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.launchConfButton.Image = global::TDUModdingTools.Properties.Resources.arrow_down_16;
            this.launchConfButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.launchConfButton.Location = new System.Drawing.Point(0, 0);
            this.launchConfButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.launchConfButton.Name = "launchConfButton";
            this.launchConfButton.Size = new System.Drawing.Size(493, 33);
            this.launchConfButton.TabIndex = 0;
            this.launchConfButton.Text = "Launch Test Drive Unlimited ! ";
            this.launchConfButton.UseVisualStyleBackColor = true;
            this.launchConfButton.Click += new System.EventHandler(this.launchConfButton_Click);
            // 
            // lineOnePanel
            // 
            this.lineOnePanel.BackColor = System.Drawing.SystemColors.Control;
            this.lineOnePanel.Controls.Add(this.panel4);
            this.lineOnePanel.Controls.Add(this.panel3);
            this.lineOnePanel.Controls.Add(this.panel2);
            this.lineOnePanel.Controls.Add(this.panel1);
            this.lineOnePanel.Controls.Add(this.vehicleManagerButton);
            this.lineOnePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineOnePanel.Location = new System.Drawing.Point(4, 4);
            this.lineOnePanel.Name = "lineOnePanel";
            this.lineOnePanel.Size = new System.Drawing.Size(493, 280);
            this.lineOnePanel.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.patchEditorButton);
            this.panel4.Location = new System.Drawing.Point(311, 147);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(167, 117);
            this.panel4.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(16, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "Publish";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.DDS2DBButton);
            this.panel3.Controls.Add(this.DBDDSButton);
            this.panel3.Location = new System.Drawing.Point(311, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(167, 117);
            this.panel3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(14, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Graphics";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.trackPackButton);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(14, 147);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(167, 117);
            this.panel2.TabIndex = 3;
            // 
            // trackPackButton
            // 
            this.trackPackButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackPackButton.Location = new System.Drawing.Point(3, 62);
            this.trackPackButton.Name = "trackPackButton";
            this.trackPackButton.Size = new System.Drawing.Size(75, 50);
            this.trackPackButton.TabIndex = 1;
            this.trackPackButton.Text = "Track Pack";
            this.trackPackButton.UseVisualStyleBackColor = true;
            this.trackPackButton.Click += new System.EventHandler(this.trackPackButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(96, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Wizards";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dbButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dbResButton);
            this.panel1.Controls.Add(this.fileBrowserButton);
            this.panel1.Location = new System.Drawing.Point(14, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(167, 117);
            this.panel1.TabIndex = 1;
            // 
            // dbButton
            // 
            this.dbButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbButton.Location = new System.Drawing.Point(3, 62);
            this.dbButton.Name = "dbButton";
            this.dbButton.Size = new System.Drawing.Size(75, 50);
            this.dbButton.TabIndex = 2;
            this.dbButton.Text = "Database";
            this.dbButton.UseVisualStyleBackColor = true;
            this.dbButton.Visible = false;
            this.dbButton.Click += new System.EventHandler(this.dbButton_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(104, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Main";
            // 
            // vehicleManagerButton
            // 
            this.vehicleManagerButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vehicleManagerButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vehicleManagerButton.Location = new System.Drawing.Point(196, 89);
            this.vehicleManagerButton.Name = "vehicleManagerButton";
            this.vehicleManagerButton.Size = new System.Drawing.Size(100, 100);
            this.vehicleManagerButton.TabIndex = 0;
            this.vehicleManagerButton.Text = "Vehicle Manager";
            this.vehicleManagerButton.UseVisualStyleBackColor = true;
            this.vehicleManagerButton.Click += new System.EventHandler(this.vehicleManagerButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 351);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TDU Modding Tools";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainForm_FormClosed);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.lineTwoPanel.ResumeLayout(false);
            this.lineOnePanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DDS2DBButton;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button DBDDSButton;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem basicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button fileBrowserButton;
        private System.Windows.Forms.ToolStripMenuItem userGuidetoolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button dbResButton;
        private System.Windows.Forms.Button launchConfButton;
        private System.Windows.Forms.Button patchEditorButton;
        private System.Windows.Forms.ToolStripMenuItem getUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Panel lineOnePanel;
        private System.Windows.Forms.Panel lineTwoPanel;
        private System.Windows.Forms.Button vehicleManagerButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button trackPackButton;
        private System.Windows.Forms.Button dbButton;
    }
}


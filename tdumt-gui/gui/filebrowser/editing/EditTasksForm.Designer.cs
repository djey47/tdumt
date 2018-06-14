namespace TDUModdingTools.gui.filebrowser.editing
{
    partial class EditTasksForm
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
            this.applyButton = new System.Windows.Forms.Button();
            this.discardButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.folderButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.editTasksListView = new System.Windows.Forms.ListView();
            this.packedColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.typeColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.dateColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.lastDateColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.bnkColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.fileColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.propertiesButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyButton.ForeColor = System.Drawing.Color.SeaGreen;
            this.applyButton.Location = new System.Drawing.Point(440, 255);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(68, 31);
            this.applyButton.TabIndex = 4;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // discardButton
            // 
            this.discardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.discardButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.discardButton.ForeColor = System.Drawing.Color.DarkRed;
            this.discardButton.Location = new System.Drawing.Point(514, 255);
            this.discardButton.Name = "discardButton";
            this.discardButton.Size = new System.Drawing.Size(68, 31);
            this.discardButton.TabIndex = 5;
            this.discardButton.Text = "Discard";
            this.discardButton.UseVisualStyleBackColor = true;
            this.discardButton.Click += new System.EventHandler(this.discardButton_Click);
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editButton.Location = new System.Drawing.Point(102, 255);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(91, 31);
            this.editButton.TabIndex = 2;
            this.editButton.Text = "Open file";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // folderButton
            // 
            this.folderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.folderButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderButton.Location = new System.Drawing.Point(5, 255);
            this.folderButton.Name = "folderButton";
            this.folderButton.Size = new System.Drawing.Size(91, 31);
            this.folderButton.TabIndex = 1;
            this.folderButton.Text = "Open location";
            this.folderButton.UseVisualStyleBackColor = true;
            this.folderButton.Click += new System.EventHandler(this.folderButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.editTasksListView);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 245);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Currently editing";
            // 
            // editTasksListView
            // 
            this.editTasksListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editTasksListView.CheckBoxes = true;
            this.editTasksListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.packedColumnHeader,
            this.typeColumnHeader,
            this.dateColumnHeader,
            this.lastDateColumnHeader,
            this.bnkColumnHeader,
            this.fileColumnHeader});
            this.editTasksListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editTasksListView.FullRowSelect = true;
            this.editTasksListView.GridLines = true;
            this.editTasksListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.editTasksListView.HideSelection = false;
            this.editTasksListView.Location = new System.Drawing.Point(6, 21);
            this.editTasksListView.MultiSelect = false;
            this.editTasksListView.Name = "editTasksListView";
            this.editTasksListView.ShowItemToolTips = true;
            this.editTasksListView.Size = new System.Drawing.Size(571, 218);
            this.editTasksListView.TabIndex = 0;
            this.editTasksListView.UseCompatibleStateImageBehavior = false;
            this.editTasksListView.View = System.Windows.Forms.View.Details;
            this.editTasksListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.editTasksListView_MouseDoubleClick);
            // 
            // packedColumnHeader
            // 
            this.packedColumnHeader.Text = "File";
            this.packedColumnHeader.Width = 150;
            // 
            // typeColumnHeader
            // 
            this.typeColumnHeader.Text = "?";
            this.typeColumnHeader.Width = 175;
            // 
            // dateColumnHeader
            // 
            this.dateColumnHeader.Text = "Started";
            this.dateColumnHeader.Width = 125;
            // 
            // lastDateColumnHeader
            // 
            this.lastDateColumnHeader.Text = "Last modified";
            this.lastDateColumnHeader.Width = 125;
            // 
            // bnkColumnHeader
            // 
            this.bnkColumnHeader.Text = "Parent BNK";
            this.bnkColumnHeader.Width = 350;
            // 
            // fileColumnHeader
            // 
            this.fileColumnHeader.Text = "Working file";
            this.fileColumnHeader.Width = 400;
            // 
            // propertiesButton
            // 
            this.propertiesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.propertiesButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertiesButton.Location = new System.Drawing.Point(199, 255);
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(91, 31);
            this.propertiesButton.TabIndex = 3;
            this.propertiesButton.Text = "Properties...";
            this.propertiesButton.UseVisualStyleBackColor = true;
            this.propertiesButton.Click += new System.EventHandler(this.propertiesButton_Click);
            // 
            // EditTasksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 298);
            this.Controls.Add(this.propertiesButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.folderButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.discardButton);
            this.Controls.Add(this.applyButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditTasksForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Tasks";
            this.VisibleChanged += new System.EventHandler(this.EditTasksForm_VisibleChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditTasksForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button discardButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button folderButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView editTasksListView;
        private System.Windows.Forms.ColumnHeader packedColumnHeader;
        private System.Windows.Forms.ColumnHeader typeColumnHeader;
        private System.Windows.Forms.ColumnHeader dateColumnHeader;
        private System.Windows.Forms.ColumnHeader bnkColumnHeader;
        private System.Windows.Forms.ColumnHeader fileColumnHeader;
        private System.Windows.Forms.ColumnHeader lastDateColumnHeader;
        private System.Windows.Forms.Button propertiesButton;
    }
}
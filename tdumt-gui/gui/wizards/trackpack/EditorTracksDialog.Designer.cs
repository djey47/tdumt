namespace TDUModdingTools.gui.wizards.trackpack
{
    partial class EditorTracksDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorTracksDialog));
            this.idColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.briefingColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.okToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.igeListView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.profileName = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // idColumnHeader
            // 
            this.idColumnHeader.Text = "#";
            this.idColumnHeader.Width = 50;
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 300;
            // 
            // briefingColumnHeader
            // 
            this.briefingColumnHeader.Text = "Briefing";
            this.briefingColumnHeader.Width = 300;
            // 
            // fileColumnHeader
            // 
            this.fileColumnHeader.Text = "File name";
            this.fileColumnHeader.Width = 300;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.okToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 360);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "originalToolStrip";
            // 
            // okToolStripButton
            // 
            this.okToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.okToolStripButton.AutoToolTip = false;
            this.okToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.okToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("okToolStripButton.Image")));
            this.okToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.okToolStripButton.Name = "okToolStripButton";
            this.okToolStripButton.Size = new System.Drawing.Size(28, 22);
            this.okToolStripButton.Text = "OK";
            this.okToolStripButton.Click += new System.EventHandler(this.okToolStripButton_Click);
            // 
            // igeListView
            // 
            this.igeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.igeListView.CheckBoxes = true;
            this.igeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idColumnHeader,
            this.nameColumnHeader,
            this.briefingColumnHeader,
            this.fileColumnHeader});
            this.igeListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.igeListView.GridLines = true;
            this.igeListView.Location = new System.Drawing.Point(12, 26);
            this.igeListView.Name = "igeListView";
            this.igeListView.Size = new System.Drawing.Size(984, 331);
            this.igeListView.TabIndex = 1;
            this.igeListView.UseCompatibleStateImageBehavior = false;
            this.igeListView.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Import from TDU editor...";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(408, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Player profile: ";
            // 
            // profileName
            // 
            this.profileName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.profileName.AutoSize = true;
            this.profileName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.profileName.Location = new System.Drawing.Point(509, 9);
            this.profileName.Name = "profileName";
            this.profileName.Size = new System.Drawing.Size(103, 14);
            this.profileName.TabIndex = 5;
            this.profileName.Text = "<profile name>";
            // 
            // EditorTracksDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 385);
            this.Controls.Add(this.profileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.igeListView);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "EditorTracksDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editor tracks";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader idColumnHeader;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader briefingColumnHeader;
        private System.Windows.Forms.ColumnHeader fileColumnHeader;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton okToolStripButton;
        private System.Windows.Forms.ListView igeListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label profileName;
    }
}
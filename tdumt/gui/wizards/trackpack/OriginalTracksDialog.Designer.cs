namespace TDUModdingTools.gui.wizards.trackpack
{
    partial class OriginalTracksDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OriginalTracksDialog));
            this.dfeListView = new System.Windows.Forms.ListView();
            this.idColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.igNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gameModeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gametypeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.searchToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.okToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dfeListView
            // 
            this.dfeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dfeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idColumnHeader,
            this.nameColumnHeader,
            this.igNameColumnHeader,
            this.gameModeColumnHeader,
            this.gametypeColumnHeader,
            this.fileColumnHeader});
            this.dfeListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dfeListView.FullRowSelect = true;
            this.dfeListView.GridLines = true;
            this.dfeListView.Location = new System.Drawing.Point(12, 12);
            this.dfeListView.Name = "dfeListView";
            this.dfeListView.Size = new System.Drawing.Size(984, 345);
            this.dfeListView.TabIndex = 1;
            this.dfeListView.UseCompatibleStateImageBehavior = false;
            this.dfeListView.View = System.Windows.Forms.View.Details;
            // 
            // idColumnHeader
            // 
            this.idColumnHeader.Text = "#";
            this.idColumnHeader.Width = 50;
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Original name";
            this.nameColumnHeader.Width = 300;
            // 
            // igNameColumnHeader
            // 
            this.igNameColumnHeader.Text = "Ingame name";
            this.igNameColumnHeader.Width = 300;
            // 
            // gameModeColumnHeader
            // 
            this.gameModeColumnHeader.Text = "Game mode";
            this.gameModeColumnHeader.Width = 100;
            // 
            // gametypeColumnHeader
            // 
            this.gametypeColumnHeader.Text = "Game type";
            this.gametypeColumnHeader.Width = 100;
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
            this.searchToolStripButton,
            this.okToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 360);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "originalToolStrip";
            // 
            // searchToolStripButton
            // 
            this.searchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.searchToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("searchToolStripButton.Image")));
            this.searchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchToolStripButton.Name = "searchToolStripButton";
            this.searchToolStripButton.Size = new System.Drawing.Size(60, 22);
            this.searchToolStripButton.Text = "Search...";
            this.searchToolStripButton.Click += new System.EventHandler(this.searchToolStripButton_Click);
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
            // OriginalTracksDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 385);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dfeListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "OriginalTracksDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Original tracks";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OriginalTracksDialog_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView dfeListView;
        private System.Windows.Forms.ColumnHeader idColumnHeader;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader igNameColumnHeader;
        private System.Windows.Forms.ColumnHeader gameModeColumnHeader;
        private System.Windows.Forms.ColumnHeader gametypeColumnHeader;
        private System.Windows.Forms.ColumnHeader fileColumnHeader;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton searchToolStripButton;
        private System.Windows.Forms.ToolStripButton okToolStripButton;
    }
}
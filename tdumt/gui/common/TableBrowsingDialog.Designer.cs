namespace TDUModdingTools.gui.common
{
    partial class TableBrowsingDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableBrowsingDialog));
            this.messageLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.searchToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.getToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.valueListView = new System.Windows.Forms.ListView();
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // messageLabel
            // 
            this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.messageLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.Location = new System.Drawing.Point(12, 9);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(304, 35);
            this.messageLabel.TabIndex = 0;
            this.messageLabel.Text = "<Message>";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripButton,
            this.toolStripSeparator1,
            this.getToolStripButton,
            this.addToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 324);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(328, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // searchToolStripButton
            // 
            this.searchToolStripButton.AutoToolTip = false;
            this.searchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.searchToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("searchToolStripButton.Image")));
            this.searchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchToolStripButton.Name = "searchToolStripButton";
            this.searchToolStripButton.Size = new System.Drawing.Size(60, 22);
            this.searchToolStripButton.Text = "Search...";
            this.searchToolStripButton.ToolTipText = "Retrieves a value in the list";
            this.searchToolStripButton.Click += new System.EventHandler(this.searchToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // getToolStripButton
            // 
            this.getToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.getToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.getToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("getToolStripButton.Image")));
            this.getToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.getToolStripButton.Name = "getToolStripButton";
            this.getToolStripButton.Size = new System.Drawing.Size(28, 22);
            this.getToolStripButton.Text = "OK";
            this.getToolStripButton.ToolTipText = "Uses selected value";
            this.getToolStripButton.Click += new System.EventHandler(this.getToolStripButton_Click);
            // 
            // valueListView
            // 
            this.valueListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.valueListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.valueColumnHeader});
            this.valueListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueListView.FullRowSelect = true;
            this.valueListView.GridLines = true;
            this.valueListView.HideSelection = false;
            this.valueListView.Location = new System.Drawing.Point(0, 47);
            this.valueListView.MultiSelect = false;
            this.valueListView.Name = "valueListView";
            this.valueListView.Size = new System.Drawing.Size(328, 274);
            this.valueListView.TabIndex = 1;
            this.valueListView.UseCompatibleStateImageBehavior = false;
            this.valueListView.View = System.Windows.Forms.View.Details;
            this.valueListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.valueListView_KeyDown);
            this.valueListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.valueListView_MouseDoubleClick);
            // 
            // valueColumnHeader
            // 
            this.valueColumnHeader.Text = "Value";
            this.valueColumnHeader.Width = 800;
            // 
            // addToolStripButton
            // 
            this.addToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("addToolStripButton.Image")));
            this.addToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addToolStripButton.Name = "addToolStripButton";
            this.addToolStripButton.Size = new System.Drawing.Size(45, 22);
            this.addToolStripButton.Text = "Add...";
            this.addToolStripButton.ToolTipText = "Provides new values to select";
            this.addToolStripButton.Click += new System.EventHandler(this.addToolStripButton_Click);
            // 
            // TableBrowsingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 349);
            this.Controls.Add(this.valueListView);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.messageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TableBrowsingDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Database Browser...";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton searchToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton getToolStripButton;
        private System.Windows.Forms.ListView valueListView;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private System.Windows.Forms.ToolStripButton addToolStripButton;
    }
}
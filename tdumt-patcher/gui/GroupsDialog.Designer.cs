namespace TDUModAndPlay.gui
{
    partial class GroupsDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.installButton = new System.Windows.Forms.Button();
            this.groupListView = new System.Windows.Forms.ListView();
            this.groupColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(25, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select which components to install...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.backButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.backButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.backButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.ForeColor = System.Drawing.Color.Black;
            this.backButton.Location = new System.Drawing.Point(22, 11);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(120, 39);
            this.backButton.TabIndex = 5;
            this.backButton.Text = "BACK";
            this.backButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // installButton
            // 
            this.installButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.installButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.installButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.installButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installButton.ForeColor = System.Drawing.Color.DarkGreen;
            this.installButton.Image = global::TDUModAndPlay.Properties.Resources.arrow_forward_32;
            this.installButton.Location = new System.Drawing.Point(162, 11);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(120, 39);
            this.installButton.TabIndex = 4;
            this.installButton.Text = "INSTALL";
            this.installButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // groupListView
            // 
            this.groupListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.groupListView.CheckBoxes = true;
            this.groupListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.groupColumnHeader});
            this.groupListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupListView.HoverSelection = true;
            this.groupListView.Location = new System.Drawing.Point(12, 36);
            this.groupListView.MultiSelect = false;
            this.groupListView.Name = "groupListView";
            this.groupListView.Size = new System.Drawing.Size(280, 173);
            this.groupListView.TabIndex = 1;
            this.groupListView.UseCompatibleStateImageBehavior = false;
            this.groupListView.View = System.Windows.Forms.View.List;
            this.groupListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.groupListView_ItemCheck);
            // 
            // groupColumnHeader
            // 
            this.groupColumnHeader.Width = 260;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkCyan;
            this.panel1.Controls.Add(this.installButton);
            this.panel1.Controls.Add(this.backButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 226);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(304, 60);
            this.panel1.TabIndex = 6;
            // 
            // GroupsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(304, 286);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "<Mod name here>";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView groupListView;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.ColumnHeader groupColumnHeader;
        private System.Windows.Forms.Panel panel1;
    }
}
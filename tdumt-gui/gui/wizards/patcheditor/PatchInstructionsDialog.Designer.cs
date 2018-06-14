namespace TDUModdingTools.gui.wizards.patcheditor
{
    partial class PatchInstructionsDialog
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.instructionListView = new System.Windows.Forms.ListView();
            this.numColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.actionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.commentColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.helpColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.customLabel = new System.Windows.Forms.Label();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(547, 341);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(466, 341);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // instructionListView
            // 
            this.instructionListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.instructionListView.CheckBoxes = true;
            this.instructionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.numColumnHeader,
            this.actionColumnHeader,
            this.groupColumnHeader,
            this.commentColumnHeader,
            this.helpColumnHeader});
            this.instructionListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionListView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.instructionListView.GridLines = true;
            this.instructionListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.instructionListView.HideSelection = false;
            this.instructionListView.Location = new System.Drawing.Point(12, 39);
            this.instructionListView.Name = "instructionListView";
            this.instructionListView.Size = new System.Drawing.Size(610, 294);
            this.instructionListView.TabIndex = 1;
            this.instructionListView.UseCompatibleStateImageBehavior = false;
            this.instructionListView.View = System.Windows.Forms.View.Details;
            // 
            // numColumnHeader
            // 
            this.numColumnHeader.Text = "#";
            this.numColumnHeader.Width = 50;
            // 
            // actionColumnHeader
            // 
            this.actionColumnHeader.Text = "Action";
            this.actionColumnHeader.Width = 150;
            // 
            // groupColumnHeader
            // 
            this.groupColumnHeader.Text = "Group";
            this.groupColumnHeader.Width = 150;
            // 
            // commentColumnHeader
            // 
            this.commentColumnHeader.Text = "Comment";
            this.commentColumnHeader.Width = 275;
            // 
            // helpColumnHeader
            // 
            this.helpColumnHeader.Text = "?";
            this.helpColumnHeader.Width = 275;
            // 
            // customLabel
            // 
            this.customLabel.AutoSize = true;
            this.customLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel.Location = new System.Drawing.Point(9, 9);
            this.customLabel.Name = "customLabel";
            this.customLabel.Size = new System.Drawing.Size(123, 14);
            this.customLabel.TabIndex = 0;
            this.customLabel.Text = "<Custom label here>";
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileNameLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.fileNameLabel.Location = new System.Drawing.Point(12, 345);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(130, 14);
            this.fileNameLabel.TabIndex = 2;
            this.fileNameLabel.Text = "<patch name here>";
            // 
            // PatchInstructionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 376);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.customLabel);
            this.Controls.Add(this.instructionListView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatchInstructionsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patch instructions...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ListView instructionListView;
        private System.Windows.Forms.ColumnHeader numColumnHeader;
        private System.Windows.Forms.ColumnHeader actionColumnHeader;
        private System.Windows.Forms.ColumnHeader groupColumnHeader;
        private System.Windows.Forms.ColumnHeader commentColumnHeader;
        private System.Windows.Forms.Label customLabel;
        private System.Windows.Forms.ColumnHeader helpColumnHeader;
        private System.Windows.Forms.Label fileNameLabel;
    }
}
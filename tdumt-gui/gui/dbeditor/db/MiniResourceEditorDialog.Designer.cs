namespace TDUModdingTools.gui.dbeditor.db
{
    partial class MiniResourceEditorDialog
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
            this.selectResourceRadioButton = new System.Windows.Forms.RadioButton();
            this.modifyResourceRadioButton = new System.Windows.Forms.RadioButton();
            this.resourceInfoLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.resourceBrowseButton = new System.Windows.Forms.Button();
            this.resourceValueTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // selectResourceRadioButton
            // 
            this.selectResourceRadioButton.AutoSize = true;
            this.selectResourceRadioButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectResourceRadioButton.Location = new System.Drawing.Point(14, 67);
            this.selectResourceRadioButton.Name = "selectResourceRadioButton";
            this.selectResourceRadioButton.Size = new System.Drawing.Size(249, 18);
            this.selectResourceRadioButton.TabIndex = 1;
            this.selectResourceRadioButton.Text = "Select another resource from same topic";
            this.selectResourceRadioButton.UseVisualStyleBackColor = true;
            this.selectResourceRadioButton.CheckedChanged += new System.EventHandler(this.selectResourceRadioButton_CheckedChanged);
            // 
            // modifyResourceRadioButton
            // 
            this.modifyResourceRadioButton.AutoSize = true;
            this.modifyResourceRadioButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifyResourceRadioButton.Location = new System.Drawing.Point(14, 99);
            this.modifyResourceRadioButton.Name = "modifyResourceRadioButton";
            this.modifyResourceRadioButton.Size = new System.Drawing.Size(191, 18);
            this.modifyResourceRadioButton.TabIndex = 3;
            this.modifyResourceRadioButton.Text = "Modify current resource value:";
            this.modifyResourceRadioButton.UseVisualStyleBackColor = true;
            this.modifyResourceRadioButton.CheckedChanged += new System.EventHandler(this.selectResourceRadioButton_CheckedChanged);
            // 
            // resourceInfoLabel
            // 
            this.resourceInfoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resourceInfoLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resourceInfoLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.resourceInfoLabel.Location = new System.Drawing.Point(12, 9);
            this.resourceInfoLabel.Name = "resourceInfoLabel";
            this.resourceInfoLabel.Size = new System.Drawing.Size(336, 52);
            this.resourceInfoLabel.TabIndex = 0;
            this.resourceInfoLabel.Text = "<Resource information>";
            this.resourceInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(298, 160);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(50, 31);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // resourceBrowseButton
            // 
            this.resourceBrowseButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resourceBrowseButton.Location = new System.Drawing.Point(269, 64);
            this.resourceBrowseButton.Name = "resourceBrowseButton";
            this.resourceBrowseButton.Size = new System.Drawing.Size(27, 25);
            this.resourceBrowseButton.TabIndex = 2;
            this.resourceBrowseButton.Text = "...";
            this.resourceBrowseButton.UseVisualStyleBackColor = true;
            this.resourceBrowseButton.Click += new System.EventHandler(this.resourceBrowseButton_Click);
            // 
            // resourceValueTextBox
            // 
            this.resourceValueTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resourceValueTextBox.Location = new System.Drawing.Point(24, 123);
            this.resourceValueTextBox.Name = "resourceValueTextBox";
            this.resourceValueTextBox.Size = new System.Drawing.Size(313, 22);
            this.resourceValueTextBox.TabIndex = 4;
            // 
            // MiniResourceEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 203);
            this.Controls.Add(this.resourceValueTextBox);
            this.Controls.Add(this.resourceBrowseButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.resourceInfoLabel);
            this.Controls.Add(this.modifyResourceRadioButton);
            this.Controls.Add(this.selectResourceRadioButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MiniResourceEditorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modify resource...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton selectResourceRadioButton;
        private System.Windows.Forms.RadioButton modifyResourceRadioButton;
        private System.Windows.Forms.Label resourceInfoLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button resourceBrowseButton;
        private System.Windows.Forms.TextBox resourceValueTextBox;
    }
}
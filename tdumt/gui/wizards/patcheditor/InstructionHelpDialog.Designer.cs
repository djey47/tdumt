namespace TDUModdingTools.gui.wizards.patcheditor
{
    partial class InstructionHelpDialog
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
            this.instructionDescriptionHelpTextBox = new System.Windows.Forms.TextBox();
            this.nameGroupBox = new System.Windows.Forms.GroupBox();
            this.nameGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // instructionDescriptionHelpTextBox
            // 
            this.instructionDescriptionHelpTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.instructionDescriptionHelpTextBox.BackColor = System.Drawing.Color.White;
            this.instructionDescriptionHelpTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.instructionDescriptionHelpTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.instructionDescriptionHelpTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionDescriptionHelpTextBox.Location = new System.Drawing.Point(11, 17);
            this.instructionDescriptionHelpTextBox.Multiline = true;
            this.instructionDescriptionHelpTextBox.Name = "instructionDescriptionHelpTextBox";
            this.instructionDescriptionHelpTextBox.ReadOnly = true;
            this.instructionDescriptionHelpTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.instructionDescriptionHelpTextBox.Size = new System.Drawing.Size(260, 137);
            this.instructionDescriptionHelpTextBox.TabIndex = 1;
            this.instructionDescriptionHelpTextBox.TabStop = false;
            this.instructionDescriptionHelpTextBox.Text = "<instruction description>";
            // 
            // nameGroupBox
            // 
            this.nameGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nameGroupBox.Controls.Add(this.instructionDescriptionHelpTextBox);
            this.nameGroupBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameGroupBox.Location = new System.Drawing.Point(3, 2);
            this.nameGroupBox.Name = "nameGroupBox";
            this.nameGroupBox.Size = new System.Drawing.Size(277, 160);
            this.nameGroupBox.TabIndex = 0;
            this.nameGroupBox.TabStop = false;
            this.nameGroupBox.Text = "<instruction name>";
            // 
            // InstructionHelpDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(284, 166);
            this.Controls.Add(this.nameGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "InstructionHelpDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Help!";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InstructionHelpDialog_FormClosing);
            this.nameGroupBox.ResumeLayout(false);
            this.nameGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox instructionDescriptionHelpTextBox;
        private System.Windows.Forms.GroupBox nameGroupBox;
    }
}
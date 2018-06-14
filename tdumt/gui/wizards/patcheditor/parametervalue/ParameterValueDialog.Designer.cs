namespace TDUModdingTools.gui.wizards.patcheditor.parametervalue
{
    partial class ParameterValueDialog
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
            this.insertVariableLinkLabel = new System.Windows.Forms.LinkLabel();
            this.knownValuesLinkLabel = new System.Windows.Forms.LinkLabel();
            this.insertTabLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // customLabel
            // 
            this.customLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.customLabel.Location = new System.Drawing.Point(220, 9);
            this.customLabel.Size = new System.Drawing.Size(274, 35);
            // 
            // promptTextBox
            // 
            this.promptTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.promptTextBox.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.promptTextBox.Multiline = true;
            this.promptTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.promptTextBox.Size = new System.Drawing.Size(687, 62);
            this.promptTextBox.WordWrap = false;
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.okButton.Location = new System.Drawing.Point(312, 130);
            this.okButton.Size = new System.Drawing.Size(91, 38);
            // 
            // insertVariableLinkLabel
            // 
            this.insertVariableLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.insertVariableLinkLabel.AutoSize = true;
            this.insertVariableLinkLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.insertVariableLinkLabel.Location = new System.Drawing.Point(103, 112);
            this.insertVariableLinkLabel.Name = "insertVariableLinkLabel";
            this.insertVariableLinkLabel.Size = new System.Drawing.Size(77, 13);
            this.insertVariableLinkLabel.TabIndex = 5;
            this.insertVariableLinkLabel.TabStop = true;
            this.insertVariableLinkLabel.Text = "Insert variable";
            this.insertVariableLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.insertVariableLinkLabel_LinkClicked);
            // 
            // knownValuesLinkLabel
            // 
            this.knownValuesLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.knownValuesLinkLabel.AutoSize = true;
            this.knownValuesLinkLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.knownValuesLinkLabel.Location = new System.Drawing.Point(12, 112);
            this.knownValuesLinkLabel.Name = "knownValuesLinkLabel";
            this.knownValuesLinkLabel.Size = new System.Drawing.Size(85, 13);
            this.knownValuesLinkLabel.TabIndex = 6;
            this.knownValuesLinkLabel.TabStop = true;
            this.knownValuesLinkLabel.Text = "Known values...";
            this.knownValuesLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.knownValuesLinkLabel_LinkClicked);
            // 
            // insertTabLinkLabel
            // 
            this.insertTabLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.insertTabLinkLabel.AutoSize = true;
            this.insertTabLinkLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.insertTabLinkLabel.Location = new System.Drawing.Point(186, 112);
            this.insertTabLinkLabel.Name = "insertTabLinkLabel";
            this.insertTabLinkLabel.Size = new System.Drawing.Size(55, 13);
            this.insertTabLinkLabel.TabIndex = 7;
            this.insertTabLinkLabel.TabStop = true;
            this.insertTabLinkLabel.Text = "Insert tab";
            this.insertTabLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.insertTabLinkLabel_LinkClicked);
            // 
            // ParameterValueDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(714, 180);
            this.Controls.Add(this.insertTabLinkLabel);
            this.Controls.Add(this.knownValuesLinkLabel);
            this.Controls.Add(this.insertVariableLinkLabel);
            this.Name = "ParameterValueDialog";
            this.Controls.SetChildIndex(this.customLabel, 0);
            this.Controls.SetChildIndex(this.insertVariableLinkLabel, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.promptTextBox, 0);
            this.Controls.SetChildIndex(this.knownValuesLinkLabel, 0);
            this.Controls.SetChildIndex(this.insertTabLinkLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel insertVariableLinkLabel;
        private System.Windows.Forms.LinkLabel knownValuesLinkLabel;
        private System.Windows.Forms.LinkLabel insertTabLinkLabel;
    }
}
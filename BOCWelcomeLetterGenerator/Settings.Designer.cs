namespace BOCWelcomeLetterGenerator {
    partial class SettingsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.emailInclusionCheckBox = new System.Windows.Forms.CheckBox();
            this.openFileCheckBox = new System.Windows.Forms.CheckBox();
            this.branchSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // emailInclusionCheckBox
            // 
            this.emailInclusionCheckBox.AutoSize = true;
            this.emailInclusionCheckBox.Location = new System.Drawing.Point(43, 128);
            this.emailInclusionCheckBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.emailInclusionCheckBox.Name = "emailInclusionCheckBox";
            this.emailInclusionCheckBox.Size = new System.Drawing.Size(618, 35);
            this.emailInclusionCheckBox.TabIndex = 0;
            this.emailInclusionCheckBox.Text = "Include Branch Email in Welcome Letter by Default";
            this.emailInclusionCheckBox.UseVisualStyleBackColor = true;
            // 
            // openFileCheckBox
            // 
            this.openFileCheckBox.AutoSize = true;
            this.openFileCheckBox.Location = new System.Drawing.Point(43, 173);
            this.openFileCheckBox.Margin = new System.Windows.Forms.Padding(5);
            this.openFileCheckBox.Name = "openFileCheckBox";
            this.openFileCheckBox.Size = new System.Drawing.Size(566, 35);
            this.openFileCheckBox.TabIndex = 0;
            this.openFileCheckBox.Text = "Automatically Open Generated File by Default";
            this.openFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // branchSelectionComboBox
            // 
            this.branchSelectionComboBox.FormattingEnabled = true;
            this.branchSelectionComboBox.Location = new System.Drawing.Point(267, 84);
            this.branchSelectionComboBox.Name = "branchSelectionComboBox";
            this.branchSelectionComboBox.Size = new System.Drawing.Size(394, 39);
            this.branchSelectionComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Default Branch: ";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(106, 257);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(133, 58);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(441, 257);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(133, 58);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 371);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.branchSelectionComboBox);
            this.Controls.Add(this.openFileCheckBox);
            this.Controls.Add(this.emailInclusionCheckBox);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox emailInclusionCheckBox;
        private System.Windows.Forms.CheckBox openFileCheckBox;
        private System.Windows.Forms.ComboBox branchSelectionComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
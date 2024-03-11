namespace FirstGUIAttempt
{
    partial class IntermediatePage
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
            this.PasswordMatchText = new System.Windows.Forms.Label();
            this.FacialAnalysisText = new System.Windows.Forms.Label();
            this.KeystrokeAnalysisText = new System.Windows.Forms.Label();
            this.PasswordMatchTickBox = new System.Windows.Forms.Label();
            this.FacialAnalysisTickBox = new System.Windows.Forms.Label();
            this.KeystrokeAnalysisTickBox = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PasswordMatchText
            // 
            this.PasswordMatchText.AutoSize = true;
            this.PasswordMatchText.Location = new System.Drawing.Point(426, 229);
            this.PasswordMatchText.Name = "PasswordMatchText";
            this.PasswordMatchText.Size = new System.Drawing.Size(126, 20);
            this.PasswordMatchText.TabIndex = 0;
            this.PasswordMatchText.Text = "Password Match";
            // 
            // FacialAnalysisText
            // 
            this.FacialAnalysisText.AutoSize = true;
            this.FacialAnalysisText.Location = new System.Drawing.Point(426, 311);
            this.FacialAnalysisText.Name = "FacialAnalysisText";
            this.FacialAnalysisText.Size = new System.Drawing.Size(113, 20);
            this.FacialAnalysisText.TabIndex = 1;
            this.FacialAnalysisText.Text = "Facial Analysis";
            // 
            // KeystrokeAnalysisText
            // 
            this.KeystrokeAnalysisText.AutoSize = true;
            this.KeystrokeAnalysisText.Location = new System.Drawing.Point(426, 384);
            this.KeystrokeAnalysisText.Name = "KeystrokeAnalysisText";
            this.KeystrokeAnalysisText.Size = new System.Drawing.Size(141, 20);
            this.KeystrokeAnalysisText.TabIndex = 2;
            this.KeystrokeAnalysisText.Text = "Keystroke Analysis";
            // 
            // PasswordMatchTickBox
            // 
            this.PasswordMatchTickBox.AutoSize = true;
            this.PasswordMatchTickBox.Location = new System.Drawing.Point(381, 228);
            this.PasswordMatchTickBox.Name = "PasswordMatchTickBox";
            this.PasswordMatchTickBox.Size = new System.Drawing.Size(0, 20);
            this.PasswordMatchTickBox.TabIndex = 3;
            this.PasswordMatchTickBox.Text = string.Empty;
            // 
            // FacialAnalysisTickBox
            // 
            this.FacialAnalysisTickBox.AutoSize = true;
            this.FacialAnalysisTickBox.Location = new System.Drawing.Point(381, 311);
            this.FacialAnalysisTickBox.Name = "FacialAnalysisTickBox";
            this.FacialAnalysisTickBox.Size = new System.Drawing.Size(25, 20);
            this.FacialAnalysisTickBox.TabIndex = 4;
            this.FacialAnalysisTickBox.Text = string.Empty;
            // 
            // KeystrokeAnalysisTickBox
            // 
            this.KeystrokeAnalysisTickBox.AutoSize = true;
            this.KeystrokeAnalysisTickBox.Location = new System.Drawing.Point(381, 384);
            this.KeystrokeAnalysisTickBox.Name = "KeystrokeAnalysisTickBox";
            this.KeystrokeAnalysisTickBox.Size = new System.Drawing.Size(25, 20);
            this.KeystrokeAnalysisTickBox.TabIndex = 5;
            this.KeystrokeAnalysisTickBox.Text = string.Empty;
            // 
            // IntermediatePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 698);
            this.Controls.Add(this.KeystrokeAnalysisTickBox);
            this.Controls.Add(this.FacialAnalysisTickBox);
            this.Controls.Add(this.PasswordMatchTickBox);
            this.Controls.Add(this.KeystrokeAnalysisText);
            this.Controls.Add(this.FacialAnalysisText);
            this.Controls.Add(this.PasswordMatchText);
            this.Name = "IntermediatePage";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PasswordMatchText;
        private System.Windows.Forms.Label FacialAnalysisText;
        private System.Windows.Forms.Label KeystrokeAnalysisText;
        private System.Windows.Forms.Label PasswordMatchTickBox;
        private System.Windows.Forms.Label FacialAnalysisTickBox;
        private System.Windows.Forms.Label KeystrokeAnalysisTickBox;
    }
}
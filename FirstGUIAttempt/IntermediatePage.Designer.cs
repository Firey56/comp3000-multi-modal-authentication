namespace FirstGUIAttempt
{
    using System.Drawing;
    using System;
    using System.Windows.Forms;
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
            this.spinner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.spinner)).BeginInit();
            this.SuspendLayout();
            // 
            // PasswordMatchText
            // 
            this.PasswordMatchText.AutoSize = true;
            this.PasswordMatchText.Font = new System.Drawing.Font("Arial", 16F);
            this.PasswordMatchText.Location = new System.Drawing.Point(397, 229);
            this.PasswordMatchText.Name = "PasswordMatchText";
            this.PasswordMatchText.Size = new System.Drawing.Size(312, 36);
            this.PasswordMatchText.TabIndex = 0;
            this.PasswordMatchText.Text = "Password Match       ";
            // 
            // FacialAnalysisText
            // 
            this.FacialAnalysisText.AutoSize = true;
            this.FacialAnalysisText.Font = new System.Drawing.Font("Arial", 16F);
            this.FacialAnalysisText.Location = new System.Drawing.Point(397, 311);
            this.FacialAnalysisText.Name = "FacialAnalysisText";
            this.FacialAnalysisText.Size = new System.Drawing.Size(295, 36);
            this.FacialAnalysisText.TabIndex = 1;
            this.FacialAnalysisText.Text = "Facial Analysis        ";
            // 
            // KeystrokeAnalysisText
            // 
            this.KeystrokeAnalysisText.AutoSize = true;
            this.KeystrokeAnalysisText.Font = new System.Drawing.Font("Arial", 16F);
            this.KeystrokeAnalysisText.Location = new System.Drawing.Point(397, 384);
            this.KeystrokeAnalysisText.Name = "KeystrokeAnalysisText";
            this.KeystrokeAnalysisText.Size = new System.Drawing.Size(278, 36);
            this.KeystrokeAnalysisText.TabIndex = 2;
            this.KeystrokeAnalysisText.Text = "Keystroke Analysis";
            // 
            // PasswordMatchTickBox
            // 
            this.PasswordMatchTickBox.AutoSize = true;
            this.PasswordMatchTickBox.Font = new System.Drawing.Font("Arial", 16F);
            this.PasswordMatchTickBox.Location = new System.Drawing.Point(710, 229);
            this.PasswordMatchTickBox.Name = "PasswordMatchTickBox";
            this.PasswordMatchTickBox.Size = new System.Drawing.Size(33, 36);
            this.PasswordMatchTickBox.TabIndex = 3;
            this.PasswordMatchTickBox.Text = "1";
            // 
            // FacialAnalysisTickBox
            // 
            this.FacialAnalysisTickBox.AutoSize = true;
            this.FacialAnalysisTickBox.Font = new System.Drawing.Font("Arial", 16F);
            this.FacialAnalysisTickBox.Location = new System.Drawing.Point(710, 311);
            this.FacialAnalysisTickBox.Name = "FacialAnalysisTickBox";
            this.FacialAnalysisTickBox.Size = new System.Drawing.Size(33, 36);
            this.FacialAnalysisTickBox.TabIndex = 4;
            this.FacialAnalysisTickBox.Text = "1";
            // 
            // KeystrokeAnalysisTickBox
            // 
            this.KeystrokeAnalysisTickBox.AutoSize = true;
            this.KeystrokeAnalysisTickBox.Font = new System.Drawing.Font("Arial", 16F);
            this.KeystrokeAnalysisTickBox.Location = new System.Drawing.Point(710, 384);
            this.KeystrokeAnalysisTickBox.Name = "KeystrokeAnalysisTickBox";
            this.KeystrokeAnalysisTickBox.Size = new System.Drawing.Size(33, 36);
            this.KeystrokeAnalysisTickBox.TabIndex = 5;
            this.KeystrokeAnalysisTickBox.Text = "1";
            // 
            // spinner
            // 
            this.spinner.Location = new System.Drawing.Point(478, 480);
            this.spinner.Name = "spinner";
            this.spinner.Size = new System.Drawing.Size(100, 100);
            this.spinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.spinner.TabIndex = 6;
            this.spinner.TabStop = false;
            // 
            // IntermediatePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(145)))), ((int)(((byte)(141)))));
            this.ClientSize = new System.Drawing.Size(1238, 698);
            this.Controls.Add(this.spinner);
            this.Controls.Add(this.KeystrokeAnalysisTickBox);
            this.Controls.Add(this.FacialAnalysisTickBox);
            this.Controls.Add(this.PasswordMatchTickBox);
            this.Controls.Add(this.KeystrokeAnalysisText);
            this.Controls.Add(this.FacialAnalysisText);
            this.Controls.Add(this.PasswordMatchText);
            this.Name = "IntermediatePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logging In...";
            ((System.ComponentModel.ISupportInitialize)(this.spinner)).EndInit();
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
        private PictureBox spinner;
    }
}
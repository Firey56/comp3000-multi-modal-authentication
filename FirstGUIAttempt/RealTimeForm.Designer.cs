namespace FirstGUIAttempt
{
    using System;
    using System.Drawing;
    partial class RealTimeForm
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
            this.userInputsText = new System.Windows.Forms.Label();
            this.passwordMatchText = new System.Windows.Forms.Label();
            this.facialAnalysisText = new System.Windows.Forms.Label();
            this.keystrokeAnalysisText = new System.Windows.Forms.Label();
            this.decisionText = new System.Windows.Forms.Label();
            this.PasswordMatchTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // userInputsText
            // 
            this.userInputsText.AutoSize = true;
            this.userInputsText.Location = new System.Drawing.Point(62, 333);
            this.userInputsText.Name = "userInputsText";
            this.userInputsText.Size = new System.Drawing.Size(92, 20);
            this.userInputsText.TabIndex = 4;
            this.userInputsText.Text = "User Inputs";
            // 
            // passwordMatchText
            // 
            this.passwordMatchText.AutoSize = true;
            this.passwordMatchText.Location = new System.Drawing.Point(382, 167);
            this.passwordMatchText.Name = "passwordMatchText";
            this.passwordMatchText.Size = new System.Drawing.Size(126, 20);
            this.passwordMatchText.TabIndex = 5;
            this.passwordMatchText.Text = "Password Match";
            // 
            // facialAnalysisText
            // 
            this.facialAnalysisText.AutoSize = true;
            this.facialAnalysisText.Location = new System.Drawing.Point(382, 349);
            this.facialAnalysisText.Name = "facialAnalysisText";
            this.facialAnalysisText.Size = new System.Drawing.Size(113, 20);
            this.facialAnalysisText.TabIndex = 6;
            this.facialAnalysisText.Text = "Facial Analysis";
            // 
            // keystrokeAnalysisText
            // 
            this.keystrokeAnalysisText.AutoSize = true;
            this.keystrokeAnalysisText.Location = new System.Drawing.Point(367, 530);
            this.keystrokeAnalysisText.Name = "keystrokeAnalysisText";
            this.keystrokeAnalysisText.Size = new System.Drawing.Size(141, 20);
            this.keystrokeAnalysisText.TabIndex = 7;
            this.keystrokeAnalysisText.Text = "Keystroke Analysis";
            // 
            // decisionText
            // 
            this.decisionText.AutoSize = true;
            this.decisionText.Location = new System.Drawing.Point(802, 372);
            this.decisionText.Name = "decisionText";
            this.decisionText.Size = new System.Drawing.Size(70, 20);
            this.decisionText.TabIndex = 8;
            this.decisionText.Text = "Decision";
            // 
            // PasswordMatchTextBox
            // 
            this.PasswordMatchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswordMatchTextBox.Location = new System.Drawing.Point(400, 200);
            this.PasswordMatchTextBox.Name = "PasswordMatchTextBox";
            this.PasswordMatchTextBox.Size = new System.Drawing.Size(70, 19);
            this.PasswordMatchTextBox.TabIndex = 9;
            this.PasswordMatchTextBox.Text = "";
            // 
            // RealTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 675);
            this.Controls.Add(this.PasswordMatchTextBox);
            this.Controls.Add(this.decisionText);
            this.Controls.Add(this.keystrokeAnalysisText);
            this.Controls.Add(this.facialAnalysisText);
            this.Controls.Add(this.passwordMatchText);
            this.Controls.Add(this.userInputsText);
            this.Name = "RealTimeForm";
            this.Text = "RealTimeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label userInputsText;
        private System.Windows.Forms.Label passwordMatchText;
        private System.Windows.Forms.Label facialAnalysisText;
        private System.Windows.Forms.Label keystrokeAnalysisText;
        private System.Windows.Forms.Label decisionText;
        private System.Windows.Forms.TextBox PasswordMatchTextBox;
    }
}
namespace FirstGUIAttempt
{
    using System.Windows.Forms;
    using System.Drawing;
    using System;
    partial class AuthenticationModel
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
        /// 
        private void InitializeComponent()
        {
            this.signIn = new System.Windows.Forms.Button();
            this.signUp = new System.Windows.Forms.Button();
            this.SignInPage = new System.Windows.Forms.PageSetupDialog();
            this.SuspendLayout();
            // 
            // signIn
            // 
            this.signIn.Location = new System.Drawing.Point(199, 261);
            this.signIn.Name = "signIn";
            this.signIn.Size = new System.Drawing.Size(166, 67);
            this.signIn.TabIndex = 0;
            this.signIn.Text = "Sign In";
            this.signIn.UseVisualStyleBackColor = true;
            this.signIn.Click += new System.EventHandler(this.signIn_Click);
            // 
            // signUp
            // 
            this.signUp.Location = new System.Drawing.Point(199, 85);
            this.signUp.Name = "signUp";
            this.signUp.Size = new System.Drawing.Size(166, 68);
            this.signUp.TabIndex = 1;
            this.signUp.Text = "Sign Up";
            this.signUp.UseVisualStyleBackColor = true;
            this.signUp.Click += new System.EventHandler(this.signUp_Click);
            // 
            // FirstGUIAttempt
            // 
            this.ClientSize = new System.Drawing.Size(578, 444);
            this.Controls.Add(this.signUp);
            this.Controls.Add(this.signIn);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.Name = "FirstGUIAttempt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Button signIn;
        private System.Windows.Forms.Button signUp;
        private System.Windows.Forms.PageSetupDialog SignInPage;
    }
}


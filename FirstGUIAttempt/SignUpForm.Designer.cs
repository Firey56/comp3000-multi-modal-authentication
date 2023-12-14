namespace FirstGUIAttempt
{
    partial class SignUpForm
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
            this.signUpFormUsernameTextBox = new System.Windows.Forms.TextBox();
            this.signUpFormUsernameLabel = new System.Windows.Forms.Label();
            this.signUpFormPasswordTextBox = new System.Windows.Forms.TextBox();
            this.signUpFormPasswordLabel = new System.Windows.Forms.Label();
            this.signUpPictureBox = new System.Windows.Forms.PictureBox();
            this.signUpBrowseButton = new System.Windows.Forms.Button();
            this.signUpBrowseButtonLabel = new System.Windows.Forms.Label();
            this.signUpSubmitButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.signUpPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // signUpFormUsernameTextBox
            // 
            this.signUpFormUsernameTextBox.Location = new System.Drawing.Point(423, 179);
            this.signUpFormUsernameTextBox.Name = "signUpFormUsernameTextBox";
            this.signUpFormUsernameTextBox.Size = new System.Drawing.Size(572, 26);
            this.signUpFormUsernameTextBox.TabIndex = 0;
            this.signUpFormUsernameTextBox.TextChanged += new System.EventHandler(this.signUpUsernameTextbox);
            // 
            // signUpFormUsernameLabel
            // 
            this.signUpFormUsernameLabel.AutoSize = true;
            this.signUpFormUsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.signUpFormUsernameLabel.Location = new System.Drawing.Point(144, 169);
            this.signUpFormUsernameLabel.Name = "signUpFormUsernameLabel";
            this.signUpFormUsernameLabel.Size = new System.Drawing.Size(164, 37);
            this.signUpFormUsernameLabel.TabIndex = 1;
            this.signUpFormUsernameLabel.Text = "Username";
            this.signUpFormUsernameLabel.Click += new System.EventHandler(this.usernameLabel);
            // 
            // signUpFormPasswordTextBox
            // 
            this.signUpFormPasswordTextBox.Location = new System.Drawing.Point(423, 325);
            this.signUpFormPasswordTextBox.Name = "signUpFormPasswordTextBox";
            this.signUpFormPasswordTextBox.PasswordChar = '*';
            this.signUpFormPasswordTextBox.Size = new System.Drawing.Size(614, 26);
            this.signUpFormPasswordTextBox.TabIndex = 2;
            this.signUpFormPasswordTextBox.TextChanged += new System.EventHandler(this.signUpPasswordTextBox);
            // 
            // signUpFormPasswordLabel
            // 
            this.signUpFormPasswordLabel.AutoSize = true;
            this.signUpFormPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.signUpFormPasswordLabel.Location = new System.Drawing.Point(144, 325);
            this.signUpFormPasswordLabel.Name = "signUpFormPasswordLabel";
            this.signUpFormPasswordLabel.Size = new System.Drawing.Size(158, 37);
            this.signUpFormPasswordLabel.TabIndex = 3;
            this.signUpFormPasswordLabel.Text = "Password";
            this.signUpFormPasswordLabel.Click += new System.EventHandler(this.signUpPasswordLabel);
            // 
            // signUpPictureBox
            // 
            this.signUpPictureBox.Location = new System.Drawing.Point(550, 442);
            this.signUpPictureBox.Name = "signUpPictureBox";
            this.signUpPictureBox.Size = new System.Drawing.Size(211, 129);
            this.signUpPictureBox.TabIndex = 4;
            this.signUpPictureBox.TabStop = false;
            this.signUpPictureBox.Click += new System.EventHandler(this.signUpFormPictureBox);
            // 
            // signUpBrowseButton
            // 
            this.signUpBrowseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.signUpBrowseButton.Location = new System.Drawing.Point(349, 496);
            this.signUpBrowseButton.Name = "signUpBrowseButton";
            this.signUpBrowseButton.Size = new System.Drawing.Size(161, 45);
            this.signUpBrowseButton.TabIndex = 5;
            this.signUpBrowseButton.Text = "Browse";
            this.signUpBrowseButton.UseVisualStyleBackColor = true;
            this.signUpBrowseButton.Click += new System.EventHandler(this.browseButton);
            // 
            // signUpBrowseButtonLabel
            // 
            this.signUpBrowseButtonLabel.AutoSize = true;
            this.signUpBrowseButtonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.signUpBrowseButtonLabel.Location = new System.Drawing.Point(144, 496);
            this.signUpBrowseButtonLabel.Name = "signUpBrowseButtonLabel";
            this.signUpBrowseButtonLabel.Size = new System.Drawing.Size(119, 37);
            this.signUpBrowseButtonLabel.TabIndex = 6;
            this.signUpBrowseButtonLabel.Text = "Upload";
            this.signUpBrowseButtonLabel.Click += new System.EventHandler(this.browseButtonLabel);
            // 
            // signUpSubmitButton
            // 
            this.signUpSubmitButton.Location = new System.Drawing.Point(608, 628);
            this.signUpSubmitButton.Name = "signUpSubmitButton";
            this.signUpSubmitButton.Size = new System.Drawing.Size(107, 63);
            this.signUpSubmitButton.TabIndex = 7;
            this.signUpSubmitButton.Text = "Submit";
            this.signUpSubmitButton.UseVisualStyleBackColor = true;
            this.signUpSubmitButton.Click += new System.EventHandler(this.submitButton);
            // 
            // SignUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 733);
            this.Controls.Add(this.signUpSubmitButton);
            this.Controls.Add(this.signUpBrowseButtonLabel);
            this.Controls.Add(this.signUpBrowseButton);
            this.Controls.Add(this.signUpPictureBox);
            this.Controls.Add(this.signUpFormPasswordLabel);
            this.Controls.Add(this.signUpFormPasswordTextBox);
            this.Controls.Add(this.signUpFormUsernameLabel);
            this.Controls.Add(this.signUpFormUsernameTextBox);
            this.Name = "SignUpForm";
            this.Text = "SignUpForm";
            ((System.ComponentModel.ISupportInitialize)(this.signUpPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox signUpFormUsernameTextBox;
        private System.Windows.Forms.Label signUpFormUsernameLabel;
        private System.Windows.Forms.TextBox signUpFormPasswordTextBox;
        private System.Windows.Forms.Label signUpFormPasswordLabel;
        private System.Windows.Forms.PictureBox signUpPictureBox;
        private System.Windows.Forms.Button signUpBrowseButton;
        private System.Windows.Forms.Label signUpBrowseButtonLabel;
        private System.Windows.Forms.Button signUpSubmitButton;
    }
}
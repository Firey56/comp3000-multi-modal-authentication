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
            this.photoText = new System.Windows.Forms.Label();
            this.signUpSubmitButton = new System.Windows.Forms.Button();
            this.takePhotoButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.signUpPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // signUpFormUsernameTextBox
            // 
            this.signUpFormUsernameTextBox.Location = new System.Drawing.Point(200, 120);
            this.signUpFormUsernameTextBox.Name = "signUpFormUsernameTextBox";
            this.signUpFormUsernameTextBox.Size = new System.Drawing.Size(400, 30);
            this.signUpFormUsernameTextBox.TabIndex = 0;
            // 
            // signUpFormUsernameLabel
            // 
            this.signUpFormUsernameLabel.AutoSize = true;
            this.signUpFormUsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.signUpFormUsernameLabel.Location = new System.Drawing.Point(200, 100);
            this.signUpFormUsernameLabel.Name = "signUpFormUsernameLabel";
            this.signUpFormUsernameLabel.Size = new System.Drawing.Size(180, 40);
            this.signUpFormUsernameLabel.TabIndex = 1;
            this.signUpFormUsernameLabel.Text = "Username";
            // 
            // signUpFormPasswordTextBox
            // 
            this.signUpFormPasswordTextBox.Location = new System.Drawing.Point(244, 172);
            this.signUpFormPasswordTextBox.Name = "signUpFormPasswordTextBox";
            this.signUpFormPasswordTextBox.PasswordChar = '*';
            this.signUpFormPasswordTextBox.Size = new System.Drawing.Size(200, 26);
            this.signUpFormPasswordTextBox.TabIndex = 2;
            // 
            // signUpFormPasswordLabel
            // 
            this.signUpFormPasswordLabel.AutoSize = true;
            this.signUpFormPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.signUpFormPasswordLabel.Location = new System.Drawing.Point(48, 172);
            this.signUpFormPasswordLabel.Name = "signUpFormPasswordLabel";
            this.signUpFormPasswordLabel.Size = new System.Drawing.Size(158, 37);
            this.signUpFormPasswordLabel.TabIndex = 3;
            this.signUpFormPasswordLabel.Text = "Password";
            // 
            // signUpPictureBox
            // 
            this.signUpPictureBox.Location = new System.Drawing.Point(550, 442);
            this.signUpPictureBox.Name = "signUpPictureBox";
            this.signUpPictureBox.Size = new System.Drawing.Size(212, 129);
            this.signUpPictureBox.TabIndex = 4;
            this.signUpPictureBox.TabStop = false;
            // 
            // photoText
            // 
            this.photoText.AutoSize = true;
            this.photoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.photoText.Location = new System.Drawing.Point(184, 489);
            this.photoText.Name = "photoText";
            this.photoText.Size = new System.Drawing.Size(101, 37);
            this.photoText.TabIndex = 6;
            this.photoText.Text = "Photo";
            // 
            // signUpSubmitButton
            // 
            this.signUpSubmitButton.Location = new System.Drawing.Point(608, 628);
            this.signUpSubmitButton.Name = "signUpSubmitButton";
            this.signUpSubmitButton.Size = new System.Drawing.Size(106, 63);
            this.signUpSubmitButton.TabIndex = 7;
            this.signUpSubmitButton.Text = "Submit";
            this.signUpSubmitButton.UseVisualStyleBackColor = true;
            this.signUpSubmitButton.Click += new System.EventHandler(this.submit);
            // 
            // takePhotoButton
            // 
            this.takePhotoButton.Location = new System.Drawing.Point(406, 495);
            this.takePhotoButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.takePhotoButton.Name = "takePhotoButton";
            this.takePhotoButton.Size = new System.Drawing.Size(112, 35);
            this.takePhotoButton.TabIndex = 8;
            this.takePhotoButton.Text = "Take Photo";
            this.takePhotoButton.UseVisualStyleBackColor = true;
            this.takePhotoButton.Click += new System.EventHandler(this.takePhoto);
            // 
            // SignUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 807);
            this.Controls.Add(this.takePhotoButton);
            this.Controls.Add(this.signUpSubmitButton);
            this.Controls.Add(this.photoText);
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
        private System.Windows.Forms.Label photoText;
        private System.Windows.Forms.Button signUpSubmitButton;
        private System.Windows.Forms.Button takePhotoButton;
    }
}
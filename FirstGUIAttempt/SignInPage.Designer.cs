namespace FirstGUIAttempt
{
    partial class SignInPage
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
            this.usernameInputTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordInputTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabelText = new System.Windows.Forms.Label();
            this.signInButton = new System.Windows.Forms.Button();
            this.photoUploadButton = new System.Windows.Forms.PictureBox();
            this.photoUploadLabelText = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.takePhotoButton = new System.Windows.Forms.Button();
            this.newImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.photoUploadButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newImage)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameInputTextBox
            // 
            this.usernameInputTextBox.Location = new System.Drawing.Point(346, 205);
            this.usernameInputTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.usernameInputTextBox.Name = "usernameInputTextBox";
            this.usernameInputTextBox.Size = new System.Drawing.Size(418, 26);
            this.usernameInputTextBox.TabIndex = 0;
            this.usernameInputTextBox.TextChanged += new System.EventHandler(this.userNameInput);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(195, 205);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            this.label1.Click += new System.EventHandler(this.usernameLabel);
            // 
            // passwordInputTextBox
            // 
            this.passwordInputTextBox.Location = new System.Drawing.Point(346, 352);
            this.passwordInputTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.passwordInputTextBox.Name = "passwordInputTextBox";
            this.passwordInputTextBox.PasswordChar = '*';
            this.passwordInputTextBox.Size = new System.Drawing.Size(418, 26);
            this.passwordInputTextBox.TabIndex = 2;
            this.passwordInputTextBox.TextChanged += new System.EventHandler(this.passwordInput);
            // 
            // passwordLabelText
            // 
            this.passwordLabelText.AutoSize = true;
            this.passwordLabelText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.passwordLabelText.Location = new System.Drawing.Point(195, 352);
            this.passwordLabelText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.passwordLabelText.Name = "passwordLabelText";
            this.passwordLabelText.Size = new System.Drawing.Size(120, 29);
            this.passwordLabelText.TabIndex = 3;
            this.passwordLabelText.Text = "Password";
            this.passwordLabelText.Click += new System.EventHandler(this.passwordLabel);
            // 
            // signInButton
            // 
            this.signInButton.Location = new System.Drawing.Point(488, 562);
            this.signInButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.signInButton.Name = "signInButton";
            this.signInButton.Size = new System.Drawing.Size(112, 35);
            this.signInButton.TabIndex = 4;
            this.signInButton.Text = "Sign In";
            this.signInButton.UseVisualStyleBackColor = true;
            this.signInButton.Click += new System.EventHandler(this.signIn);
            // 
            // photoUploadButton
            // 
            this.photoUploadButton.Location = new System.Drawing.Point(761, 388);
            this.photoUploadButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.photoUploadButton.Name = "photoUploadButton";
            this.photoUploadButton.Size = new System.Drawing.Size(320, 240);
            this.photoUploadButton.TabIndex = 5;
            this.photoUploadButton.TabStop = false;
            this.photoUploadButton.Click += new System.EventHandler(this.photoUpload);
            // 
            // photoUploadLabelText
            // 
            this.photoUploadLabelText.AutoSize = true;
            this.photoUploadLabelText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.photoUploadLabelText.Location = new System.Drawing.Point(195, 458);
            this.photoUploadLabelText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.photoUploadLabelText.Name = "photoUploadLabelText";
            this.photoUploadLabelText.Size = new System.Drawing.Size(91, 29);
            this.photoUploadLabelText.TabIndex = 6;
            this.photoUploadLabelText.Text = "Upload";
            this.photoUploadLabelText.Click += new System.EventHandler(this.photoUploadLabel);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(315, 454);
            this.browseButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(128, 35);
            this.browseButton.TabIndex = 7;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.photoUploadButtonClick);
            // 
            // takePhotoButton
            // 
            this.takePhotoButton.Location = new System.Drawing.Point(656, 493);
            this.takePhotoButton.Name = "takePhotoButton";
            this.takePhotoButton.Size = new System.Drawing.Size(98, 25);
            this.takePhotoButton.TabIndex = 8;
            this.takePhotoButton.Text = "Take Photo";
            this.takePhotoButton.UseVisualStyleBackColor = true;
            this.takePhotoButton.Click += new System.EventHandler(this.takePhotoButton_Click);
            // 
            // newImage
            // 
            this.newImage.Location = new System.Drawing.Point(761, 102);
            this.newImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.newImage.Name = "newImage";
            this.newImage.Size = new System.Drawing.Size(320, 240);
            this.newImage.TabIndex = 9;
            this.newImage.TabStop = false;
            // 
            // SignInPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.newImage);
            this.Controls.Add(this.takePhotoButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.photoUploadLabelText);
            this.Controls.Add(this.photoUploadButton);
            this.Controls.Add(this.signInButton);
            this.Controls.Add(this.passwordLabelText);
            this.Controls.Add(this.passwordInputTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usernameInputTextBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SignInPage";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.SignInPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.photoUploadButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameInputTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordInputTextBox;
        private System.Windows.Forms.Label passwordLabelText;
        private System.Windows.Forms.Button signInButton;
        private System.Windows.Forms.PictureBox photoUploadButton;
        private System.Windows.Forms.Label photoUploadLabelText;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button takePhotoButton;
        private System.Windows.Forms.PictureBox newImage;
    }
}
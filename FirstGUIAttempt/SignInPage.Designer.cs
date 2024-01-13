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
            this.takePhotoButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.photoUploadButton)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameInputTextBox
            // 
            this.usernameInputTextBox.Location = new System.Drawing.Point(231, 133);
            this.usernameInputTextBox.Name = "usernameInputTextBox";
            this.usernameInputTextBox.Size = new System.Drawing.Size(280, 20);
            this.usernameInputTextBox.TabIndex = 0;
            this.usernameInputTextBox.TextChanged += new System.EventHandler(this.userNameInput);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(130, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            this.label1.Click += new System.EventHandler(this.usernameLabel);
            // 
            // passwordInputTextBox
            // 
            this.passwordInputTextBox.Location = new System.Drawing.Point(231, 229);
            this.passwordInputTextBox.Name = "passwordInputTextBox";
            this.passwordInputTextBox.PasswordChar = '*';
            this.passwordInputTextBox.Size = new System.Drawing.Size(280, 20);
            this.passwordInputTextBox.TabIndex = 2;
            this.passwordInputTextBox.TextChanged += new System.EventHandler(this.passwordInput);
            // 
            // passwordLabelText
            // 
            this.passwordLabelText.AutoSize = true;
            this.passwordLabelText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.passwordLabelText.Location = new System.Drawing.Point(130, 229);
            this.passwordLabelText.Name = "passwordLabelText";
            this.passwordLabelText.Size = new System.Drawing.Size(78, 20);
            this.passwordLabelText.TabIndex = 3;
            this.passwordLabelText.Text = "Password";
            this.passwordLabelText.Click += new System.EventHandler(this.passwordLabel);
            // 
            // signInButton
            // 
            this.signInButton.Location = new System.Drawing.Point(323, 392);
            this.signInButton.Name = "signInButton";
            this.signInButton.Size = new System.Drawing.Size(75, 23);
            this.signInButton.TabIndex = 4;
            this.signInButton.Text = "Sign In";
            this.signInButton.UseVisualStyleBackColor = true;
            this.signInButton.Click += new System.EventHandler(this.submit);
            // 
            // photoUploadButton
            // 
            this.photoUploadButton.Location = new System.Drawing.Point(298, 281);
            this.photoUploadButton.Name = "photoUploadButton";
            this.photoUploadButton.Size = new System.Drawing.Size(127, 105);
            this.photoUploadButton.TabIndex = 5;
            this.photoUploadButton.TabStop = false;
            // 
            // takePhotoButton
            // 
            this.takePhotoButton.Location = new System.Drawing.Point(134, 322);
            this.takePhotoButton.Margin = new System.Windows.Forms.Padding(2);
            this.takePhotoButton.Name = "takePhotoButton";
            this.takePhotoButton.Size = new System.Drawing.Size(74, 26);
            this.takePhotoButton.TabIndex = 8;
            this.takePhotoButton.Text = "Take Photo";
            this.takePhotoButton.UseVisualStyleBackColor = true;
            this.takePhotoButton.Click += new System.EventHandler(this.takePhotoButton_Click);
            // 
            // SignInPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.takePhotoButton);
            this.Controls.Add(this.photoUploadButton);
            this.Controls.Add(this.signInButton);
            this.Controls.Add(this.passwordLabelText);
            this.Controls.Add(this.passwordInputTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usernameInputTextBox);
            this.Name = "SignInPage";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.photoUploadButton)).EndInit();
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
        //private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button takePhotoButton;
    }
}
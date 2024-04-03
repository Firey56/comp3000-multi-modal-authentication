namespace FirstGUIAttempt
{
    partial class AdminForm
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
            this.CurrentUsersListBox = new System.Windows.Forms.ListBox();
            this.DeletedUsersListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // CurrentUsersListBox
            // 
            this.CurrentUsersListBox.FormattingEnabled = true;
            this.CurrentUsersListBox.ItemHeight = 20;
            this.CurrentUsersListBox.Location = new System.Drawing.Point(93, 61);
            this.CurrentUsersListBox.Name = "CurrentUsersListBox";
            this.CurrentUsersListBox.Size = new System.Drawing.Size(380, 764);
            this.CurrentUsersListBox.TabIndex = 0;
            // 
            // DeletedUsersListBox
            // 
            this.DeletedUsersListBox.FormattingEnabled = true;
            this.DeletedUsersListBox.ItemHeight = 20;
            this.DeletedUsersListBox.Location = new System.Drawing.Point(1002, 61);
            this.DeletedUsersListBox.Name = "DeletedUsersListBox";
            this.DeletedUsersListBox.Size = new System.Drawing.Size(408, 764);
            this.DeletedUsersListBox.TabIndex = 1;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1582, 1115);
            this.Controls.Add(this.DeletedUsersListBox);
            this.Controls.Add(this.CurrentUsersListBox);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.ListBox CurrentUsersListBox;
        private System.Windows.Forms.ListBox DeletedUsersListBox;
    }
}
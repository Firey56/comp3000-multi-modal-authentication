namespace FirstGUIAttempt
{
    partial class AccessibilitySettings
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
            this.FontSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.FontSizeLabel = new System.Windows.Forms.Label();
            this.FontLabel = new System.Windows.Forms.Label();
            this.FontTrackBar = new System.Windows.Forms.TrackBar();
            this.SaveAccessibility = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FontSizeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FontTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // FontSizeTrackBar
            // 
            this.FontSizeTrackBar.LargeChange = 1;
            this.FontSizeTrackBar.Location = new System.Drawing.Point(92, 129);
            this.FontSizeTrackBar.Maximum = 16;
            this.FontSizeTrackBar.Minimum = 8;
            this.FontSizeTrackBar.Name = "FontSizeTrackBar";
            this.FontSizeTrackBar.Size = new System.Drawing.Size(633, 69);
            this.FontSizeTrackBar.TabIndex = 0;
            this.FontSizeTrackBar.Value = 8;
            // 
            // FontSizeLabel
            // 
            this.FontSizeLabel.AutoSize = true;
            this.FontSizeLabel.Location = new System.Drawing.Point(92, 83);
            this.FontSizeLabel.Name = "FontSizeLabel";
            this.FontSizeLabel.Size = new System.Drawing.Size(77, 20);
            this.FontSizeLabel.TabIndex = 1;
            this.FontSizeLabel.Text = "Font Size";
            // 
            // FontLabel
            // 
            this.FontLabel.AutoSize = true;
            this.FontLabel.Location = new System.Drawing.Point(96, 225);
            this.FontLabel.Name = "FontLabel";
            this.FontLabel.Size = new System.Drawing.Size(42, 20);
            this.FontLabel.TabIndex = 2;
            this.FontLabel.Text = "Font";
            // 
            // FontTrackBar
            // 
            this.FontTrackBar.LargeChange = 1;
            this.FontTrackBar.Location = new System.Drawing.Point(92, 257);
            this.FontTrackBar.Maximum = 4;
            this.FontTrackBar.Minimum = 1;
            this.FontTrackBar.Name = "FontTrackBar";
            this.FontTrackBar.Size = new System.Drawing.Size(625, 69);
            this.FontTrackBar.TabIndex = 3;
            this.FontTrackBar.Value = 1;
            // 
            // SaveAccessibility
            // 
            this.SaveAccessibility.Location = new System.Drawing.Point(320, 392);
            this.SaveAccessibility.Name = "SaveAccessibility";
            this.SaveAccessibility.Size = new System.Drawing.Size(75, 30);
            this.SaveAccessibility.TabIndex = 4;
            this.SaveAccessibility.Text = "Save";
            this.SaveAccessibility.UseVisualStyleBackColor = true;
            // 
            // AccessibilitySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SaveAccessibility);
            this.Controls.Add(this.FontTrackBar);
            this.Controls.Add(this.FontLabel);
            this.Controls.Add(this.FontSizeLabel);
            this.Controls.Add(this.FontSizeTrackBar);
            this.Name = "AccessibilitySettings";
            this.Text = "AccessibilitySettings";
            ((System.ComponentModel.ISupportInitialize)(this.FontSizeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FontTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar FontSizeTrackBar;
        private System.Windows.Forms.Label FontSizeLabel;
        private System.Windows.Forms.Label FontLabel;
        private System.Windows.Forms.TrackBar FontTrackBar;
        private System.Windows.Forms.Button SaveAccessibility;
    }
}
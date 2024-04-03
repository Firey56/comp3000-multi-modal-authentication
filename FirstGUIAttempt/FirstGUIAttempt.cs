using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;

namespace FirstGUIAttempt
{
    public partial class FirstGUIAttempt : Form
    {
        private LinkLabel AccessibilitySettingsText = new LinkLabel();
        public FirstGUIAttempt()
        {
            InitializeComponent();
            AccessibilitySettingsText.Text = "Accessibility Settings";
            AccessibilitySettingsText.Location = new Point(230, 350);
            AccessibilitySettingsText.AutoSize = true;
            this.Controls.Add(AccessibilitySettingsText);
            AccessibilitySettingsText.Click += AccessibilitySettingsText_Click;
        }
        private void AccessibilitySettingsText_Click(object sender, EventArgs e)
        {
            var goToAccessibility = new AccessibilitySettings();
            goToAccessibility.Show();
        }
        private void signIn_Click(object sender, EventArgs e)
        {
            var goToSignIn = new NewSignInForm();
            goToSignIn.Show();
        }

        private void signUp_Click(object sender, EventArgs e)
        {
            var goToSignUp = new NewSignUpForm();
            goToSignUp.Show();

        }

    }
}

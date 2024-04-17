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
using System.Drawing.Text;
using System.IO;
using AForge.Video;
using AForge.Video.DirectShow;

namespace FirstGUIAttempt
{
    public partial class FirstGUIAttempt : Form
    {
        private LinkLabel AccessibilitySettingsText = new LinkLabel();
        public static PrivateFontCollection OpenDyslexic { get; set; } = new PrivateFontCollection();
        public FirstGUIAttempt()
        {
            InitializeComponent();
            LoadCustomFont("OpenDyslexic3-Regular");
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
            FilterInfoCollection Webcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if(Webcams.Count < 1)
            {
                MessageBox.Show("You are unable to complete sign up without a webcam.");
            }
            else
            {
                var goToSignUp = new NewSignUpForm();
                goToSignUp.Show();
            }
            

        }
        private static void LoadCustomFont(string fontFileName)
        {
            // Get path to custom font file
            string executableLocation = Application.StartupPath;
            string ParentDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(executableLocation).FullName).FullName).FullName;
            //Console.WriteLine("This is the parent directory: " + ParentDirectory);
            string fontPath = ParentDirectory + @"\" + fontFileName + ".ttf";
            //Console.WriteLine(fontPath);
            // Load custom font
            OpenDyslexic.AddFontFile(fontPath);
            Console.WriteLine($"Font Loaded: {OpenDyslexic.Families[0].Name}");
        }

    }
}

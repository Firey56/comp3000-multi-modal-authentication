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
    public partial class RealTimeForm : Form
    {
        private MetroFramework.Controls.MetroLabel KeystrokeAnalysisLabel;
        private MetroFramework.Controls.MetroLabel MetroKeystrokeAnalysisChangingLabel;
        private MetroFramework.Controls.MetroLabel PasswordMatchLabel;
        private MetroFramework.Controls.MetroLabel PasswordMatchTickBox;
        private MetroFramework.Controls.MetroLabel FacialAnalysisTickBox;
        private MetroFramework.Controls.MetroLabel FacialAnalysisText;
        private MetroFramework.Controls.MetroLabel KeystrokeAnalysisTickBox;
        private MetroFramework.Controls.MetroLabel UserInputsText;
        private MetroFramework.Controls.MetroLabel UserInputsChangingLabel;
        private MetroFramework.Controls.MetroLabel DecisionText;
        private MetroFramework.Controls.MetroLabel DecisionChangingLabel;



        public RealTimeForm()
        {
            InitializeComponent();

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            int centerX = ((screenWidth - this.Width) / 2) - 80;
            int centerY = ((screenHeight - this.Height) / 2)-50;

            ////
            ///KeystrokeAnalysisLabel
            ///
            KeystrokeAnalysisLabel = new MetroFramework.Controls.MetroLabel
            {
                Text = "Keystroke Analysis",
                Location = new System.Drawing.Point(centerX, centerY + 150), // Set the location of the 
                Theme = MetroFramework.MetroThemeStyle.Light,
                Style = MetroFramework.MetroColorStyle.Blue,
                Size = new System.Drawing.Size(150, 50),
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold) // Set custom font
            };

            FacialAnalysisText = new MetroFramework.Controls.MetroLabel
            {
                Location = new System.Drawing.Point(centerX, centerY),
                Text = "Facial Analysis"
            };

            PasswordMatchLabel = new MetroFramework.Controls.MetroLabel
            {
                Size = new System.Drawing.Size(120,100),
                Location = new System.Drawing.Point(centerX, centerY - 150),
                Text = "Password Match"
            };

            /////
            ///UserInputsText
            ///

            UserInputsText = new MetroFramework.Controls.MetroLabel
            {
                Location = new System.Drawing.Point(centerX - 200, centerY),
                Text = "User Inputs"
            };

            DecisionText = new MetroFramework.Controls.MetroLabel
            {
                Text = "Decision",
                Location = new System.Drawing.Point(centerX+300, centerY)
            };

            ///////////////////////////////////////////////////////
            ///All labels complete
            ///////////////////////////////////////////////////////
            ///

            ////////////////////////////////////////////////////
            ///Adding Tick Boxes
            ////////////////////////////////////////////////////
            ///

            /////
            ///FacialAnalysisTickBox
            ///
            FacialAnalysisTickBox = new MetroFramework.Controls.MetroLabel
            {
                Location = new System.Drawing.Point(centerX-30, centerY),
                Size = new System.Drawing.Size(30, 30)
            };

            /////
            ///PasswordMatchTickBox
            ///

            PasswordMatchTickBox = new MetroFramework.Controls.MetroLabel
            {
                Location = new System.Drawing.Point(centerX - 30, centerY - 150),
                Size = new System.Drawing.Size(30, 30)
            };
            
            /////
            ///KeystrokeAnalysisTickBox
            ///

            KeystrokeAnalysisTickBox = new MetroFramework.Controls.MetroLabel
            {
                Location = new System.Drawing.Point(centerX - 30, centerY + 150),
                Size = new System.Drawing.Size(30, 30)
            };




            /////////////////////////////////////////////
            ///End of Tick Boxes
            /////////////////////////////////////////////
            ///

            /////////////////////////////////////
            ///Start of Changing Text
            /////////////////////////////////////
            ///


            /////
            ///UserInputsChangingLabel
            ///

            UserInputsChangingLabel = new MetroFramework.Controls.MetroLabel
            {
                Location = new System.Drawing.Point(centerX - 200, centerY + 50),
                Size = new System.Drawing.Size(300,300),
                Font = new System.Drawing.Font("Comic Sans MS", 8, System.Drawing.FontStyle.Bold) // Set custom font

            };

            DecisionChangingLabel = new MetroFramework.Controls.MetroLabel
            {
                Location = new System.Drawing.Point(centerX+300, centerY+30),
                Size = new System.Drawing.Size(100,150)
            };

            DecisionChangingLabel = new MetroFramework.Controls.MetroLabel
            {
                Location = new System.Drawing.Point(centerX + 300, centerY + 30),
                Size = new System.Drawing.Size(100, 150)
            };
            MetroKeystrokeAnalysisChangingLabel = new MetroFramework.Controls.MetroLabel
            {
                Location = new System.Drawing.Point(centerX, centerY + 180),
                Size = new System.Drawing.Size(100, 150)
            };



            this.ClientSize = new System.Drawing.Size(1080, 720);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new System.Drawing.Size(1238, 698);

            this.Controls.Add(FacialAnalysisTickBox);
            this.Controls.Add(KeystrokeAnalysisTickBox);
            this.Controls.Add(PasswordMatchTickBox);
            this.Controls.Add(MetroKeystrokeAnalysisChangingLabel);
            this.Controls.Add(KeystrokeAnalysisLabel);
            this.Controls.Add(UserInputsText);
            this.Controls.Add(UserInputsChangingLabel);
            this.Controls.Add(DecisionText);
            this.Controls.Add(DecisionChangingLabel);
            this.Controls.Add(PasswordMatchLabel);
            this.Controls.Add(FacialAnalysisText);
        }
        public void UpdateLabelsForLogin(string labelUpdated, string data)
        {
            // Update labels on Form3 based on the event data
            switch (labelUpdated)
            {
                case "PasswordMatchTickBox":
                    if (data == "true")
                    {
                        PasswordMatchTickBox.Text = "✔";
                    }
                    break;
                case "FacialAnalysisTickBox":
                    if (float.Parse(data) >= 0.6)
                    {
                        FacialAnalysisTickBox.Text = "✔";
                    }
                    else
                    {
                        FacialAnalysisTickBox.Text = "✘";
                    }

                    break;
                case "KeystrokeAnalysisTickBox":
                    if (float.Parse(data) >= 0.6)
                    {
                        KeystrokeAnalysisTickBox.Text = "✔";
                        MetroKeystrokeAnalysisChangingLabel.Text = data;
                    }
                    else
                    {
                        KeystrokeAnalysisTickBox.Text = "✘";
                        MetroKeystrokeAnalysisChangingLabel.Text = data;
                    }
                    break;
                case "Decision":
                    if(float.Parse(data) > 0.8)
                    {
                        DecisionChangingLabel.Text = data;
                    }
                    else
                    {
                        DecisionChangingLabel.Text = "✘";
                    }
                    break;
                case "UserInputs":
                    UserInputsChangingLabel.Text = data;
                    break;
            }

        }

    }
}

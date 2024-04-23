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
        private Label KeystrokeAnalysisLabel;
        private Label MetroKeystrokeAnalysisChangingLabel;
        private Label PasswordMatchLabel;
        private Label PasswordMatchTickBox;
        private Label FacialAnalysisTickBox;
        private Label FacialAnalysisText;
        private Label KeystrokeAnalysisTickBox;
        private Label UserInputsText;
        private Label UserInputsChangingLabel;
        private Label DecisionText;
        private Label DecisionChangingLabel;
        private Label FacialAnalysisChangingLabel;
        private Label DecisionTickBox;



        public RealTimeForm()
        {
            InitializeComponent();
            ApplyFontSettings();
            this.Text = "Real Time Form";
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int xSize = 1080;
            int ySize = 800;
            this.ClientSize = new Size(xSize, ySize);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            int centerX = xSize/2;
            int centerY = ySize/ 2;

            ////
            ///KeystrokeAnalysisLabel
            ///
            KeystrokeAnalysisLabel = new Label
            {
                Text = "Keystroke Analysis",
                Location = new System.Drawing.Point(centerX-50, centerY + 150), // Set the location of the 
                AutoSize = true
            };

            FacialAnalysisText = new Label
            {
                Location = new System.Drawing.Point(centerX-50, centerY-50),
                Text = "Facial Analysis",
                AutoSize = true

            };

            PasswordMatchLabel = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(centerX-50, centerY - 250),
                Text = "Password Match"
            };

            /////
            ///UserInputsText
            ///

            UserInputsText = new Label
            {
                Location = new System.Drawing.Point(centerX-480, centerY-120),
                AutoSize = true,
                Text = "User Inputs"
            };

            DecisionText = new Label
            {
                Text = "Decision",
                Location = new System.Drawing.Point(centerX+300, centerY-50),
                AutoSize = true

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
            FacialAnalysisTickBox = new Label
            {
                Location = new System.Drawing.Point(centerX-100, centerY-50),
                AutoSize = true

            };

            /////
            ///PasswordMatchTickBox
            ///

            PasswordMatchTickBox = new Label
            {
                Location = new System.Drawing.Point(centerX - 100, centerY - 250),
                AutoSize = true

            };
            
            /////
            ///KeystrokeAnalysisTickBox
            ///

            KeystrokeAnalysisTickBox = new Label
            {
                Location = new System.Drawing.Point(centerX - 100, centerY + 150),
                AutoSize = true

            };

            DecisionTickBox = new Label
            {
                Location = new System.Drawing.Point(centerX+250, centerY-50),
                AutoSize = true
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

            UserInputsChangingLabel = new Label
            {
                Location = new System.Drawing.Point(centerX-480 , centerY - 80),
                AutoSize = true

            };

            DecisionChangingLabel = new Label
            {
                Location = new System.Drawing.Point(centerX+300, centerY-20),
                AutoSize = true
            };

            MetroKeystrokeAnalysisChangingLabel = new Label
            {
                Location = new System.Drawing.Point(centerX-50, centerY + 180),
                AutoSize = true

            };
            FacialAnalysisChangingLabel = new Label
            {
                Location = new System.Drawing.Point(centerX-50, centerY -20),
                AutoSize = true

            };


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
            this.Controls.Add(FacialAnalysisChangingLabel);
            this.Controls.Add(DecisionTickBox);
        }

        private void ApplyFontSettings()
        {
            if (AccessibilitySettings.Font == "OpenDyslexic 3")
            {
                Font font = new Font(AuthenticationModel.OpenDyslexic.Families[0], AccessibilitySettings.FontSize);
                this.Font = font;
                ApplyFontToControls(this.Controls, font);
            }
            else
            {
                Font font = new Font(AccessibilitySettings.Font, AccessibilitySettings.FontSize);
                this.Font = font;
                ApplyFontToControls(this.Controls, font);
            }
        }

        private void ApplyFontToControls(Control.ControlCollection controls, Font font)
        {
            foreach (Control control in controls)
            {
                control.Font = font;
                if (control.Controls.Count > 0)
                {
                    ApplyFontToControls(control.Controls, font);
                }
            }
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
                    if (float.Parse(data) >= 60)
                    {
                        FacialAnalysisTickBox.Text = "✔";
                        FacialAnalysisChangingLabel.Text = data + "%";

                    }
                    else
                    {
                        FacialAnalysisTickBox.Text = "✘";
                        float percentage = float.Parse(data) * 100;
                        FacialAnalysisChangingLabel.Text = percentage.ToString() + "^";
                    }

                    break;
                case "KeystrokeAnalysisTickBox":
                    if (float.Parse(data) >= 0.6)
                    {
                        KeystrokeAnalysisTickBox.Text = "✔";
                        MetroKeystrokeAnalysisChangingLabel.Text = float.Parse(data) * 100 + "%";
                    }
                    else
                    {
                        KeystrokeAnalysisTickBox.Text = "✘";
                        MetroKeystrokeAnalysisChangingLabel.Text = float.Parse(data)*100 + "%";
                    }
                    break;
                case "Decision":
                    if(float.Parse(data) > 70)
                    {
                        DecisionTickBox.Text = "✔";
                        DecisionChangingLabel.Text = data + "%";
                    }
                    else
                    {
                        DecisionChangingLabel.Text = data;
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

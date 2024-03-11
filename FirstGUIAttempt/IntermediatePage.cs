using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace FirstGUIAttempt
{
    public partial class IntermediatePage : Form
    {
        public IntermediatePage()
        {
            InitializeComponent();
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
                    }
                    else
                    {
                        KeystrokeAnalysisTickBox.Text = "✘";
                    }
                    break;
            }

        }
    }
}

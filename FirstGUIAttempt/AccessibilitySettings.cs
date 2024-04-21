using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using MahApps.Metro.Behaviors;



namespace FirstGUIAttempt
{
    public partial class AccessibilitySettings : Form
    {
        public static string Font { get; set; } = SystemFonts.DefaultFont.FontFamily.Name;
        public static float FontSize { get; set; } = SystemFonts.DefaultFont.SizeInPoints;
        private string tempFont;
        private float tempFontSize;
        private string currentFont;
        private float currentFontSize;
        private Label FontSizeStart = new Label();
        private Label FontSizeEnd = new Label();
        private Label Testlabel = new Label();
        private Label ExampleText = new Label();
       
        public AccessibilitySettings()
        {
            InitializeComponent();
            FontSizeTrackBar.Scroll += FontSizeTrackBar_Scroll;
            FontTrackBar.Scroll += FontTrackBar_Scroll;
            ExampleText.AutoSize = true;
            ExampleText.Font = new Font(Font, FontSize);
            ExampleText.Location = new Point(0, 0);
            ExampleText.Text = "The quick brown fox jumped over the lazy dog";
            this.Controls.Add(ExampleText);
            SaveAccessibility.Click += SaveAccessibility_Click;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            FontSizeStart.Text = FontSizeTrackBar.Minimum.ToString();
            FontSizeEnd.Text = FontSizeTrackBar.Maximum.ToString();
            FontSizeStart.Location = new Point(FontSizeTrackBar.Location.X-10, 100);
            FontSizeEnd.Location = new Point(FontSizeTrackBar.Location.X + FontSizeTrackBar.Width, 100);
            FontSizeStart.AutoSize = true;
            FontSizeEnd.AutoSize = true;
            this.Controls.Add(FontSizeStart);
            this.Controls.Add(FontSizeEnd);
            FontSizeTrackBar.Size = new Size(420, 10);
            this.Load += AccessibilitySettings_Load;


        }

        

        private void SaveAccessibility_Click(object sender, EventArgs e)
        {
            Font = tempFont;
            FontSize = tempFontSize;
            Console.WriteLine("Saving Font is: " + tempFont);
            Console.WriteLine("Saving Font Size is: " + tempFontSize);
            this.Close();
        }

        private void AccessibilitySettings_Load(object sender, EventArgs e)
        {

            
            currentFont = Font;
            currentFontSize = FontSize;
            tempFont = Font;
            tempFontSize = FontSize;
            Console.WriteLine("Current Font: " + currentFont);
            Console.WriteLine("Current Font Size: " + currentFontSize);



            if (currentFontSize == 8.25)
            {
                tempFontSize = 8;
                currentFontSize = 8;
            }
            FontSizeTrackBar.Value = Int32.Parse(currentFontSize.ToString());
            Console.WriteLine(currentFont);
            Console.WriteLine(tempFontSize);
            switch (currentFont)
            { 
                case "Microsoft Sans Serif":
                    FontTrackBar.Value = 1;
                    break;
                case "Comic Sans MS":
                    FontTrackBar.Value = 2;
                    break;
                case "OpenDyslexic 3":
                    FontTrackBar.Value = 3;
                    break;
                case "Impact":
                    FontTrackBar.Value = 4;
                    break;
                    
            }
            if(currentFont != "OpenDyslexic 3")
            {
                ExampleText.Font = new Font(currentFont, currentFontSize);
            }
            else
            {
                ExampleText.Font = new Font(FirstGUIAttempt.OpenDyslexic.Families[0], currentFontSize);
            }


        }
        private void FontSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            // Get the current value of the TrackBar

            int currentValue = FontSizeTrackBar.Value;
            switch (currentValue)
            {
                case 8:
                    //Fontsize is 8
                    tempFontSize = 8;
                    break;
                case 9:
                    //Font size is 9
                    tempFontSize = 9;
                    break;
                case 10:
                    //Font size is 10
                    tempFontSize = 10;
                    break;
                case 11:
                    //Font size is 11
                    tempFontSize = 11;
                    break;
                case 12:
                    //Fontsize is 12
                    tempFontSize = 12;
                    break;
                case 13:
                    //Font size is 13
                    tempFontSize = 13;
                    break;
                case 14:
                    //Font size is 14
                    tempFontSize = 14;
                    break;
                case 15:
                    //Font size is 15
                    tempFontSize = 15;
                    break;
                case 16:
                    //font size is 16
                    tempFontSize = 16;
                    break;
            };

            //Console.WriteLine("Why is the font updating? " + currentFont);
            if(tempFont != "OpenDyslexic 3")
            {
                ExampleText.Font = new Font(tempFont, tempFontSize);
            }
            else
            {
                ExampleText.Font = new Font(FirstGUIAttempt.OpenDyslexic.Families[0], tempFontSize);
            }
   
        }

        private void FontTrackBar_Scroll(object sender, EventArgs e)
        {
            // Get the current value of the TrackBar
            int currentValue = FontTrackBar.Value;
            switch (currentValue)
            {
                case 1:
                    //Fontsize is 8
                    tempFont = "Microsoft Sans Serif";
                    break;
                case 2:
                    //Font size is 9
                    tempFont = "Comic Sans MS";
                    break;
                case 3:
                    //Font size is 10
                    tempFont = "OpenDyslexic 3";
                    break;
                case 4:
                    //Font size is 11
                    tempFont = "Impact";
                    break;
               
            };


            
            if(tempFont == "OpenDyslexic 3")
            {
                ExampleText.Font= new Font(FirstGUIAttempt.OpenDyslexic.Families[0], tempFontSize);
            }
            else
            {
                ExampleText.Font = new Font(tempFont, tempFontSize);
            }
        }
    }
}

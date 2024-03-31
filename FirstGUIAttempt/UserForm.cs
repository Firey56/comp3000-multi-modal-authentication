using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstGUIAttempt
{

    public partial class UserForm : Form
    {
        private PictureBox UserPhoto = new PictureBox();
        public string UserID { get; set; }
        public string Username { get; set; }
        public string base64Image { get; set; }
        public UserForm()
        {
            InitializeComponent();



            // Set PictureBox properties
            UserPhoto.Location = new System.Drawing.Point(50, 50); // Set the position
            UserPhoto.Size = new System.Drawing.Size(200, 200); // Set the size
            UserPhoto.SizeMode = PictureBoxSizeMode.StretchImage; // Set the size mode (e.g., StretchImage)
            // Add the PictureBox to the form's Controls collection
            this.Controls.Add(UserPhoto);

        }

        //TODO The image currently doesn't show, suspect it is due to the picture box not being big enough.
        // Set other properties as needed (e.g., BackColor, BorderStyle, etc.)
        /////////////////////////////////////////////////////////////////////
        ///Added the controls, now time to actually do the page
        private void Form2_Load(object sender, EventArgs e)
        {
            // Example: Display the base64 image in a PictureBox
            if (!string.IsNullOrEmpty(base64Image))
            {
                byte[] imageBytes = Convert.FromBase64String(base64Image);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    UserPhoto.Image = Image.FromStream(ms);
                }
            }
        }

    }
    
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace FirstGUIAttempt
{
    public partial class SignInPage : Form
    {
        public SignInPage()
        {
            InitializeComponent();
        }
        string selectedFilePath = null;
        private string connectionString = "Data Source=localhost;Initial Catalog=Users;Integrated Security=True";

        private void userNameInput(object sender, EventArgs e)
        {

        }

        private void usernameLabel(object sender, EventArgs e)
        {

        }

        private void passwordLabel(object sender, EventArgs e)
        {

        }

        private void passwordInput(object sender, EventArgs e)
        {

        }

        private void SignInPage_Load(object sender, EventArgs e)
        {

        }

        private void signIn(object sender, EventArgs e)
        {
        }

        private void photoUploadLabel(object sender, EventArgs e)
        {

        }

        private void photoUpload(object sender, EventArgs e)
        {

        }

        private void photoUploadButtonClick(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.png;*.jpeg)|*.png;*.jpeg|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                    if (IsValidFile(selectedFilePath))
                    {
                        // Display the selected image
                        photoUploadButton.Image = Image.FromFile(selectedFilePath);
                        photoUploadButton.SizeMode = PictureBoxSizeMode.Zoom;
                        photoUploadLabelText.Text = selectedFilePath;
                    }
                    else
                    {
                        MessageBox.Show("Invalid file format. Please select a .png or .jpeg file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private bool IsValidFile(string filePath) //Method which is called when the user uploads a photo to check that it is of valid format.
        {
            string extension = Path.GetExtension(filePath)?.ToLower();
            return extension == ".png" || extension == ".jpeg";
        }
       
    }
 }

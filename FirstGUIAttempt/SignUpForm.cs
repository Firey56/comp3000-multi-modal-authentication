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
using static FirstGUIAttempt.Common;

namespace FirstGUIAttempt
{
    public partial class SignUpForm : Form
    {
        string selectedFilePath = null;
        string base64Image = null;
        string connectionString = "Data Source=localhost;Initial Catalog=Users;Integrated Security=True";
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void signUpUsernameTextbox(object sender, EventArgs e)
        {

        }

        private void usernameLabel(object sender, EventArgs e)
        {

        }

        private void signUpPasswordTextBox(object sender, EventArgs e)
        {

        }

        private void signUpPasswordLabel(object sender, EventArgs e)
        {

        }

        private void signUpFormPictureBox(object sender, EventArgs e)
        {

        }

        private void browseButton(object sender, EventArgs e)
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
                        //Display the selected image
                        signUpPictureBox.Image = Image.FromFile(selectedFilePath);
                        Image theImage = signUpPictureBox.Image;
                        if (theImage != null)
                        {
                            byte[] theImageData = Common.ImageToByteArray(theImage);
                            base64Image = Convert.ToBase64String(theImageData);
                        }
                        signUpPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        signUpBrowseButtonLabel.Text = selectedFilePath;
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

        private void browseButtonLabel(object sender, EventArgs e)
        {

        }

        private void submitButton(object sender, EventArgs e)
        {
            string usernameInput = signUpFormUsernameTextBox.Text;
            string passwordInput = signUpFormPasswordTextBox.Text;
            MessageBox.Show(usernameInput);
            MessageBox.Show(passwordInput);
            if (usernameInput != null && passwordInput != null && selectedFilePath != null)
            {
                InsertIntoUsers(usernameInput, passwordInput, base64Image);
            }
            else
            {
                MessageBox.Show("Please fill in all fields!");
            }

        }
        private void InsertIntoUsers(string username, string password, string base64Image)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert data into the database
                    using (SqlCommand command = new SqlCommand("INSERT INTO users (Username, Password, filePath) VALUES (@Username, @Password, @FilePath)", connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        //string newFilePath = InsertPhotoIntoLocation(username, theImageData);
                        command.Parameters.AddWithValue("@FilePath", base64Image);
                        //MessageBox.Show(@"{username}, {password}, {filePath}");
                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data inserted successfully into the database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert data into the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*        private string InsertPhotoIntoLocation(string username, byte[] imageData)
                {
                    string storagePath = @"C:\DissertationWork";
                    string savedFilePath = Path.Combine(storagePath, $"{username}.png");
                    MessageBox.Show(savedFilePath);

                    File.WriteAllBytes(savedFilePath, imageData);
                    return savedFilePath;
                }


            }
        */
    }
}
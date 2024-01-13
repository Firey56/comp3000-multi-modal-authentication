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
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Diagnostics;

namespace FirstGUIAttempt
{
    public partial class SignUpForm : Form
    {
        string selectedFilePath = null;
        string base64Image = null;
        string connectionString = "Data Source=localhost;Initial Catalog=Users;Integrated Security=True";
        static List<long> keystrokePattern = new List<long>();
        static Stopwatch keyboardTimer = new Stopwatch();
        static List<string> finalKeystrokePattern = new List<string>();
        public SignUpForm()
        {
            InitializeComponent();
            signUpFormPasswordTextBox.KeyDown += password_KeyPress;
        }

        private static void password_KeyPress(object sender, KeyEventArgs e)
        {
            //Console.WriteLine("Down");
            if (!keyboardTimer.IsRunning)
            {
                keyboardTimer.Start();
            }
            else
            {
                long currentTime = keyboardTimer.ElapsedMilliseconds;
                keystrokePattern.Add(currentTime);
            }

            // Display or process the keystroke pattern as needed
            Console.WriteLine($"Recorded: Keystroke");
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
                openFileDialog.Filter = "Image Files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All Files (*.*)|*.*";
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
            return extension == ".png" || extension == ".jpeg" || extension == ".jpg";
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
            finalKeystrokePattern.Add("Keystroke");//Initial keystroke
            long x = 0;//Set value as 0 seconds before first keystroke
            foreach (long value in keystrokePattern)
            {
                long y = value;//Sets equal to the "currentTime" variable previously added
                               //Keystroke down -> time -> keystroke down -> time -> keystroke down -> time -> keystroke down

                finalKeystrokePattern.Add((y - x).ToString());//Adds in the time difference between 
                finalKeystrokePattern.Add("Keystroke");
                x = y;//This will be our new subtraction time between keystrokes.

            }
            foreach (string value in finalKeystrokePattern)
            {
                Console.WriteLine(value);
            }
            if (usernameInput != null && passwordInput != null && selectedFilePath != null)
            {
                CreateCSV(usernameInput);
                InsertIntoUsers(usernameInput, passwordInput, base64Image);
            }
            else
            {
                MessageBox.Show("Please fill in all fields!");
            }

        }

        private void CreateCSV(string username)
        {
            string csvFilePath = @"C:\Users\alexw\OneDrive\Documents\University Work\COMP3000\Github\comp3000-multi-modal-authentication\CSV\" + username + ".csv";

            var csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
            string csvWritableFormat = string.Join(",", finalKeystrokePattern);
            Console.WriteLine(csvWritableFormat);
            using (var csvWriter = new StreamWriter(csvFilePath, append: true))
            {
                csvWriter.WriteLine(string.Join(",", csvWritableFormat));
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
                    using (SqlCommand command = new SqlCommand("INSERT INTO users (Username, Password, image) VALUES (@Username, @Password, @image)", connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        //string newFilePath = InsertPhotoIntoLocation(username, theImageData);
                        command.Parameters.AddWithValue("@image", base64Image);
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
﻿using System;
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
using System.Diagnostics;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Security.Cryptography;

namespace FirstGUIAttempt
{
    public partial class SignUpForm : Form
    {
        string base64Image = null;
        string connectionString = "Data Source=localhost;Initial Catalog=Users;Integrated Security=True";
        static List<long> keystrokePattern = new List<long>();
        static Stopwatch keyboardTimer = new Stopwatch();
        static List<string> finalKeystrokePattern = new List<string>();
        private VideoCaptureDevice videoSource;
        public SignUpForm()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
            InitializeWebcam();
            signUpFormPasswordTextBox.KeyDown += password_KeyPress;
            
        }
        private void InitializeWebcam()
        {
            signUpPictureBox.Visible = true;
            // Get the list of available video devices (webcams)
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count > 0)
            {
                // Select the first video device
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;

                // Start the video source
                videoSource.Start();
            }
            else
            {
                MessageBox.Show("No video devices found.");
            }
        }
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap originalFrame = (Bitmap)eventArgs.Frame.Clone(); //Creates a clone of the current feed.
            Size newSize = CalculateSizeToFit(originalFrame.Size, signUpPictureBox.ClientSize); //Creates a size variable of what the feed size is vs what the PictureBox is
            Bitmap resizedFrame = ResizeImage(originalFrame, newSize);//Changes the video feed to have a resolution and aspect ratio to fit the picture box.
            signUpPictureBox.Image = resizedFrame;

            // Dispose of old frame
            originalFrame.Dispose();
        }

        private Size CalculateSizeToFit(Size originalSize, Size containerSize)
        {
            int width, height;

            // Calculate the width and height to fit the original aspect ratio within the container
            if (originalSize.Width > originalSize.Height)
            {
                width = containerSize.Width;
                height = (int)(containerSize.Width * (float)originalSize.Height / originalSize.Width);
            }
            else
            {
                height = containerSize.Height;
                width = (int)(containerSize.Height * (float)originalSize.Width / originalSize.Height);
            }

            return new Size(width, height);
        }

        private Bitmap ResizeImage(Bitmap originalImage, Size newSize)
        {
            // Create a new Bitmap with the specified size
            Bitmap resizedImage = new Bitmap(newSize.Width, newSize.Height);

            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                // Maintain the aspect ratio of the original image and apply high-quality interpolation
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, newSize.Width, newSize.Height);
            }

            return resizedImage;
        }
        /// <summary>
        /// Function for stopping the video source when the form is closed.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Stop the video source when closing the form
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }

            base.OnFormClosing(e);
        }


        private static void password_KeyPress(object sender, KeyEventArgs e)
        {
            //Console.WriteLine("Down");
            //This section now requires a check if the key pressed is "Backspace".
            //If backspace clicked, must remove 2 items from list for every click.
            //Set timer to the last value in the list.
            //If CTRL + Backspace is clicked, reset timer and reset list.
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
        /// <summary>
        /// This function hashes the password using SHA256 hashing and then gets it converted to a string for storage and comparison.
        /// 
        /// This function could be further enhanced using Argon2, but would need to think about salt generation and storage.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string HashPassword(string password)
        {
            byte[] passwordAsBytes;
            byte[] calculatedHash;
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string to bytes
                passwordAsBytes= Encoding.UTF8.GetBytes(password);

                // Calculate the SHA-256 hash
                calculatedHash = sha256.ComputeHash(passwordAsBytes);

                // Convert the hash to a hexadecimal string
                string hashedInput = BitConverter.ToString(calculatedHash).Replace("-", "").ToLower();
                Console.WriteLine(hashedInput);
                //MessageBox.Show("Hashed output should be out");
                return hashedInput;
            }
        }

 

        private void submitButton(object sender, EventArgs e)
        {
            string usernameInput = signUpFormUsernameTextBox.Text;
            string hashedPassword = HashPassword(signUpFormPasswordTextBox.Text);
            //MessageBox.Show(usernameInput);
            //MessageBox.Show(passwordInput);
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
            if (usernameInput != null && hashedPassword != null && base64Image != null)
            {
                CreateCSV(usernameInput);
                InsertIntoUsers(usernameInput, hashedPassword, base64Image);
            }
            else
            {
                MessageBox.Show("Please fill in all fields!");
            }

        }

        private void CreateCSV(string username)
        {
            string csvFilePath = @"C:\Users\alexw\OneDrive\Documents\University Work\COMP3000\Github\comp3000-multi-modal-authentication\CSV\" + username + ".csv"
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

        private void takePhoto(object sender, EventArgs e)
        {
            if (signUpPictureBox.Image != null)
            {
                // Capture the current image from the PictureBox
                Bitmap capturedImage = (Bitmap)signUpPictureBox.Image.Clone();
                videoSource.SignalToStop();
                videoSource.WaitForStop();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Save the Bitmap to the MemoryStream
                    capturedImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                    // Convert the MemoryStream to a byte array
                    byte[] byteArray = memoryStream.ToArray();

                    // Convert the byte array to a Base64 string
                    base64Image = Convert.ToBase64String(byteArray);
                }
                // Dispose the captured image
                capturedImage.Dispose();
            }
            else
            {
                MessageBox.Show("No image to capture. Ensure the webcam is providing a video stream.");
            }
        }

        private void signUpUsernameTextbox(object sender, EventArgs e)
        {

        }


        private void signUpPasswordTextBox(object sender, EventArgs e)
        {
        }


        private void signUpFormPictureBox(object sender, EventArgs e)
        {

        }
    }

}
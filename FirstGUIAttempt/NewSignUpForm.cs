using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;

namespace FirstGUIAttempt
{
    public partial class NewSignUpForm : Form
    {
        private Label UsernameLabel = new Label();
        private TextBox UsernameTextBox = new TextBox();
        private Label PasswordLabel = new Label();
        private TextBox PasswordTextBox = new TextBox();
        private Label PhotoLabel = new Label();
        private PictureBox PhotoBox = new PictureBox();
        private Button TakePhotoButton = new Button();
        private Button SubmitButton = new Button();
        private VideoCaptureDevice videoSource;
        private LinkLabel AccessibilitySettingsText = new LinkLabel();

        string base64Image = null;
        private string connectionString = "Data Source=localhost;Initial Catalog=Users;Integrated Security=True";
        //private string connectionString = "Server=tcp:finalyearproject.database.windows.net,1433;Initial Catalog=MultiModalAuthentication;Persist Security Info=False;User ID=finalyearprojectadmin;Password=h2B&e3Hvs$%bDsk@Vgp4Yf5&F;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //private string connectionString = "Server=dissi-database.c32y6sk2evqy.eu-west-2.rds.amazonaws.com;Database=Dissertation;User ID=admin;Password=V4F^E2Tt#M#p#bjj;Encrypt=true;TrustServerCertificate=true;Connection Timeout=30;";
        readonly static List<long> keystrokePattern = new List<long>();
        readonly static Stopwatch keyboardTimer = new Stopwatch();
        readonly static List<string> finalKeystrokePattern = new List<string>();


        public NewSignUpForm()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
            ApplyFontSettings();
            InitializeWebcam();
            TakePhotoButton.Click += takePhoto;
            SubmitButton.Click += submit;
            PasswordTextBox.KeyDown += password_KeyPress;


            this.ClientSize = new Size(600, 600);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            /////////////////////////
            ///Username Label work
            /////////////////////////
            ///
            UsernameLabel.Location = new Point(125,30);
            UsernameLabel.AutoSize = true;
            UsernameLabel.Text = "Username";
            //UsernameLabel.Font = new Font("Arial", 8);
            this.Controls.Add(UsernameLabel);



            UsernameTextBox.Location = new Point(130, 60);
            UsernameTextBox.Size = new Size(300, 40);
            UsernameTextBox.BringToFront();
            this.Controls.Add(UsernameTextBox);

            PasswordLabel.Location = new Point(125, 170);
            PasswordLabel.AutoSize = true;
            PasswordLabel.Text = "Password";
            //UsernameLabel.Font = new Font("Arial", 8);
            this.Controls.Add(PasswordLabel);

            PasswordTextBox.Location = new Point(130, 200);
            PasswordTextBox.Size = new Size(300, 40);
            PasswordTextBox.BringToFront();
            PasswordTextBox.PasswordChar = '*';
            this.Controls.Add(PasswordTextBox);

            PhotoLabel.Location = new Point(130, 250);
            PhotoLabel.AutoSize = true;
            PhotoLabel.Text = "Photo";
            this.Controls.Add(PhotoLabel);

            TakePhotoButton.Location = new Point(200, 250);
            TakePhotoButton.AutoSize = true;
            TakePhotoButton.Text = "Take Photo";
            this.Controls.Add(TakePhotoButton);

            PhotoBox.Location = new Point(180, 300);
            PhotoBox.Size = new Size(200, 130);
            this.Controls.Add(PhotoBox);

            SubmitButton.Location = new Point(240, 520);
            SubmitButton.AutoSize = true;
            SubmitButton.Text = "Submit";
            this.Controls.Add(SubmitButton);
        }

        private void submit(object sender, EventArgs e)
        {
            string usernameInput = UsernameTextBox.Text;
            string hashedPassword = HashPassword(PasswordTextBox.Text);
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
                //CreateCSV(usernameInput);
                if (InputSanitisation(usernameInput) == true)
                {
                    InsertIntoUsers(usernameInput, hashedPassword, base64Image);

                }
                else
                {
                    MessageBox.Show("Input has not passed data sanitisation. There is an attempt at an exploit.");
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields!");
            }

        }

        public bool InputSanitisation(string checkUsername)
        {
            ///////////////////////////////////////////////////////////////////////////////
            ///Series of patterns to check for input validation
            ///////////////////////////////////////////////////////////////////////////////
            string sqlInjectionPattern = @"(\b(ALTER|CREATE|DELETE|DROP|EXEC(UTE)?|INSERT( INTO)?|MERGE|SELECT|UPDATE|UNION( ALL)?)\b)|(--.*$)";
            string xssPattern = @"<script\b[^<]*(?:(?!</script>)<[^<]*)*</script>";
            string pathTraversalPattern = @"(\.\./|\/\.\.|\\\/\\\.\.|\\.\.|file:\/\/)";
            string htmlInjectionPattern = @"(<|>|&|%3C|%3E|%26)";
            string commandInjectionPattern = @"(&|\||;|`|\\|\$\(|\)|\{|\})";
            string ldapInjectionPattern = @"(\*|\(|\)|\x00)";

            // Combine all patterns into a single regex pattern
            string combinedPattern = string.Join("|", sqlInjectionPattern, xssPattern, pathTraversalPattern, htmlInjectionPattern, commandInjectionPattern, ldapInjectionPattern);

            // Create a Regex object with the combined pattern
            Regex regex = new Regex(combinedPattern, RegexOptions.IgnoreCase);

            // Check if the user input matches any of the patterns
            if (regex.IsMatch(checkUsername))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void InsertIntoUsers(string username, string password, string base64Image)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    bool success = false;
                    // Insert data into the database
                    //using (SqlCommand command = new SqlCommand("INSERT INTO users (Username, Password, image) VALUES (@Username, @Password, @image)", connection))
                    using (SqlCommand command = new SqlCommand("Authentication.UserSignUp", connection))
                    {
                        Console.WriteLine("We are inside the command.");
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        //string newFilePath = InsertPhotoIntoLocation(username, theImageData);
                        command.Parameters.AddWithValue("@Image", base64Image);

                        //MessageBox.Show(@"{username}, {password}, {filePath}");
                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            success = true;
                            MessageBox.Show("Data inserted successfully into the database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("Failed to insert data into the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    if (success)
                    {
                        InsertKeystrokes(username, 0);
                    }
                    connection.Close();
                    //TODO If unable to get database trigger to work, create stored procedure that creates the table using existing username variable
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                passwordAsBytes = Encoding.UTF8.GetBytes(password);

                // Calculate the SHA-256 hash
                calculatedHash = sha256.ComputeHash(passwordAsBytes);

                // Convert the hash to a hexadecimal string
                string hashedInput = BitConverter.ToString(calculatedHash).Replace("-", "").ToLower();
                Console.WriteLine(hashedInput);
                //MessageBox.Show("Hashed output should be out");
                return hashedInput;
            }
        }



        /*
        /// <summary>
        /// Creates a CSV file for when user creates an account.
        /// </summary>
        /// <param name="username"></param>
        private void CreateCSV(string username)
        {
            string csvFilePath = @"C:\Users\alexw\OneDrive\Documents\University Work\COMP3000\Github\comp3000-multi-modal-authentication\CSV\" + username + ".csv";
            string csvWritableFormat = string.Join(",", finalKeystrokePattern);
            Console.WriteLine(csvWritableFormat);
            using (var csvWriter = new StreamWriter(csvFilePath, append: true))
            {
                csvWriter.WriteLine(string.Join(",", csvWritableFormat));
            }
            
        }
        */


        private void takePhoto(object sender, EventArgs e)
        {
            if (PhotoBox.Image != null)
            {
                // Capture the current image from the PictureBox
                Bitmap capturedImage = (Bitmap)PhotoBox.Image.Clone();
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
        private void InsertKeystrokes(string Username, int Expected)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert data into the database
                    //using (SqlCommand command = new SqlCommand("INSERT INTO users (Username, Password, image) VALUES (@Username, @Password, @image)", connection))
                    using (SqlCommand command = new SqlCommand("Authentication.InsertKeystrokesIntoDynamicTable", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TableName", Username); ;
                        string csv = string.Join(",", finalKeystrokePattern);
                        command.Parameters.AddWithValue("@Keystrokes", csv);
                        command.Parameters.AddWithValue("@Expected", 0);
                        //*MessageBox.Show(@"{username}, {password}, {filePath}");
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
    private void ApplyFontSettings()
        {
            Font font = new Font(AccessibilitySettings.Font, AccessibilitySettings.FontSize);
            this.Font = font;
            ApplyFontToControls(this.Controls, font);
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

        private void InitializeWebcam()
        {
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
            Size newSize = CalculateSizeToFit(originalFrame.Size, PhotoBox.ClientSize); //Creates a size variable of what the feed size is vs what the PictureBox is
            Bitmap resizedFrame = ResizeImage(originalFrame, newSize);//Changes the video feed to have a resolution and aspect ratio to fit the picture box.
            PhotoBox.Image = resizedFrame;

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
    }
}

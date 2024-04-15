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
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Collections;
using System.Security.Cryptography;
using System.Threading;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace FirstGUIAttempt
{
    public partial class NewSignInForm : Form
    {
        private System.Windows.Forms.Label UsernameLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label PasswordLabel = new System.Windows.Forms.Label();
        private TextBox UsernameTextBox = new TextBox();
        private TextBox PasswordTextBox = new TextBox();
        private PictureBox PhotoBox = new PictureBox();
        private Button SubmitButton = new Button();

        private VideoCaptureDevice videoSource;
        private string connectionString = "Data Source=localhost;Initial Catalog=Users;Integrated Security=True";
        //private string connectionString = "Server=tcp:finalyearproject.database.windows.net,1433;Initial Catalog=MultiModalAuthentication;Persist Security Info=False;User ID=finalyearprojectadmin;Password=h2B&e3Hvs$%bDsk@Vgp4Yf5&F;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //private string connectionString = "Server=dissi-database.c32y6sk2evqy.eu-west-2.rds.amazonaws.com;Database=Dissertation;User ID=admin;Password=V4F^E2Tt#M#p#bjj;Encrypt=true;TrustServerCertificate=true;Connection Timeout=30;";
        List<string> comparisonImageBase64 = new List<string>();
        static List<long> keystrokePattern = new List<long>();
        static Stopwatch keyboardTimer = new Stopwatch();
        bool pasteFlag = false;
        int photoCount = 0;
        static Stopwatch photoStopwatch = new Stopwatch();
        List<string> finalKeystrokePattern = new List<string>();
        RealTimeForm goToRealTime = new RealTimeForm();
        bool WebcamAvailable = false;
        public NewSignInForm()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            InitializeComponent();
            ApplyFontSettings();
            InitializeWebcam();
            PasswordTextBox.KeyDown += Password_KeyPress;
            PasswordTextBox.TextChanged += PasswordTextBox_TextChanged;
            SubmitButton.Click += Submit;

            this.ClientSize = new Size(600, 500);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;


            UsernameLabel.Location = new System.Drawing.Point(125, 80);
            UsernameLabel.AutoSize = true;
            UsernameLabel.Text = "Username";
            this.Controls.Add(UsernameLabel);
            UsernameTextBox.Location = new System.Drawing.Point(130, 110);
            UsernameTextBox.Size = new Size(300, 40);
            this.Controls.Add(UsernameTextBox);


            PasswordLabel.Location = new System.Drawing.Point(125, 220);
            PasswordLabel.AutoSize = true;
            PasswordLabel.Text = "Password";
            this.Controls.Add(PasswordLabel);
            PasswordTextBox.Location = new System.Drawing.Point(130, 250);
            PasswordTextBox.Size = new Size(300, 40);
            PasswordTextBox.PasswordChar = '*';
            this.Controls.Add(PasswordTextBox);


            PhotoBox.Location = new System.Drawing.Point(180, 300);
            PhotoBox.Size = new Size(200, 130);
            this.Controls.Add(PhotoBox);


            SubmitButton.Location = new System.Drawing.Point(240, 440);
            SubmitButton.AutoSize = true;
            SubmitButton.Text = "Submit";
            this.Controls.Add(SubmitButton);
        }
        private void Submit(object sender, EventArgs e)
        {
            UserSubmit();
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

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {

                if (Clipboard.GetText() == PasswordTextBox.Text)
                {
                    //pasteFlag = true;
                    Console.WriteLine("Pasted info and clipboard are the same");
                }
            }
        }

        private void UserSubmit()
        {
            string usernameInput = UsernameTextBox.Text;
            string hashedPassword = HashPassword(PasswordTextBox.Text);
            //Makes the list of the timings of the keystroke
            //Our new list with keystrokes + timings
            if (finalKeystrokePattern.Count == 0)
            {
                finalKeystrokePattern.Add("Keystroke");//Initial keystroke
                long x = 0;//Set value as 0 seconds before first keystroke
                foreach (long value in keystrokePattern)
                {
                    long y = value;//Sets equal to the "currentTime" variable previously added
                                   //! Keystroke down -> time -> keystroke down -> time -> keystroke down -> time -> keystroke down

                    finalKeystrokePattern.Add((y - x).ToString());//Adds in the time difference between 
                                                                  //finalKeystrokePattern.Add(value.ToString());
                    finalKeystrokePattern.Add("Keystroke");
                    x = y;//This will be our new subtraction time between keystrokes.

                }
            }
            if (usernameInput != null && hashedPassword != null && comparisonImageBase64 != null)
            {
                // MessageBox.Show("We are inside the SubmitButton function");
                foreach (string value in finalKeystrokePattern)
                {
                    Console.WriteLine($"{value}");
                }
                //CallPython();
                if (InputSanitisation(usernameInput) == true)
                {
                    goToRealTime = new RealTimeForm();
                    this.Hide();
                    goToRealTime.Show();
                    string allUserInputs = "Password Correct \n" + "Photos Supplied: " + photoCount.ToString() + "\nKeystroke Analysis: Processing...";
                    goToRealTime.UpdateLabelsForLogin("UserInputs", allUserInputs);
                    //this.Hide();
                    UserSignIn(usernameInput, hashedPassword);
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
        async Task<float> RunMachineLearningProcessAsync()
        {
            // Your code to run the machine learning process asynchronously
            // For example:
            float estimate = 0;
            await Task.Run(() =>
            {
                // Code to execute the machine learning process
                // This could involve calling a Python script, invoking an external process, etc.
                // Ensure to properly handle any exceptions or errors
                string pythonInterpreter = "python";
                string pythonScript = "../../../MachineLearningModel.py";
                string csv = string.Join(",", finalKeystrokePattern);

                // Prepare process start information
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = pythonInterpreter;
                start.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\"", pythonScript, UsernameTextBox.Text, csv);
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.CreateNoWindow = true;

                // Start the process
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();

                        ///////////////////////////////////////////////////////////////////////////
                        ///The result we get from this includes a file path and then the value we want.
                        ///We need to parse out the file path.
                        ///////////////////////////////////////////////////////////////////////////
                        string[] outputLines = result.Split('\n');//Split on new line
                        string numericalValue = outputLines[outputLines.Length - 2].Trim(); // Remove the previous values from the array

                        // Parse the numerical value
                        if (!string.IsNullOrEmpty(result))//Ensures we have a number
                        {
                            if (float.TryParse(result, out estimate))
                            {
                                Console.WriteLine("Estimate from Python script: " + estimate);
                                // Use the estimate in further processing
                            }
                        }
                        else
                        {
                            Console.WriteLine("No numerical value received from Python script");
                        }
                    }
                }
            });
            return estimate;
        }
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
                //Console.WriteLine(hashedInput);
                //*MessageBox.Show("Hashed output should be out");
                return hashedInput;
            }
        }
        private void InitializeWebcam()
        {
            // Get the list of available video devices (webcams)
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count > 0)
            {
                WebcamAvailable = true;
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

        /// <summary>
        /// This function puts the video feed into the PictureBox.
        /// The image is resized to ensure that the whole video is in the box and centered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap originalFrame = (Bitmap)eventArgs.Frame.Clone(); //Creates a clone of the current feed.
            Size newSize = CalculateSizeToFit(originalFrame.Size, PhotoBox.ClientSize); //Creates a size variable of what the feed size is vs what the PictureBox is
            Bitmap resizedFrame = ResizeImage(originalFrame, newSize);//Changes the video feed to have a resolution and aspect ratio to fit the picture box.
            PhotoBox.Image = resizedFrame;

            // Dispose of old frame
            originalFrame.Dispose();
        }

        /// <summary>
        /// This function checks the width and height of both the PictureBox and the video feed and changes the aspect ratio
        /// to fit the PictureBox
        /// </summary>
        /// <param name="originalSize"></param>
        /// <param name="containerSize"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This function resizes the image with the aspect ratio that has previously been calculated.
        /// </summary>
        /// <param name="originalImage"></param>
        /// <param name="newSize"></param>
        /// <returns></returns>
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
        private void TakePhoto()
        {
            //*Console.WriteLine("Inside Take Photo");
            if (photoCount <= 5)
            {
                //*Console.WriteLine("inside photo count");
                if (PhotoBox.Image != null)
                {
                    if (!photoStopwatch.IsRunning)
                    {
                        photoStopwatch.Start();
                        //*Console.WriteLine("stopwatch is started");
                    }
                    //*Console.WriteLine("Image is being displayedc");
                    //*long temp = keyboardTimer.ElapsedMilliseconds;
                    //*Console.WriteLine("stopwtach has been created");

                    //*Console.WriteLine(tempStopwatch.Elapsed.ToString());
                    //*Console.WriteLine("Stopwatch is running");
                    //*Console.WriteLine(tempStopwatch.ElapsedMilliseconds.ToString());

                    photoStopwatch.Restart();
                    // Capture the current image from the PictureBox
                    Bitmap capturedImage = (Bitmap)PhotoBox.Image.Clone();
                    //*videoSource.SignalToStop();
                    //*videoSource.WaitForStop();

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // Save the Bitmap to the MemoryStream
                        capturedImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                        // Convert the MemoryStream to a byte array
                        byte[] byteArray = memoryStream.ToArray();

                        // Convert the byte array to a Base64 string
                        comparisonImageBase64.Add(Convert.ToBase64String(byteArray));
                        //*Console.WriteLine("Photo taken " + photoCount);
                        //*Console.WriteLine();
                        photoCount++;
                        // Dispose the captured image
                        capturedImage.Dispose();

                    }
                }

                else
                {
                    MessageBox.Show("No image to capture. Ensure the webcam is providing a video stream.");
                }

            }
        }

        /// <summary>
        /// This function triggers whenever a key is detected in down state in the textPassword box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Password_KeyPress(object sender, KeyEventArgs e)
        {

            //Console.WriteLine("Down");
            if (!keyboardTimer.IsRunning)
            {
                keyboardTimer.Start();
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    UserSubmit();
                }
                if (e.KeyCode == Keys.Back)
                {//TODO make sure this is changed so that it doesn't crash if they press backspace when empty
                    keystrokePattern.Clear();
                    PasswordTextBox.Text = "";
                    keyboardTimer.Stop();
                    keyboardTimer.Reset();
                    keystrokePattern.Clear();
                    finalKeystrokePattern.Clear();

                    //keystrokePattern.Add(keyboardTimer.ElapsedMilliseconds);
                }
                else
                {
                    long currentTime = keyboardTimer.ElapsedMilliseconds;
                    keystrokePattern.Add(currentTime);
                }


                //keyboardTimer.Reset();
            }
            if (keystrokePattern.Count == 1 || photoStopwatch.ElapsedMilliseconds >= 100)
            {
                TakePhoto();
            }


            // Display or process the keystroke pattern as needed
            //*Console.WriteLine($"Recorded: Keystroke");
        }




        /*private static void password_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Up");
        }
        */



        /// <summary>
        /// This section is used to actually complete the user sign in. It establishes a database connection
        /// and selects all the data available for the user. This will be their UID, username, hashed password and b64 of the image.
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="comparisonImage"></param>
        /// 
        private async Task<float> CalculateFacialScoreAverage(string databaseImage)
        {
            List<float> allSimilarities = new List<float>(); //This is for all the photos taken
            if (comparisonImageBase64.Count > 0) //This should always be true
            {
                foreach (string value in comparisonImageBase64)
                {
                    Task<float> executeFacialComparison = FacialComparison(databaseImage, value);
                    float currentSimilarity = await executeFacialComparison;
                    Console.WriteLine("The current similarity is: " + currentSimilarity);
                    //float currentSimilarity = await executeFacialComparison;
                    allSimilarities.Add(currentSimilarity);

                }
                float totalSimilarity = 0; //This starts calculating the average
                foreach (float value in allSimilarities)
                {
                    totalSimilarity += value;
                }
                return totalSimilarity / allSimilarities.Count;
                ////////////////////////////////////////////////////////////////////////
                ///End of the calculation
                ///////////////////////////////////////////////////////////////////////
            }
            else
            {
                //Code for when there are no face matches.
                MessageBox.Show("No face found in provided images.");
            }
            return 0;
        }
        private async void UserSignIn(string username, string password)
        {
            //MessageBox.Show("We are inside the UserSignIn Function");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int UserID;
                //MessageBox.Show("Connection Opened");
                //TODO MAKE THIS A STORED PROCEDURE
                string sqlQuery = "SELECT * FROM Authentication.Users WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    //Add parameters to the command
                    command.Parameters.AddWithValue("@Username", username);
                    //MessageBox.Show("Parameters set.");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //MessageBox.Show("DataReader executed");
                        // Check if there are rows returned from the query
                        if (reader.HasRows)
                        {
                            //MessageBox.Show("Has Rows!");
                            // Iterate through the result set using the SqlDataReader
                            while (reader.Read())
                            {
                                //MessageBox.Show("Now assigning values");
                                //The reader object stores the database values as their headers

                                //This section can be probably changed to be a stored procedure to be more secure.
                                string databaseUsername = reader["Username"].ToString();
                                string databasePassword = reader["Password"].ToString();
                                string databaseImage = reader["Image"].ToString();
                                int databaseUserType = Int32.Parse(reader["UserType"].ToString());
                                UserID = Int32.Parse(reader["UserID"].ToString());
                                if (databasePassword == password)
                                {
                                    goToRealTime.UpdateLabelsForLogin("PasswordMatchTickBox", "true");
                                    Task<float> calculateAverageTask = CalculateFacialScoreAverage(databaseImage);
                                    float averageFacialAnalysisScore = await calculateAverageTask;
                                    goToRealTime.UpdateLabelsForLogin("FacialAnalysisTickBox", averageFacialAnalysisScore.ToString());
                                    float keystrokeAnalysisConfidence = 1;
                                    //MessageBox.Show("Passwords matched");

                                    //!We want all forms of authentication to occur, so we can call all three functions here and then decide later.
                                    //TODO FaceMatch Function
                                    //TODO Keystroke Function
                                    //TODO Face Liveness Function
                                    //long confidenceScore = KeystrokeAnalysis(UserID)
                                    if (pasteFlag == true)
                                    {
                                        //TODO This needs to make sure the confidence score is lowered as user pasted in password
                                        keystrokeAnalysisConfidence = 0;
                                        goToRealTime.UpdateLabelsForLogin("KeystrokeAnalysisTickBox", keystrokeAnalysisConfidence.ToString());


                                    }
                                    if (keystrokeAnalysisConfidence == 1)
                                    {
                                        //Task<float> machineLearningTask = RunMachineLearningProcessAsync();
                                        //keystrokeAnalysisConfidence = await machineLearningTask
                                        goToRealTime.UpdateLabelsForLogin("KeystrokeAnalysisTickBox", keystrokeAnalysisConfidence.ToString());
                                        string userInputs = "Password Correct \n" + "Photos Supplied: " + photoCount.ToString() + "\nKeystroke Analysis: Complete";
                                        goToRealTime.UpdateLabelsForLogin("UserInputs", userInputs);
                                        //keystrokeAnalysisConfidence = 90;
                                    }

                                    /////////////////////////////////////////////////////////////////////////////
                                    ///At this point we have all of our similarity scores, so we are able to calculate our confidence score
                                    /////////////////////////////////////////////////////////////////////////////
                                    ///
                                    float finalConfidence = ((float)(averageFacialAnalysisScore * 0.5) + (float)(keystrokeAnalysisConfidence * 100 * 0.5));
                                    Console.WriteLine("Final Confidence: " + finalConfidence);
                                    goToRealTime.UpdateLabelsForLogin("Decision", finalConfidence.ToString());
                                    //MessageBox.Show(highestSimilarity.ToString());
                                    if (finalConfidence >= 0.8)
                                    {
                                        //TODO Need a conditional here, check whether the UserType is a 0 or 1 to define what page is openeed next
                                        if (databaseUserType == 1)
                                        {
                                            AdminForm AdminPage = new AdminForm();
                                            AdminPage.Show();
                                        }
                                        else
                                        {
                                            UserForm RegularUserPage = new UserForm("Regular");
                                            RegularUserPage.UserType = databaseUserType.ToString();
                                            RegularUserPage.Username = databaseUsername;
                                            RegularUserPage.base64Image = databaseImage;
                                            RegularUserPage.Show();
                                        }
                                        MessageBox.Show("You have successfully logged in.");

                                        if (keystrokeAnalysisConfidence > 0.1)//TODO This needs to be changed to a higher value, this is just temporary to get some values in.
                                        {
                                            InsertKeystrokes(username, 0);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Password is incorrect");
                                    //Code for when password is incorrect
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No records found for the given ID.");
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Facial Comparison Function.
        /// Takes the two images, one stored in database for user and the one that is being uploaded and compares them
        /// Both images are converted into MemoryStreams from Base64 and sent to AWS Facial Rekognition.
        /// This sends back a similiarty % which can be then used for furhter processing.
        /// </summary>
        /// <param name="databaseImage"></param>
        /// <param name="comparisonImage"></param>
        //This function is called when using AWS API.
        async Task<float> FacialComparison(string databaseImage, string comparisonImage)
        {
            float highestValue = 0;
            await Task.Run(() =>
            {
                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("AKIAQ3EGUMLMQTICJUPB", "iRLnUHYMcr88EwSWMzW6iFEUiimGuDRFf1q9eYDI");

                var rekognitionClient = new AmazonRekognitionClient(awsCredentials, RegionEndpoint.GetBySystemName("eu-west-2"));

                var dbImage = new MemoryStream(Convert.FromBase64String(databaseImage));//Create Image Stream for DB Image
                var uploadImage = new MemoryStream(Convert.FromBase64String(comparisonImage));//Create Image Stream for Uploaded Image

            //*MessageBox.Show("Image memory streams created");
                var compareFacesRequest = new CompareFacesRequest
                {
                    SourceImage = new Amazon.Rekognition.Model.Image
                    {
                        Bytes = new MemoryStream(dbImage.ToArray())
                    },
                    TargetImage = new Amazon.Rekognition.Model.Image
                    {
                        Bytes = new MemoryStream(uploadImage.ToArray())
                    },
                    SimilarityThreshold = 0,
                };
            // Call Amazon Rekognition API to compare faces

            //MessageBox.Show("Calling the API");
                CompareFacesResponse compareFacesResponse = rekognitionClient.CompareFaces(compareFacesRequest);
            //List<float> allFaceMatchSimilarities = new List<float>();
            // Process the response
                if (compareFacesResponse.FaceMatches.Count > 0)
                {

                    foreach (var faceMatch in compareFacesResponse.FaceMatches) //Uses a foreach incase multiple people are in frame
                    {
                        if (highestValue <= faceMatch.Similarity)
                        {
                            highestValue = faceMatch.Similarity;
                        }
                        Console.WriteLine("Inside of FaceComparison Function: " + faceMatch.Similarity);
                        return highestValue;
                    //faceMatchSimilarities.Add(faceMatch.Similarity);
                    //MessageBox.Show("Similarity:" + faceMatch.Similarity + "%");
                    //Console.WriteLine($"Similarity: {faceMatch.Similarity}%");
                    }
                }
                else
                {
                    MessageBox.Show("Please use a photo with your face in it.");
                    return highestValue;
                }
                return highestValue;
            }
            );
            return highestValue;
        }

        //MessageBox.Show("Now inside the facial comparison function");



        /// <summary>
        /// This function inserts values into the Keystroke Pattern table. This will always be executed on user login attempt.
        /// It takes 3 arguments, UserID (int), Keystrokes (string list) and Expected (bool).
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Expected"></param>
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
    }
}



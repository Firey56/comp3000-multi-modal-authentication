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

namespace FirstGUIAttempt
{
    public partial class SignInPage : Form
    {
        public SignInPage()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
            InitializeWebcam();
            passwordInputTextBox.KeyDown += password_KeyPress;
            passwordInputTextBox.TextChanged += passwordInput_TextChanged;
            //passwordInputTextBox.KeyUp += password_KeyUp;
        }
        private VideoCaptureDevice videoSource;
        private string connectionString = "Data Source=localhost;Initial Catalog=Users;Integrated Security=True";
        List<string> comparisonImageBase64 = new List<string>();
        static List<long> keystrokePattern = new List<long>();
        static Stopwatch keyboardTimer = new Stopwatch();
        bool pasteFlag = false;
        int photoCount = 0;
        static Stopwatch photoStopwatch = new Stopwatch();
        int loginAttempt = 0;


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

        private void passwordInput_TextChanged(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                
                if(Clipboard.GetText() == passwordInputTextBox.Text)
                {
                    pasteFlag = true;
                    Console.WriteLine("Pasted info and clipboard are the same");
                }
            }
        }
        /// <summary>
        /// Function for when the user clicks "Take Photo".
        /// This creates a bitmap of the user image and then converts this into a base64 string for face comparison.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void takePhotoButton_Click(object sender, EventArgs e)
        {
            // Check if there is an image in the PictureBox
            if (photoUploadButton.Image != null)
            {
                // Capture the current image from the PictureBox
                Bitmap capturedImage = (Bitmap)photoUploadButton.Image.Clone();
                videoSource.SignalToStop();
                videoSource.WaitForStop();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Save the Bitmap to the MemoryStream
                    capturedImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                    // Convert the MemoryStream to a byte array
                    byte[] byteArray = memoryStream.ToArray();

                    // Convert the byte array to a Base64 string
                    comparisonImageBase64.Add(Convert.ToBase64String(byteArray));
                }

                // Dispose the captured image
                capturedImage.Dispose();
            }
            else
            {
                MessageBox.Show("No image to capture. Ensure the webcam is providing a video stream.");
            }
        }
        /// <summary>
        /// This section assigns our variables from the user input text and checks if they've been input
        /// </summary>
        private void submit(object sender, EventArgs e) 
        {
            string usernameInput = usernameInputTextBox.Text;
            string hashedPassword = HashPassword(passwordInputTextBox.Text);

            
            //Makes the list of the timings of the keystroke
            List<string> finalKeystrokePattern = new List<string>();//Our new list with keystrokes + timings
            finalKeystrokePattern.Add("Keystroke");//Initial keystroke
            long x = 0;//Set value as 0 seconds before first keystroke
            foreach (long value in keystrokePattern)
            { 
                long y = value;//Sets equal to the "currentTime" variable previously added
                //Keystroke down -> time -> keystroke down -> time -> keystroke down -> time -> keystroke down
                
                finalKeystrokePattern.Add((y-x).ToString());//Adds in the time difference between 
                finalKeystrokePattern.Add("Keystroke");
                x = y;//This will be our new subtraction time between keystrokes.

            }
            foreach(string value in finalKeystrokePattern)
            {
                Console.WriteLine(value);
            }
            //MessageBox.Show(usernameInput);
            //MessageBox.Show(passwordInput);
            if (usernameInput != null && hashedPassword != null && comparisonImageBase64 != null)
            {
                //MessageBox.Show("We are inside the SubmitButton function");
                UserSignIn(usernameInput, hashedPassword, comparisonImageBase64);
            }
            else
            {
                MessageBox.Show("Please fill in all fields!");
            }
        }

       

        /// <summary>
        /// This section is used to actually complete the user sign in. It establishes a database connection
        /// and selects all the data available for the user. This will be their UID, username, hashed password and b64 of the image.
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="comparisonImage"></param>
        private void UserSignIn(string username, string password, string comparisonImage)
        {
            //MessageBox.Show("We are inside the UserSignIn Function");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //MessageBox.Show("Connection Opened");
                string sqlQuery = "SELECT * FROM Users WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Username", username);
                    //MessageBox.Show("Parameters set.");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //MessageBox.Show("DataReader executed");
                        // Check if there are rows returned from the query
                        if (reader.HasRows)
                        {
                           // MessageBox.Show("Has Rows!");
                            // Iterate through the result set using the SqlDataReader
                            while (reader.Read())
                            {
                               // MessageBox.Show("Now assigning values");
                               //The reader object stores the database values as their headers

                                //This section can be probably changed to be a stored procedure to be more secure.
                                string databaseUsername = reader["Username"].ToString();
                                string databasePassword = reader["Password"].ToString();
                                string databaseImage = reader["image"].ToString();
                                if(databasePassword == password)
                                {
                                    //MessageBox.Show("Passwords matched");
                                    List<float> similarity = new List<float>(FacialComparison(databaseImage, comparisonImage));
                                    float totalSimilarity = 0;
                                    if(similarity.Count > 0)
                                    {
                                        foreach (float value in similarity)
                                        {
                                            totalSimilarity += value;
                                        }
                                        //Code for when there definitely was a face match
                                        if ((totalSimilarity / similarity.Count) > 95)
                                        { 
                                            //Complete keystroke analysis and live person analysis (i can't remember what it's called)
                                        }
                                        else
                                        {
                                            //For if average isn't high enough.
                                            MessageBox.Show("Please take a new photo.");
                                            loginAttempt++;
                                            if(loginAttempt >= 5)
                                            {
                                                //Create procedure to lock account?
                                            }
                                        }
    
                                    }
                                    else
                                    {
                                        //Code for when there was no face matches.
                                    }
                                    
                                }
                                else
                                {
                                    MessageBox.Show("Password is Incorrect");
                                    //Code for when password is incorrect
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No records found for the given ID.");
                        }
                    }
                }
                connection.Close();
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
        static List<float> FacialComparison(string databaseImage, string comparisonImage)
        {
            //MessageBox.Show("Now inside the facial comparison function");
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("AKIAQ3EGUMLMQTICJUPB", "iRLnUHYMcr88EwSWMzW6iFEUiimGuDRFf1q9eYDI");

            var rekognitionClient = new AmazonRekognitionClient(awsCredentials, RegionEndpoint.GetBySystemName("eu-west-2"));

            var dbImage = new MemoryStream(Convert.FromBase64String(databaseImage));//Create Image Stream for DB Image
            var uploadImage = new MemoryStream(Convert.FromBase64String(comparisonImage));//Create Image Stream for Uploaded Image
            //MessageBox.Show("Image memory streams created");
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
            List<float> faceMatchSimilarities = new List<float>();
            // Process the response
            if (compareFacesResponse.FaceMatches.Count > 0)
            {
                
                foreach (var faceMatch in compareFacesResponse.FaceMatches) //Uses a foreach incase multiple people are in frame
                {

                    faceMatchSimilarities.Add(faceMatch.Similarity);
                    MessageBox.Show("Similarity:" + faceMatch.Similarity + "%");
                    return faceMatchSimilarities;
                    //Console.WriteLine($"Similarity: {faceMatch.Similarity}%");
                }
            }
            else
            {
                MessageBox.Show("Please use a photo with your face in it.");
                return null;
            }
            return null;
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
        /// <summary>
        /// This function allows for the webcam to be accessible using a Video Source using AForge libraries.
        /// </summary>
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

        /// <summary>
        /// This function puts the video feed into the PictureBox.
        /// The image is resized to ensure that the whole video is in the box and centered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap originalFrame = (Bitmap)eventArgs.Frame.Clone(); //Creates a clone of the current feed.
            Size newSize = CalculateSizeToFit(originalFrame.Size, photoUploadButton.ClientSize); //Creates a size variable of what the feed size is vs what the PictureBox is
            Bitmap resizedFrame = ResizeImage(originalFrame, newSize);//Changes the video feed to have a resolution and aspect ratio to fit the picture box.
            photoUploadButton.Image = resizedFrame;

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
            //Console.WriteLine("Inside Take Photo");
            if (photoCount <= 5)
            {
                //Console.WriteLine("inside photo count");
                if (photoUploadButton.Image != null)
                {
                    if (!photoStopwatch.IsRunning)
                    {
                        photoStopwatch.Start();
                        Console.WriteLine("stopwatch is started");
                    }
                    //Console.WriteLine("Image is being displayedc");
                    //long temp = keyboardTimer.ElapsedMilliseconds;
                    //Console.WriteLine("stopwtach has been created");

                    //Console.WriteLine(tempStopwatch.Elapsed.ToString());
                    //Console.WriteLine("Stopwatch is running");
                    //Console.WriteLine(tempStopwatch.ElapsedMilliseconds.ToString());

                    photoStopwatch.Restart();
                    // Capture the current image from the PictureBox
                    Bitmap capturedImage = (Bitmap)photoUploadButton.Image.Clone();
                    //videoSource.SignalToStop();
                    //videoSource.WaitForStop();

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // Save the Bitmap to the MemoryStream
                        capturedImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                        // Convert the MemoryStream to a byte array
                        byte[] byteArray = memoryStream.ToArray();

                        // Convert the byte array to a Base64 string
                        comparisonImageBase64.Add(Convert.ToBase64String(byteArray));
                        Console.WriteLine("Photo taken " + photoCount);
                        //Console.WriteLine();
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
        private void password_KeyPress(object sender, KeyEventArgs e)
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
            if (keystrokePattern.Count == 1 || photoStopwatch.ElapsedMilliseconds >= 200)
            {
                TakePhoto();
            }


            // Display or process the keystroke pattern as needed
            Console.WriteLine($"Recorded: Keystroke");
        }

   


        /*private static void password_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Up");
        }
        */


        /// <summary>
        /// This section assigns our variables from the user input text and checks if they've been input
        /// </summary>
        private void submitButton() 
        {
            string usernameInput = usernameInputTextBox.Text;
            string passwordHash = HashPassword(passwordInputTextBox.Text);
            
            //Makes the list of the timings of the keystroke
            List<string> finalKeystrokePattern = new List<string>
            {
                "Keystroke"//Initial keystroke
            };//Our new list with keystrokes + timings
            long x = 0;//Set value as 0 seconds before first keystroke
            foreach (long value in keystrokePattern)
            { 
                long y = value;//Sets equal to the "currentTime" variable previously added
                //Keystroke down -> time -> keystroke down -> time -> keystroke down -> time -> keystroke down
                
                finalKeystrokePattern.Add((y-x).ToString());//Adds in the time difference between 
                finalKeystrokePattern.Add("Keystroke");
                x = y;//This will be our new subtraction time between keystrokes.

            }
            foreach(string value in finalKeystrokePattern)
            {
                Console.WriteLine(value);
            }
            //MessageBox.Show(usernameInput);
            //MessageBox.Show(passwordInput);
            if (usernameInput != null && passwordHash != null && comparisonImageBase64 != null)
            {
                //MessageBox.Show("We are inside the SubmitButton function");
                UserSignIn(usernameInput, passwordHash, comparisonImageBase64);
            }
            else
            {
                MessageBox.Show("Please fill in all fields!");
            }
        }
        /// <summary>
        /// This section is used to actually complete the user sign in. It establishes a database connection
        /// and selects all the data available for the user. This will be their UID, username, hashed password and b64 of the image.
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="comparisonImage"></param>
        private void UserSignIn(string username, string password, List<string> comparisonImage)
        {
            //MessageBox.Show("We are inside the UserSignIn Function");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //MessageBox.Show("Connection Opened");
                string sqlQuery = "SELECT * FROM Users WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Username", username);
                    //MessageBox.Show("Parameters set.");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //MessageBox.Show("DataReader executed");
                        // Check if there are rows returned from the query
                        if (reader.HasRows)
                        {
                           // MessageBox.Show("Has Rows!");
                            // Iterate through the result set using the SqlDataReader
                            while (reader.Read())
                            {
                               // MessageBox.Show("Now assigning values");
                               //The reader object stores the database values as their headers

                                //This section can be probably changed to be a stored procedure to be more secure.
                                string databaseUsername = reader["Username"].ToString();
                                string databasePassword = reader["Password"].ToString();
                                string databaseImage = reader["image"].ToString();
                                if(databasePassword == password)
                                {
                                    //MessageBox.Show("Passwords matched");
                                    float highestSimilarity = FacialComparison(databaseImage, comparisonImage); //This is for current setup
                                    List<float> allSimilarities = new List<float>(); //This is for when multiple photos

                                    foreach (float value in allSimilarities)
                                    {
                                        if (value > 95)
                                        {
                                            //Add code for keystroke analysis here
                                            allSimilarities.Add(value);
                                        }
                                        else
                                        {
                                            //For if no user looks similar
                                        }
                                    }
                                    if (allSimilarities.Count > 0)
                                    {
                                        //Add code for when there is no face match
                                    }
                                    else
                                    {
                                        //Add code for when there are no face matches.
                                    }
                                }
                                else
                                {
                                    //Code for when password is incorrect
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No records found for the given ID.");
                        }
                    }
                }
                connection.Close();
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
        static float FacialComparison(string databaseImage, List<string> comparisonImages)
        {
            //MessageBox.Show("Now inside the facial comparison function");
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("AKIAQ3EGUMLMQTICJUPB", "iRLnUHYMcr88EwSWMzW6iFEUiimGuDRFf1q9eYDI");

            var rekognitionClient = new AmazonRekognitionClient(awsCredentials, RegionEndpoint.GetBySystemName("eu-west-2"));

            foreach(string value in comparisonImages)
            {
                var dbImage = new MemoryStream(Convert.FromBase64String(databaseImage));//Create Image Stream for DB Image
                var uploadImage = new MemoryStream(Convert.FromBase64String(value));//Create Image Stream for Uploaded Image
                                                                                              //MessageBox.Show("Image memory streams created");
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
                // Process the response
                if (compareFacesResponse.FaceMatches.Count > 0)
                {
                    float highestSimilarity = 0;

                    foreach (var faceMatch in compareFacesResponse.FaceMatches) //Uses a foreach incase multiple people are in frame
                    {
                        if (faceMatch.Similarity > highestSimilarity)
                        {
                            highestSimilarity = faceMatch.Similarity;
                        }
                        return highestSimilarity;
                        //faceMatchSimilarities.Add(faceMatch.Similarity);
                        //MessageBox.Show("Similarity:" + faceMatch.Similarity + "%");
                        //Console.WriteLine($"Similarity: {faceMatch.Similarity}%");
                    }
                }
                else
                {
                    MessageBox.Show("Please use a photo with your face in it.");
                    return 0;
                }
            }
            
            return 0;
        }
    }
 }

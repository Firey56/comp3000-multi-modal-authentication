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

namespace FirstGUIAttempt
{
    public partial class SignInPage : Form
    {
        public SignInPage()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
        }
        private VideoCaptureDevice videoSource;
        string selectedFilePath = null;
        private string connectionString = "Data Source=localhost;Initial Catalog=Users;Integrated Security=True";
        string comparisonImageBase64 = null;

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
            submitButton();
        }

        private void photoUploadLabel(object sender, EventArgs e)
        {

        }

        private void photoUpload(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// This is for the browse button that will be later removed when the seamless image selection is implemented.
        /// When an image is selected, it displays it on the screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void photoUploadButtonClick(object sender, EventArgs e)
        {
            InitializeWebcam();
            /*using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                    if (IsValidFile(selectedFilePath))
                    {
                        // Display the selected image
                        photoUploadButton.Image = System.Drawing.Image.FromFile(selectedFilePath);
                        photoUploadButton.SizeMode = PictureBoxSizeMode.Zoom;
                        photoUploadLabelText.Text = selectedFilePath;
                        System.Drawing.Image theImage = photoUploadButton.Image;
                        if (theImage != null)
                        {
                            byte[] theImageData = Common.ImageToByteArray(theImage);
                            comparisonImageBase64 = Convert.ToBase64String(theImageData);
                            //Console.WriteLine(comparisonImageBase64);
                            //MessageBox.Show("WriteLine should be here");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid file format. Please select a .png or .jpeg file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            */
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
            // Get the original frame from the webcam
            Bitmap originalFrame = (Bitmap)eventArgs.Frame.Clone(); //Creates a clone of the current feed.

            // Calculate the size to fit the entire frame within the PictureBox
            Size newSize = CalculateSizeToFit(originalFrame.Size, photoUploadButton.ClientSize); //Creates a size variable of what the feed size is vs what the PictureBox is

            // Resize the frame
            Bitmap resizedFrame = ResizeImage(originalFrame, newSize);//Changes the video feed to have a resolution and aspect ratio to fit the picture box.

            // Display the resized frame in the PictureBox
            photoUploadButton.Image = resizedFrame;

            // Dispose the original frame to avoid memory leaks
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
        /// <summary>
        /// Checks that the uploaded file has an image format that is widely accepted.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool IsValidFile(string filePath) //Method which is called when the user uploads a photo to check that it is of valid format.
        {
            string extension = Path.GetExtension(filePath)?.ToLower();
            return extension == ".png" || extension == ".jpeg" || extension == ".jpg";
        }

       
        /// <summary>
        /// This section assigns our variables from the user input text and checks if they've been input
        /// </summary>
        private void submitButton() 
        {
            string usernameInput = usernameInputTextBox.Text;
            string passwordInput = passwordInputTextBox.Text;
            //MessageBox.Show(usernameInput);
            //MessageBox.Show(passwordInput);
            if (usernameInput != null && passwordInput != null && comparisonImageBase64 != null)
            {
                //MessageBox.Show("We are inside the SubmitButton function");
                UserSignIn(usernameInput, passwordInput, comparisonImageBase64);
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
                                string databaseUsername = reader["Username"].ToString();
                                string databasePassword = reader["Password"].ToString();
                                string databaseImage = reader["FilePath"].ToString();
                                if(databasePassword == password)
                                {
                                    //MessageBox.Show("Passwords matched");
                                    FacialComparison(databaseImage, comparisonImage);
                                }
                                // Process the retrieved data (e.g., display or use it)
                                //Console.WriteLine($"Column1: {column1Value}, Column2: {column2Value}, Column3: {column3Value}");
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
        static void FacialComparison(string databaseImage, string comparisonImage)
        {
            //MessageBox.Show("Now inside the facial comparison function");
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("", "");

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

            // Process the response
            if(compareFacesResponse.FaceMatches.Count > 0)
            {
                foreach (var faceMatch in compareFacesResponse.FaceMatches) //Uses a foreach incase multiple people are in frame
                {
                    MessageBox.Show("Similarity:" + faceMatch.Similarity + "%");
                    //Console.WriteLine($"Similarity: {faceMatch.Similarity}%");
                }
            }
            else
            {
                MessageBox.Show("Please use a photo with your face in it.");
            }
         
        }

    }
 }

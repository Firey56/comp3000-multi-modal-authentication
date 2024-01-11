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

        private void photoUploadButtonClick(object sender, EventArgs e)
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
        }

        private bool IsValidFile(string filePath) //Method which is called when the user uploads a photo to check that it is of valid format.
        {
            string extension = Path.GetExtension(filePath)?.ToLower();
            return extension == ".png" || extension == ".jpeg" || extension == ".jpg";
        }

       

        private void submitButton() 
        {
            string usernameInput = usernameInputTextBox.Text;
            string passwordInput = passwordInputTextBox.Text;
            //MessageBox.Show(usernameInput);
            //MessageBox.Show(passwordInput);
            if (usernameInput != null && passwordInput != null)
            {
                //MessageBox.Show("We are inside the SubmitButton function");
                UserSignIn(usernameInput, passwordInput, comparisonImageBase64);
            }
            else
            {
                MessageBox.Show("Please fill in all fields!");
            }
        }
        private void UserSignIn(string username, string password, string comparisonImage)
        {
            MessageBox.Show("We are inside the UserSignIn Function");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                MessageBox.Show("Connection Opened");
                string sqlQuery = "SELECT * FROM Users WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Username", username);
                    MessageBox.Show("Parameters set.");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        MessageBox.Show("DataReader executed");
                        // Check if there are rows returned from the query
                        if (reader.HasRows)
                        {
                            MessageBox.Show("Has Rows!");
                            // Iterate through the result set using the SqlDataReader
                            while (reader.Read())
                            {
                                MessageBox.Show("Now assigning values");
                                // Access columns by name or index (0-based)
                                string databaseUsername = reader["Username"].ToString();
                                string databasePassword = reader["Password"].ToString();
                                string databaseImage = reader["FilePath"].ToString();
                                if(databasePassword == password)
                                {
                                    MessageBox.Show("Passwords matched");
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
            }
        }
        static void FacialComparison(string databaseImage, string comparisonImage)
        {
            MessageBox.Show("Now inside the facial comparison function");
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("", "");

            // Replace "YourRegion" with the AWS region where your Rekognition resource is located (e.g., us-east-1)
            var rekognitionClient = new AmazonRekognitionClient(awsCredentials, RegionEndpoint.GetBySystemName("eu-west-2"));

            // Replace "path/to/your/image.jpg" with the actual path to your image file
            byte[] byteArrayOfDatabaseImage = Convert.FromBase64String(databaseImage);
            byte[] byteArrayOfComparisonImage = Convert.FromBase64String(comparisonImage);
            var image1Stream = new MemoryStream(byteArrayOfDatabaseImage);
            var image2Stream = new MemoryStream(byteArrayOfComparisonImage);

            MessageBox.Show("Image memory streams created");
            var compareFacesRequest = new CompareFacesRequest
            {
                SourceImage = new Amazon.Rekognition.Model.Image
                {
                    Bytes = new MemoryStream(image1Stream.ToArray())
                },
                TargetImage = new Amazon.Rekognition.Model.Image
                {
                    Bytes = new MemoryStream(image2Stream.ToArray())
                },
                SimilarityThreshold = 0,
            };
            // Call Amazon Rekognition API to compare faces
            MessageBox.Show("Calling the API");
            CompareFacesResponse compareFacesResponse = rekognitionClient.CompareFaces(compareFacesRequest);

            // Process the response
            foreach (var faceMatch in compareFacesResponse.FaceMatches)
            {
                MessageBox.Show("Similarity:" + faceMatch.Similarity + "%");
                Console.WriteLine($"Similarity: {faceMatch.Similarity}%");
            }
        }

    }
 }

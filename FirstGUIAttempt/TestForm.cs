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
using System.Data.SqlClient;
using System.Windows.Forms.VisualStyles;
namespace FirstGUIAttempt
{
    public partial class TestForm : Form
    {
        private Button RefreshButton = new Button();
        private string connectionString = "Server=dissi-database.c32y6sk2evqy.eu-west-2.rds.amazonaws.com;Database=Dissertation;User ID=admin;Password=V4F^E2Tt#M#p#bjj;Encrypt=true;TrustServerCertificate=true;Connection Timeout=30;";
        public TestForm()
        {
            InitializeComponent();
            // Set up the ListBox
            CurrentUsersListBox.DrawMode = DrawMode.OwnerDrawFixed;
            CurrentUsersListBox.DrawItem += CurrentUsersListBox_DrawItem;
            DeletedUsersListBox.DrawMode = DrawMode.OwnerDrawFixed;
            DeletedUsersListBox.DrawItem += DeletedUsersListBox_DrawItem;
            RefreshButton.Location = new Point(0, 0);
            RefreshButton.Size = new Size(100, 50);
            RefreshButton.Text = "Refresh";
            this.Controls.Add(RefreshButton);
            // Populate the ListBox with items (including base64 encoded images)
            CurrentUsersListBox.ItemHeight = 100;
            DeletedUsersListBox.ItemHeight = 100;
            CurrentUsersListBox.DoubleClick += CurrentUsersListBox_DoubleClick;
            DeletedUsersListBox.DoubleClick += DeletedUsersListBox_DoubleClick;
            RefreshButton.Click += RefreshListBox;
            LoadDataIntoListBox("SELECT * FROM Authentication.AllUsersView", "CurrentUsers");
            LoadDataIntoListBox("SELECT * FROM Authentication.AllDeletedUsersView", "DeletedUsers");
            
        }

        private void RefreshListBox(object sender, EventArgs e)
        {
            CurrentUsersListBox.Items.Clear();
            DeletedUsersListBox.Items.Clear();
            LoadDataIntoListBox("SELECT * FROM Authentication.AllUsersView", "CurrentUsers");
            LoadDataIntoListBox("SELECT * FROM Authentication.AllDeletedUsersView", "DeletedUsers");
        }

        private void LoadDataIntoListBox(string query, string listBox)
        {
            // Assuming conn is your database connection object
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Open the connection
                conn.Open();

                // Define the SQL query to fetch data from the view

                // Create a command object with the SQL query and connection
                SqlCommand cmd = new SqlCommand(query, conn);

                // Execute the command and obtain a data reader
                SqlDataReader reader = cmd.ExecuteReader();

                // Iterate through the result set
                while (reader.Read())
                {
                    // Extract data from the reader
                    string userID = reader["UserID"].ToString();
                    string username = reader["Username"].ToString();
                    string base64Image = reader["Image"].ToString();
                    string UserType = reader["UserType"].ToString();

                    // Format the item text to include UserID, Username, and base64 image string
                    string itemText = $"{userID},{username},{base64Image}, {UserType}";

                    // Add the formatted item to the ListBox
                    if(listBox == "CurrentUsers")
                    {
                        CurrentUsersListBox.Items.Add(itemText);

                    }
                    else
                    {
                        DeletedUsersListBox.Items.Add(itemText);
                    }
                }

                // Close the reader
                reader.Close();
            }
        }

        private void CurrentUsersListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Check if the item index is valid
            if (e.Index >= 0)
            {
                // Get the ListBox control
                ListBox listBox = sender as ListBox;

                // Check if the ListBox control is valid
                if (listBox != null)
                {
                    // Set the bounds for drawing the item
                    Rectangle bounds = e.Bounds;

                    // Draw the background of the item
                    e.DrawBackground();

                    // Draw the image of the item
                    Image itemImage = GetImageForItem(e.Index); // Implement this method to get the image for each item
                    if (itemImage != null)
                    {
                        // Adjust the position of the image to align with the item
                        int imageSize = 80; // Set the desired size (width and height) for the image

                        // Calculate the position to center the image horizontally within the item
                        int imageX = bounds.Left + 10;

                        // Calculate the position to center the image vertically within the item
                        int imageY = bounds.Top + (bounds.Height - imageSize) / 2;

                        // Draw the image at the adjusted position
                        e.Graphics.DrawImage(itemImage, imageX, imageY, imageSize, imageSize);
                    }
                    string itemText = listBox.Items[e.Index].ToString();
                    string[] itemParts = itemText.Split(','); // Assuming the UserID and Username are separated by a character like '|'
                    if (itemParts.Length >= 3)
                    {
                        string userID = itemParts[0];
                        string username = itemParts[1];
                        string UserType = itemParts[3];
                        if(Int32.Parse(UserType) == 0)
                        {
                            UserType = "User";
                        }
                        else
                        {
                            UserType = "Admin";
                        }
                        // Set the position and font for drawing the text
                        int textX = bounds.Left + 90; // Adjust this value as needed
                        int textY = bounds.Top + 10; // Adjust this value as needed
                        Font textFont = listBox.Font;

                        // Draw the text (UserID and Username)
                        e.Graphics.DrawString("UserID: " + userID, textFont, Brushes.Black, textX, textY);
                        e.Graphics.DrawString("Username: " + username, textFont, Brushes.Black, textX, textY + 20); // Add some vertical spacing between UserID and Username
                        e.Graphics.DrawString("User Type: " + UserType, textFont, Brushes.Black, textX, textY + 40); // Add some vertical spacing between UserID and Username
                    }
                    // Indicate that the drawing is complete
                    e.DrawFocusRectangle();
                }
            }
        }



        private Image GetImageForItem(int index)
        {
            // Get the ListBox control
            ListBox listBox = CurrentUsersListBox;

            // Check if the ListBox control is valid and if the index is within bounds
            if (listBox != null && index >= 0 && index < listBox.Items.Count)
            {
                // Get the base64 string from the ListBox item
                string[] rowArray = listBox.Items[index].ToString().Split(',');
                string base64string = rowArray[2];

                // Convert the base64 string to bytes
                byte[] imageBytes = Convert.FromBase64String(base64string);

                // Create a MemoryStream from the bytes
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    // Create an Image from the MemoryStream
                    return Image.FromStream(ms);
                }
            }

            return null; // Return null if index is out of bounds or ListBox is invalid
        }
        private void DeletedUsersListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Check if the item index is valid
            if (e.Index >= 0)
            {
                // Get the ListBox control
                ListBox listBox = sender as ListBox;

                // Check if the ListBox control is valid
                if (listBox != null)
                {
                    // Set the bounds for drawing the item
                    Rectangle bounds = e.Bounds;

                    // Draw the background of the item
                    e.DrawBackground();

                    // Draw the image of the item
                    Image itemImage = GetImageForItemDeletedUsers(e.Index); // Implement this method to get the image for each item
                    if (itemImage != null)
                    {
                        // Adjust the position of the image to align with the item
                        int imageSize = 80; // Set the desired size (width and height) for the image

                        // Calculate the position to center the image horizontally within the item
                        int imageX = bounds.Left + 10;

                        // Calculate the position to center the image vertically within the item
                        int imageY = bounds.Top + (bounds.Height - imageSize) / 2;

                        // Draw the image at the adjusted position
                        e.Graphics.DrawImage(itemImage, imageX, imageY, imageSize, imageSize);
                    }
                    string itemText = listBox.Items[e.Index].ToString();
                    string[] itemParts = itemText.Split(','); // Assuming the UserID and Username are separated by a character like '|'
                    if (itemParts.Length >= 3)
                    {
                        string userID = itemParts[0];
                        string username = itemParts[1];
                        string UserType = itemParts[3];
                        if (Int32.Parse(UserType) == 0)
                        {
                            UserType = "User";
                        }
                        else
                        {
                            UserType = "Admin";
                        }
                        // Set the position and font for drawing the text
                        int textX = bounds.Left + 90; // Adjust this value as needed
                        int textY = bounds.Top + 10; // Adjust this value as needed
                        Font textFont = listBox.Font;

                        // Draw the text (UserID and Username)
                        e.Graphics.DrawString("UserID: " + userID, textFont, Brushes.Black, textX, textY);
                        e.Graphics.DrawString("Username: " + username, textFont, Brushes.Black, textX, textY + 20); // Add some vertical spacing between UserID and Username
                        e.Graphics.DrawString("User Type: " + UserType, textFont, Brushes.Black, textX, textY + 40); // Add some vertical spacing between UserID and Username
                    }
                    // Indicate that the drawing is complete
                    e.DrawFocusRectangle();
                }
            }
        }



        private Image GetImageForItemDeletedUsers(int index)
        {
            // Get the ListBox control
            ListBox listBox = DeletedUsersListBox;

            // Check if the ListBox control is valid and if the index is within bounds
            if (listBox != null && index >= 0 && index < listBox.Items.Count)
            {
                // Get the base64 string from the ListBox item
                string[] rowArray = listBox.Items[index].ToString().Split(',');
                string base64string = rowArray[2];

                // Convert the base64 string to bytes
                byte[] imageBytes = Convert.FromBase64String(base64string);

                // Create a MemoryStream from the bytes
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    // Create an Image from the MemoryStream
                    return Image.FromStream(ms);
                }
            }

            return null; // Return null if index is out of bounds or ListBox is invalid
        }

        private void CurrentUsersListBox_DoubleClick(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (CurrentUsersListBox.SelectedItem != null)
            {
                // Extract data from the selected item
                string[] itemParts = CurrentUsersListBox.SelectedItem.ToString().Split(',');
                string UserID = itemParts[0];
                string Username = itemParts[1];
                string base64Image = itemParts[2];
                string UserType = itemParts[3];
                // Create a new instance of your form
                UserForm UserForm = new UserForm("Current");
                // Pass the data to the new form
                UserForm.UserID = UserID;
                UserForm.Username = Username;
                UserForm.base64Image = base64Image;
                UserForm.UserType = UserType;

                // Show the new form
                UserForm.Show();
                UserForm.FormClosed += UserForm_FormClosed;
            }
        }private void DeletedUsersListBox_DoubleClick(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (DeletedUsersListBox.SelectedItem != null)
            {
                // Extract data from the selected item
                string[] itemParts = DeletedUsersListBox.SelectedItem.ToString().Split(',');
                string UserID = itemParts[0];
                string Username = itemParts[1];
                string base64Image = itemParts[2];
                string UserType = itemParts[3];
                // Create a new instance of your form
                UserForm UserForm = new UserForm("Deleted");
                // Pass the data to the new form
                UserForm.UserID = UserID;
                UserForm.Username = Username;
                UserForm.base64Image = base64Image;
                UserForm.UserType = UserType;

                // Show the new form
                UserForm.Show();
                UserForm.FormClosed += UserForm_FormClosed;
            }
        }
        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Perform the desired action here
            // For example, show a message box or update some UI element in Form1
            CurrentUsersListBox.Items.Clear();
            DeletedUsersListBox.Items.Clear();
            LoadDataIntoListBox("SELECT * FROM Authentication.AllUsersView", "CurrentUsers");
            LoadDataIntoListBox("SELECT * FROM Authentication.AllDeletedUsersView", "DeletedUsers");
        }


    }
}

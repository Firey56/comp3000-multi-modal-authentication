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
    public partial class AdminForm : Form
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=Users;Integrated Security=True";

        //private string connectionString = "Server=dissi-database.c32y6sk2evqy.eu-west-2.rds.amazonaws.com;Database=Dissertation;User ID=admin;Password=V4F^E2Tt#M#p#bjj;Encrypt=true;TrustServerCertificate=true;Connection Timeout=30;";
        public AdminForm(NewSignInForm.Session currentSession)
        {
            InitializeComponent();
            ApplyFontSettings();
            this.ClientSize = new Size(900, 500);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            // Set up the ListBox
            CurrentUsersListBox.DrawMode = DrawMode.OwnerDrawFixed;
            CurrentUsersListBox.DrawItem += CurrentUsersListBox_DrawItem;
            DeletedUsersListBox.DrawMode = DrawMode.OwnerDrawFixed;
            DeletedUsersListBox.DrawItem += DeletedUsersListBox_DrawItem;
            // Populate the ListBox with items (including base64 encoded images)
            CurrentUsersListBox.ItemHeight = 100;
            DeletedUsersListBox.ItemHeight = 100;
            CurrentUsersListBox.Size = new Size(350, 500);
            DeletedUsersListBox.Size = new Size(350, 500);
            CurrentUsersListBox.Location = new Point(50, 50);
            DeletedUsersListBox.Location = new Point(450, 50);
            if(currentSession.Confidence > 90)
            {
                CurrentUsersListBox.DoubleClick += CurrentUsersListBox_DoubleClick;
                DeletedUsersListBox.DoubleClick += DeletedUsersListBox_DoubleClick;
            }
            
            LoadDataIntoListBox("SELECT * FROM Authentication.AllUsersView", "CurrentUsers");
            LoadDataIntoListBox("SELECT * FROM Authentication.AllDeletedUsersView", "DeletedUsers");
            
        }

        private void ApplyFontSettings()
        {
            if (AccessibilitySettings.Font == "OpenDyslexic 3")
            {
                Font font = new Font(FirstGUIAttempt.OpenDyslexic.Families[0], AccessibilitySettings.FontSize);
                this.Font = font;
                ApplyFontToControls(this.Controls, font);
            }
            else
            {
                Font font = new Font(AccessibilitySettings.Font, AccessibilitySettings.FontSize);
                this.Font = font;
                ApplyFontToControls(this.Controls, font);
            }
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

        private void LoadDataIntoListBox(string query, string listBox)
        {
            // Assuming conn is your database connection object
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Open the connection
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                // Iterate through the result set
                while (reader.Read())
                {
                    // Extract data from the reader
                    string userID = reader["UserID"].ToString();
                    string username = reader["Username"].ToString();
                    string base64Image = reader["Image"].ToString();
                    string UserType = reader["UserType"].ToString();

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
            if (e.Index >= 0)
            {
                // Get the ListBox control 
                ListBox listBox = sender as ListBox;

                if (listBox != null)
                {
                    // Set the bounds for drawing the item
                    Rectangle bounds = e.Bounds;

                    // Draw the background of the item
                    e.DrawBackground();

                    // Draw the image of the item
                    Image itemImage = GetImageForItem(e.Index);
                    if (itemImage != null)
                    {
                        int imageSize = 80;

                        int imageX = bounds.Left + 10;

                        int imageY = bounds.Top + (bounds.Height - imageSize) / 2;

                        // Draw the image at the adjusted position
                        e.Graphics.DrawImage(itemImage, imageX, imageY, imageSize, imageSize);
                    }
                    string itemText = listBox.Items[e.Index].ToString();
                    string[] itemParts = itemText.Split(',');
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
                        int textX = bounds.Left + 90; 
                        int textY = bounds.Top + 10; 
                        Font textFont = listBox.Font;

                        e.Graphics.DrawString("UserID: " + userID, textFont, Brushes.Black, textX, textY);
                        e.Graphics.DrawString("Username: " + username, textFont, Brushes.Black, textX, textY + 20); 
                        e.Graphics.DrawString("User Type: " + UserType, textFont, Brushes.Black, textX, textY + 40); 
                    }
                    e.DrawFocusRectangle();
                }
            }
        }



        private Image GetImageForItem(int index)
        {
            // Get the ListBox control
            ListBox listBox = CurrentUsersListBox;

            if (listBox != null && index >= 0 && index < listBox.Items.Count)
            {
                string[] rowArray = listBox.Items[index].ToString().Split(',');
                string base64string = rowArray[2];//This selects the image

                byte[] imageBytes = Convert.FromBase64String(base64string);

                using (MemoryStream ImageStream = new MemoryStream(imageBytes))
                {
                    // Create an Image from the MemoryStream
                    return Image.FromStream(ImageStream);
                }
            }

            return null;
        }
        private void DeletedUsersListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                // Get the control
                ListBox listBox = sender as ListBox;

                if (listBox != null)
                {

                    Rectangle bounds = e.Bounds;

                    e.DrawBackground();

                    Image itemImage = GetImageForItemDeletedUsers(e.Index);
                    if (itemImage != null)
                    {
                        int imageSize = 80;

                        int imageX = bounds.Left + 10;

                        int imageY = bounds.Top + (bounds.Height - imageSize) / 2;

                        e.Graphics.DrawImage(itemImage, imageX, imageY, imageSize, imageSize);
                    }
                    string itemText = listBox.Items[e.Index].ToString();
                    string[] itemParts = itemText.Split(',');
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
                        int textX = bounds.Left + 90;
                        int textY = bounds.Top + 10;
                        Font textFont = listBox.Font;

                        e.Graphics.DrawString("UserID: " + userID, textFont, Brushes.Black, textX, textY);
                        e.Graphics.DrawString("Username: " + username, textFont, Brushes.Black, textX, textY + 20);
                        e.Graphics.DrawString("User Type: " + UserType, textFont, Brushes.Black, textX, textY + 40);
                    }
                    // Indicate that the drawing is complete
                    e.DrawFocusRectangle();
                }
            }
        }



        private Image GetImageForItemDeletedUsers(int index)
        {
            // Get the control
            ListBox listBox = DeletedUsersListBox;

            if (listBox != null && index >= 0 && index < listBox.Items.Count)
            {
                string[] rowArray = listBox.Items[index].ToString().Split(',');
                string base64string = rowArray[2];//Select the image string

                byte[] imageBytes = Convert.FromBase64String(base64string);

                using (MemoryStream ImageStream = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(ImageStream);
                }
            }

            return null; 
        }

        private void CurrentUsersListBox_DoubleClick(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (CurrentUsersListBox.SelectedItem != null)
            {
                // Extract data from the selected item
                string[] itemParts = CurrentUsersListBox.SelectedItem.ToString().Split(',');
                string TargetUserID = itemParts[0];
                string Username = itemParts[1];
                string base64Image = itemParts[2];
                string UserType = itemParts[3];
                // Create a new instance of your form
                UserForm UserForm = new UserForm("Current");
                // Pass the data to the new form
                UserForm.InteractingUserType = 1;
                UserForm.UserID = itemParts[0];
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
                UserForm.InteractingUserType = 1;
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
            CurrentUsersListBox.Items.Clear();
            DeletedUsersListBox.Items.Clear();
            LoadDataIntoListBox("SELECT * FROM Authentication.AllUsersView", "CurrentUsers");
            LoadDataIntoListBox("SELECT * FROM Authentication.AllDeletedUsersView", "DeletedUsers");
        }


    }
}

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

namespace FirstGUIAttempt
{

    public partial class UserForm : Form
    {
        private PictureBox UserPhoto = new PictureBox();
        private Label UsernameLabel = new Label();
        private Label UserIDLabel = new Label();
        private Button DeleteAccountButton = new Button();
        private Button RecoverAccountButton = new Button();
        private Label UsernameChanging = new Label();
        private Label UserIDChanging = new Label();
        private Label UserTypeLabel = new Label();
        private Label UserTypeChanging = new Label();
        private Button ChangeAdminButton = new Button();
        private string connectionString = "Server=dissi-database.c32y6sk2evqy.eu-west-2.rds.amazonaws.com;Database=Dissertation;User ID=admin;Password=V4F^E2Tt#M#p#bjj;Encrypt=true;TrustServerCertificate=true;Connection Timeout=30;";
        public string UserID { get; set; }
        public string Username { get; set; }
        public string base64Image { get; set; }
        public string UserType { get; set; }
        public UserForm(string ListBoxSentFrom)
        {
            InitializeComponent();


            UsernameLabel.Location = new Point(250, 150);
            UsernameLabel.Size = new Size(100, 20);
            UsernameLabel.Font = new Font("Arial", 12);
            UsernameLabel.Text = "Username";
            this.Controls.Add(UsernameLabel);

            UsernameChanging.Location = new Point(250, 170);
            UsernameChanging.Size = new Size(150, 20);
            UsernameChanging.Font = new Font("Arial", 12);
            this.Controls.Add(UsernameChanging);

            UserIDLabel.Location = new Point(250, 200);
            UserIDLabel.Size = new Size(100, 20);
            UserIDLabel.Font = new Font("Arial", 12);
            UserIDLabel.Text = "User ID";
            this.Controls.Add(UserIDLabel);
            UserIDChanging.Location = new Point(250, 220);
            UserIDChanging.Size = new Size(100, 20);
            UserIDChanging.Font = new Font("Arial", 12);
            this.Controls.Add(UserIDChanging);


            UserTypeLabel.Location = new Point(250, 240);
            UserTypeLabel.Size = new Size(100, 20);
            UserTypeLabel.Font = new Font("Arial", 12);
            UserTypeLabel.Text = "User Type";
            this.Controls.Add(UserTypeLabel);
            UserTypeChanging.Location = new Point(250, 260);
            UserTypeChanging.Size = new Size(100, 20);
            UserTypeChanging.Font = new Font("Arial", 12);
            this.Controls.Add(UserTypeChanging);
            if(ListBoxSentFrom == "Current")
            {
                DeleteAccountButton.Location = new Point(180, 460);
                DeleteAccountButton.Size = new Size(100, 40);
                DeleteAccountButton.Text = "Delete Account";
                DeleteAccountButton.Font = new Font("Arial", 8);
                this.Controls.Add(DeleteAccountButton);
            }
            else
            {
                RecoverAccountButton.Location = new Point(180, 460);
                RecoverAccountButton.Size = new Size(100, 40);
                RecoverAccountButton.Text = "Recover Account";
                RecoverAccountButton.Font = new Font("Arial", 8);
                this.Controls.Add(RecoverAccountButton);
            }
            
            
            ChangeAdminButton.Location = new Point(320, 460);
            ChangeAdminButton.Size = new Size(120, 40);
            ChangeAdminButton.Text = "Change Admin Status";
            ChangeAdminButton.Font = new Font("Arial", 8);
            this.Controls.Add(ChangeAdminButton);



            // Set PictureBox properties
            UserPhoto.Location = new System.Drawing.Point(50, 150); // Set the position
            UserPhoto.Size = new System.Drawing.Size(200, 150); // Set the size
            UserPhoto.SizeMode = PictureBoxSizeMode.StretchImage; // Set the size mode (e.g., StretchImage)
            // Add the PictureBox to the form's Controls collection
            this.Controls.Add(UserPhoto);
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.MaximumSize = Size;
            this.MinimumSize = Size;
            this.Load += UserForm_load;
            DeleteAccountButton.Click += DeleteAccountButton_Click;
            RecoverAccountButton.Click += DeleteAccountButton_Click;
            ChangeAdminButton.Click += ChangeAdminButton_Click;

        }

        //TODO The image currently doesn't show, suspect it is due to the picture box not being big enough.
        // Set other properties as needed (e.g., BackColor, BorderStyle, etc.)
        /////////////////////////////////////////////////////////////////////
        ///Added the controls, now time to actually do the page
        private void UserForm_load(object sender, EventArgs e)
        {
            // Example: Display the base64 image in a PictureBox
            if (!string.IsNullOrEmpty(base64Image))
            {
                byte[] imageBytes = Convert.FromBase64String(base64Image);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    UserPhoto.Image = Image.FromStream(ms);
                }
            }
            UsernameChanging.Text = Username;
            UserIDChanging.Text = UserID;
            UserTypeChanging.Text = UserType;

        }

        private void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            // Your code to execute when the button is clicked
            //TODO: Delete the account from the database. Need to make the stored procedure 

            // Add your logic here...
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert data into the database
                    //using (SqlCommand command = new SqlCommand("INSERT INTO users (Username, Password, image) VALUES (@Username, @Password, @image)", connection))
                    using (SqlCommand command = new SqlCommand("Authentication.DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", Int32.Parse(UserID));
                        command.Parameters.AddWithValue("@Username", Username);
                        //*MessageBox.Show(@"{username}, {password}, {filePath}");
                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            
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

        private void RecoverAccountButton_Click(object sender, EventArgs e)
        {
            // Your code to execute when the button is clicked
            //TODO: Delete the account from the database. Need to make the stored procedure 

            // Add your logic here...
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert data into the database
                    //using (SqlCommand command = new SqlCommand("INSERT INTO users (Username, Password, image) VALUES (@Username, @Password, @image)", connection))
                    using (SqlCommand command = new SqlCommand("Authentication.RecoverUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", Int32.Parse(UserID));
                        command.Parameters.AddWithValue("@Username", Username);
                        //*MessageBox.Show(@"{username}, {password}, {filePath}");
                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();

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

        private void ChangeAdminButton_Click(object sender, EventArgs e)
        {
            // Your code to execute when the button is clicked
            //TODO: Delete the account from the database. Need to make the stored procedure 

            // Add your logic here...
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert data into the database
                    //using (SqlCommand command = new SqlCommand("INSERT INTO users (Username, Password, image) VALUES (@Username, @Password, @image)", connection))
                    using (SqlCommand command = new SqlCommand("Authentication.AdminChange", connection))
                    {
                        int newValue;
                        if(Int32.Parse(UserType) == 0)
                        {
                            newValue = 1;
                        }
                        else
                        {
                            newValue = 0;
                        }
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", Int32.Parse(UserID));
                        command.Parameters.AddWithValue("@UserType", newValue);
                        //*MessageBox.Show(@"{username}, {password}, {filePath}");
                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected < 0)
                        {
                            MessageBox.Show("Admin Status Changed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();

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

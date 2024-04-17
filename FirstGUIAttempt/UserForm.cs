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
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Diagnostics;

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
        private TextBox NewPassword = new TextBox();
        private TextBox ConfirmNewPassword = new TextBox();
        private Label NewPasswordLabel = new Label();
        private Label ConfirmNewPasswordLabel = new Label();
        private Button SubmitNewPasswordButton = new Button();
        private bool current;
        readonly static List<long> keystrokePattern = new List<long>();
        readonly static Stopwatch keyboardTimer = new Stopwatch();
        readonly static List<string> finalKeystrokePattern = new List<string>();
        //private string connectionString = "Data Source=localhost;Initial Catalog=Users;Integrated Security=True";
        private string connectionString = "Data Source=localhost;Initial Catalog=Users;Integrated Security=True";

        //private string connectionString = "Server=dissi-database.c32y6sk2evqy.eu-west-2.rds.amazonaws.com;Database=Dissertation;User ID=admin;Password=V4F^E2Tt#M#p#bjj;Encrypt=true;TrustServerCertificate=true;Connection Timeout=30;";
        public int InteractingUserType { get; set; }
        public string UserID { get; set; }
        public string Username { get; set; }
        public string base64Image { get; set; }
        public string UserType { get; set; }
        public UserForm(string ListBoxSentFrom)
        {
            InitializeComponent();

            ApplyFontSettings();
            UsernameLabel.Location = new Point(250, 150);
            UsernameLabel.AutoSize = true;
            UsernameLabel.Text = "Username";
            this.Controls.Add(UsernameLabel);

            UsernameChanging.Location = new Point(250, 170);
            UsernameChanging.AutoSize = true;
            this.Controls.Add(UsernameChanging);
            NewPassword.KeyDown += NewPassword_KeyPress;
            NewPassword.TextChanged += NewPassword_TextChanged;

            if (ListBoxSentFrom == "Current")
                {
                    DeleteAccountButton.Location = new Point(180, 460);
                    DeleteAccountButton.AutoSize = true;
                    DeleteAccountButton.Text = "Delete Account";
                    ChangeAdminButton.Location = new Point(320, 460);
                    ChangeAdminButton.AutoSize = true;
                    ChangeAdminButton.Text = "Change Admin Status";
                    current = true;
                }
                else
                {
                    RecoverAccountButton.Location = new Point(250, 460);
                RecoverAccountButton.AutoSize = true;
                RecoverAccountButton.Text = "Recover Account";
                    current = false;
                }

            NewPassword.Location = new Point(250, 220);
            NewPassword.AutoSize = true;
            NewPassword.PasswordChar=  '*';
            NewPasswordLabel.Location = new Point(250, 200);
            NewPasswordLabel.AutoSize = true;
            NewPasswordLabel.Text = "New Password";

            ConfirmNewPassword.Location = new Point(250, 280);
            ConfirmNewPassword.AutoSize = true;
            ConfirmNewPassword.PasswordChar = '*';
            ConfirmNewPasswordLabel.Location = new Point(250, 260);
            ConfirmNewPasswordLabel.AutoSize = true;
            ConfirmNewPasswordLabel.Text = "Confirm New Password";



            SubmitNewPasswordButton.Location = new Point(250, 460);
            SubmitNewPasswordButton.AutoSize = true;
            SubmitNewPasswordButton.Text = "Submit";

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
            RecoverAccountButton.Click += RecoverAccountButton_Click;
            ChangeAdminButton.Click += ChangeAdminButton_Click;
            SubmitNewPasswordButton.Click += SubmitPasswordChange_Click;

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
        private void NewPassword_TextChanged(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {

                if (Clipboard.GetText() == NewPassword.Text)
                {
                    //pasteFlag = true;
                    Console.WriteLine("Pasted info and clipboard are the same");
                }
            }
        }

        //TODN The image currently doesn't show, suspect it is due to the picture box not being big enough.
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
            if(InteractingUserType == 1)
            {
                UserIDLabel.Location = new Point(250, 200);
                UserIDLabel.AutoSize = true;
                UserIDLabel.Text = "User ID";
                this.Controls.Add(UserIDLabel);
                UserIDChanging.Location = new Point(250, 220);
                UserIDChanging.AutoSize = true;
                this.Controls.Add(UserIDChanging);


                UserTypeLabel.Location = new Point(250, 240);
                UserTypeLabel.AutoSize = true;
                UserTypeLabel.Text = "User Type";
                this.Controls.Add(UserTypeLabel);
                UserTypeChanging.Location = new Point(250, 260);
                UserTypeChanging.AutoSize = true;
                this.Controls.Add(UserTypeChanging);
                if (current)
                {
                    this.Controls.Add(ChangeAdminButton);
                    this.Controls.Add(DeleteAccountButton);
                }
                else
                {
                    this.Controls.Add(RecoverAccountButton);

                }
            }
            else
            {
                this.Controls.Add(NewPassword);
                this.Controls.Add(ConfirmNewPassword);
                this.Controls.Add(NewPasswordLabel);
                this.Controls.Add(ConfirmNewPasswordLabel);
                this.Controls.Add(SubmitNewPasswordButton);
            }



        }
        private void NewPassword_KeyPress(object sender, KeyEventArgs e)
        {

            //Console.WriteLine("Down");
            if (!keyboardTimer.IsRunning)
            {
                keyboardTimer.Start();
            }
            else
            {
                if (e.KeyCode == Keys.Back)
                {//TODN make sure this is changed so that it doesn't crash if they press backspace when empty
                    keystrokePattern.Clear();
                    NewPassword.Text = "";
                    keyboardTimer.Stop();
                    keyboardTimer.Reset();
                    keystrokePattern.Clear();
                    finalKeystrokePattern.Clear();

                    //keystrokePattern.Add(keyboardTimer.ElapsedMilliseconds);
                }
                if(e.KeyCode != Keys.Back || e.KeyCode != Keys.Enter)
                {
                    long currentTime = keyboardTimer.ElapsedMilliseconds;
                    keystrokePattern.Add(currentTime);
                }


                //keyboardTimer.Reset();
            }

            // Display or process the keystroke pattern as needed
            //*Console.WriteLine($"Recorded: Keystroke");
        }

        private void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            // Your code to execute when the button is clicked
            //TODN: Delete the account from the database. Need to make the stored procedure 

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
            //TODN: Delete the account from the database. Need to make the stored procedure 
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
                            MessageBox.Show("User Recovered", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //TODN: Delete the account from the database. Need to make the stored procedure 
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
        private void SubmitPasswordChange_Click(object sender, EventArgs e)
        {
            //TODN: Delete the account from the database. Need to make the stored procedure 

            string newPassword = NewPassword.Text;
            string confirmNewPassword = ConfirmNewPassword.Text;

            if(newPassword == confirmNewPassword)
            {
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
                string hashedPassword = HashPassword(newPassword);
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Insert data into the database
                        //using (SqlCommand command = new SqlCommand("INSERT INTO users (Username, Password, image) VALUES (@Username, @Password, @image)", connection))
                        using (SqlCommand command = new SqlCommand("Authentication.ChangePassword", connection))
                        {

                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TableName", Username);
                            command.Parameters.AddWithValue("@UserID", Int32.Parse(UserID));
                            command.Parameters.AddWithValue("@Password", hashedPassword);
                            //*MessageBox.Show(@"{username}, {password}, {filePath}");
                            // Execute the query
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                InsertKeystrokes(Username);
                                MessageBox.Show("Password Changed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();

                            }
                            else
                            {
                                MessageBox.Show("Failed to update password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void InsertKeystrokes(string Username)
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

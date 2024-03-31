using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FirstGUIAttempt
{
    public partial class AdminPage : Form
    {
        private string connectionString = "Server=dissi-database.c32y6sk2evqy.eu-west-2.rds.amazonaws.com;Database=Dissertation;User ID=admin;Password=V4F^E2Tt#M#p#bjj;Encrypt=true;TrustServerCertificate=true;Connection Timeout=30;";
        private string viewName = "dissertation.AllUsersView";
        public AdminPage()
        {
            InitializeComponent();

            string query = $"SELECT * FROM {viewName}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add each row to the ListBox
                        string row = $"{reader["UserID"].ToString()} - {reader["Username"]}";// - {reader["image"]}";
                        AllUsers.Items.Add(row);
                    }
                }
            }

        }
        

        // When a user loads this page, it should populate the listbox with all the user information
    }

}

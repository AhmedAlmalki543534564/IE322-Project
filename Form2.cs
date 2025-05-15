using System.Data.SqlClient;
using IE322_Project;
using Microsoft.Data.SqlClient;

namespace LoginForm1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            txtPass.UseSystemPasswordChar = true;

        }

        public bool IsLoggedIn { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                // Creating a connection to the SQL Server database
                using (SqlConnection con = new SqlConnection("Data Source=SWEET_1\\TEW_SQLEXPRESS;Initial Catalog=LoginApp;Integrated Security=True;TrustServerCertificate=True"))
                {
                    con.Open(); // Opens the connection to the database

                    // SQL query to check if the username exists in the database
                    string usernameQuery = "SELECT COUNT(*) FROM loginapp WHERE username=@username";
                    using (SqlCommand usernameCmd = new SqlCommand(usernameQuery, con))
                    {
                        // Adding the parameter for the query
                        usernameCmd.Parameters.AddWithValue("@username", txtUser.Text);
                        int usernameCount = (int)usernameCmd.ExecuteScalar(); // Executes the query and gets the result (the count of matching rows)

                        // If username doesn't exist, show an error message and exit the method
                        if (usernameCount == 0)
                        {
                            MessageBox.Show("The username does not exist. Please check your username.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Exit the method early, no need to proceed further
                        }
                    }

                    // SQL query to check if the username and password match in the database
                    string passwordQuery = "SELECT COUNT(*) FROM loginapp WHERE username=@username AND password=@password";
                    using (SqlCommand passwordCmd = new SqlCommand(passwordQuery, con))
                    {
                        // Adding parameters for the query (username and password)
                        passwordCmd.Parameters.AddWithValue("@username", txtUser.Text);
                        passwordCmd.Parameters.AddWithValue("@password", txtPass.Text);
                        int passwordCount = (int)passwordCmd.ExecuteScalar(); // Executes the query and checks if there are any matches for both username and password

                        // If password is correct, set the IsLoggedIn property to true and show a success message
                        if (passwordCount > 0)
                        {
                            IsLoggedIn = true; // The user is logged in successfully
                            InventoryManagementStudio frm = new InventoryManagementStudio();
                            frm.Show();
                        }
                        else
                        {
                            // If password is incorrect, show an error message
                            MessageBox.Show("Incorrect password. Please check your password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // In case of any exceptions (like a connection issue), display an error message
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                

            }
           
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {

            txtPass.UseSystemPasswordChar = !chkShow.Checked;
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
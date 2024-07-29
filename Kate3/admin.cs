using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Kate3
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=BOJASTARA\\SQLEXPRESS;Initial Catalog=Kate3;Integrated Security=True");
        string imgLocation = "";
        string imgLocation1 = "";
      
      
       
      
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    
        private string userEmail;
        String randomCode;
        public static String to;

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string email = txt_email.Text;
            string pass = txt_pass.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            try
            {
                conn.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = @"INSERT INTO users (Email, Password, role)
                     VALUES (@Email, @Password, @role)";
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", HashPassword(pass));
                    command.Parameters.AddWithValue("@role", "admin"); // default role is "user"

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        userEmail = email; // Assign userEmail here
                        SendVerificationCode();
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again.");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void SendVerificationCode()
        {
            String from, password, messageBody;
            Random rand = new Random();
            randomCode = (rand.Next(999999)).ToString();
            MailMessage message = new MailMessage();
            to = userEmail;
            from = "ahmadghosen20@gmail.com";
            password = "aahifayyaaacyfua"; // Consider using a more secure way to handle this
            messageBody = "Dear user\n\n" + "Your verification code is " + randomCode + "\n\nkate3 adminstrator";
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            message.Subject = "Verification Code";

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
            {
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(from, password);

                try
                {
                    smtp.Send(message);
                    MessageBox.Show("Code sent successfully");
                    Verification vc = new Verification(randomCode);
                    this.Hide();
                    vc.Show();
                }
                catch (SmtpException smtpEx)
                {
                    MessageBox.Show("Email sending error: " + smtpEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create a connection string
            string connectionString = "Data Source=BOJASTARA\\SQLEXPRESS;Initial Catalog=Kate3;Integrated Security=True";

            // Create a SQL query
            string query = "SELECT * FROM supports";

            // Create a new DataTable
            DataTable dataTable = new DataTable();

            // Create a new SqlConnection and SqlDataAdapter
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    // Fill the DataTable with data from the database
                    adapter.Fill(dataTable);
                }
            }

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;

            // Refresh the DataGridView to display the data
            dataGridView1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Create a connection string
            string connectionString = "Data Source=BOJASTARA\\SQLEXPRESS;Initial Catalog=Kate3;Integrated Security=True";

            // Create a SQL query
            string query = "SELECT * FROM not_supports";

            // Create a new DataTable
            DataTable dataTable = new DataTable();

            // Create a new SqlConnection and SqlDataAdapter
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    // Fill the DataTable with data from the database
                    adapter.Fill(dataTable);
                }
            }

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;

            // Refresh the DataGridView to display the data
            dataGridView1.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Create a connection string
            string connectionString = "Data Source=BOJASTARA\\SQLEXPRESS;Initial Catalog=Kate3;Integrated Security=True";

            // Create a SQL query
            string query = "SELECT * FROM users WHERE role='user'";

            // Create a new DataTable
            DataTable dataTable = new DataTable();

            // Create a new SqlConnection and SqlDataAdapter
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    // Fill the DataTable with data from the database
                    adapter.Fill(dataTable);
                }
            }

            // Bind the DataTable to the DataGridView
            dataGridView3.DataSource = dataTable;

            // Refresh the DataGridView to display the data
            dataGridView3.Refresh();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login loginpage1 = new login();
            loginpage1.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void save_Click_1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=BOJASTARA\\SQLEXPRESS;Initial Catalog=Kate3;Integrated Security=True");
            SqlCommand cmd;

            try
            {
                // Open the connection
                conn.Open();

                // Create a new command
                cmd = new SqlCommand("INSERT INTO supports (name, description, image) VALUES (@name1, @description1, @image1)", conn);

                // Create parameters
                cmd.Parameters.AddWithValue("@name1", textBox1.Text);
                cmd.Parameters.AddWithValue("@description1", textBox2.Text);

                // Convert image to byte array
                byte[] img1 = null;
                if (!string.IsNullOrEmpty(imgLocation1))
                {
                    FileStream fs = new FileStream(imgLocation1, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    img1 = br.ReadBytes((int)fs.Length);
                }

                // Add image parameter
                cmd.Parameters.AddWithValue("@image1", img1);

                // Execute the command
                cmd.ExecuteNonQuery();

                // Create a new command for the second table
                cmd = new SqlCommand("INSERT INTO not_supports (name, description, image) VALUES (@name2, @description2, @image2)", conn);

                // Create parameters
                cmd.Parameters.AddWithValue("@name2", textBox6.Text);
                cmd.Parameters.AddWithValue("@description2", textBox5.Text);

                // Convert image to byte array
                byte[] img2 = null;
                if (!string.IsNullOrEmpty(imgLocation))
                {
                    FileStream fs = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    img2 = br.ReadBytes((int)fs.Length);
                }

                // Add image parameter
                cmd.Parameters.AddWithValue("@image2", img2);

                // Execute the command
                cmd.ExecuteNonQuery();

                // Close the connection
                conn.Close();

                MessageBox.Show("Image saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      

        private void Browse_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                imgLocation1 = openFileDialog.FileName;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox11.Image = new Bitmap(openFileDialog.FileName);
                imgLocation = openFileDialog.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox11.Image = new Bitmap(openFileDialog.FileName);
                imgLocation = openFileDialog.FileName;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            // Create a connection string
            string connectionString = "Data Source=BOJASTARA\\SQLEXPRESS;Initial Catalog=Kate3;Integrated Security=True";

            // Create a SQL query
            string query = "SELECT * FROM users WHERE role='admin'";

            // Create a new DataTable
            DataTable dataTable = new DataTable();

            // Create a new SqlConnection and SqlDataAdapter
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    // Fill the DataTable with data from the database
                    adapter.Fill(dataTable);
                }
            }

            // Bind the DataTable to the DataGridView
            dataGridView3.DataSource = dataTable;

            // Refresh the DataGridView to display the data
            dataGridView3.Refresh();
        }

        private void refrechToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox5.Text = string.Empty;

            // Clear the PictureBox
           pictureBox1.Image = (Image)(Properties.Resources.ResourceManager.GetObject("image"));
            pictureBox11.Image = (Image)(Properties.Resources.ResourceManager.GetObject("image"));
        }
    }
    }
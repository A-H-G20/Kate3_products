using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kate3
{
    public partial class login : Form
    {
        private readonly string _connectionString;
        public login()
        {
            InitializeComponent();
            _connectionString = "Data Source=BOJASTARA\\SQLEXPRESS;Initial Catalog=Kate3;Integrated Security=True";
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string email = txt_email.Text;
            string pass = txt_pass.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Email or password cannot be empty.");
                return;
            }

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT Email, password, role
                                        FROM users
                                        WHERE Email = @Email";
                        command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = email;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                string role = reader["role"].ToString();
                                if (role == "user")
                                {
                                    Homepage hpage = new Homepage();
                                    hpage.Show();
                                  
                                    this.Hide();
                                }
                                else if (role == "admin")
                                {
                                    admin adminpage = new admin();
                                    adminpage.Show();
                                   // MessageBox.Show("Login succeeded.");
                                    this.Hide();
                                }
                            
                            }
                           


                            else
                            {
                                MessageBox.Show("Incorrect email or password.");
                            }
                        }

                    }
                }
            }




            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);

            }
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registerpage re = new registerpage();
            re.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txt_pass_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

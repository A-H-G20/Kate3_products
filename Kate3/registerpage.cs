using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Kate3.Db;
using System.Security.Cryptography;
namespace Kate3
{
    public partial class registerpage : Form
    {
        private string userEmail;
        String randomCode;
        public static String to;
        DbConnection conn = new DbConnection();
        public registerpage()
        {
            InitializeComponent();
        }

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
                using (var connection = conn.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"INSERT INTO users (Email, Password, role)
                                 VALUES (@Email, @Password, @role)";
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", pass);
                        command.Parameters.AddWithValue("@role", "user"); // default role is "user"

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
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
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
            //messageBody = "Dear user,\nYour verification code is {randomCode}\nBest, Kate3 Administrator";
            messageBody = "Dear user\n\n"+"Your verification code is " + randomCode+"\n\nkate3 adminstrator";
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

        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login newForm = new login();
            newForm.Show();
            this.Hide();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}

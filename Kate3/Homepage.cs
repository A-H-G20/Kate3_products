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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kate3
{
    public partial class Homepage : Form
    {
        public Homepage()
        {
            InitializeComponent();
        }

      

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void refrechToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            description0.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            // Clear the PictureBox
            mkata3pic.Image = (Image)(Properties.Resources.ResourceManager.GetObject("logo"));
            pictureBox4.Image = (Image)(Properties.Resources.ResourceManager.GetObject("logo"));
        }

        private void Homepage_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=BOJASTARA\\SQLEXPRESS;Initial Catalog=Kate3;Integrated Security=True";
            string name = textBox1.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // First query for table a
                    string query1 = "SELECT Id, Image, Description,Name FROM supports WHERE name = @name";
                    SqlCommand command1 = new SqlCommand(query1, connection);
                    command1.Parameters.AddWithValue("@name", name);

                    int aId = 0; // Variable to store the ID from table a

                    using (SqlDataReader reader1 = command1.ExecuteReader())
                    {
                        if (reader1.Read())
                        {
                            // Get the ID, image, and description from table a
                            aId = Convert.ToInt32(reader1["Id"]);
                            byte[] imageBytes1 = reader1["Image"] as byte[];
                            string description1 = reader1["Description"].ToString();
                            string name1 = reader1["Name"].ToString();

                            // Display the image in pictureBox1
                            if (imageBytes1 != null)
                            {
                                using (MemoryStream ms1 = new MemoryStream(imageBytes1))
                                {
                                    mkata3pic.Image = Image.FromStream(ms1);
                                }
                            }

                            // Display the description in textBox2
                            description0.Text = description1;
                            textBox2.Text = name1;
                        }
                        else
                        {
                            MessageBox.Show("This Item is Not found");
                            return; // Exit the method if no record is found
                        }
                    }

                    // Second query for table b where Id matches with table a
                    string query2 = "SELECT Image, Description,Name FROM not_supports WHERE Id = @id";
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@id", aId); // Use the ID from table a

                    using (SqlDataReader reader2 = command2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            // Get the image and description from table b
                            byte[] imageBytes2 = reader2["Image"] as byte[];
                            string description2 = reader2["Description"].ToString();
                            string name2 = reader2["Name"].ToString();
                            // Display the image in pictureBox2
                            if (imageBytes2 != null)
                            {
                                using (MemoryStream ms2 = new MemoryStream(imageBytes2))
                                {
                                    pictureBox4.Image = Image.FromStream(ms2);
                                }
                            }

                            // Display the description in textBox3
                            textBox3.Text = description2;
                            textBox4.Text = name2;
                        }
                        else
                        {
                           // MessageBox.Show("No record found in table b corresponding to the ID in table a.");
                            return; // Exit the method if no record is found
                        }
                    }

                    //MessageBox.Show("Image and description retrieved successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void description1_TextChanged(object sender, EventArgs e, string supported)
        {
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login loginpage = new login();
            loginpage.Show();
            this.Hide();
        }

        private void description1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
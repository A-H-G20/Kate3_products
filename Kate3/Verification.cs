using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kate3
{
    public partial class Verification : Form
    {
        private string verificationcode;
        public Verification(string code)
        {
            InitializeComponent();
            verificationcode = code;
        }

        
        private void pictureBox5_Click(object sender, EventArgs e)
        {

            if (verificationcode == (textBox1.Text).ToString())
            {

                this.Hide();
                login form1 = new login();
                form1.Show();

            }
            else
            {
                MessageBox.Show("Wrong Code");
            }
        }

        private void Verification_Load(object sender, EventArgs e)
        {

        }
    }
}

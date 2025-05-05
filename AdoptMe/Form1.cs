using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdoptMe.systemCS;
using static AdoptMe.User;

namespace AdoptMe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            string password = textBox2.Text;

            User user = LoginManager.Authenticate(email, password);

            if (user == null)
            {
                MessageBox.Show("Invalid email or password.");
                return;
            }

            Session.Login(user);

            if (user.IsRole == User.Role.Admin)
            {
                MessageBox.Show("Welcome Admin!");
            }
            else
            {
                MessageBox.Show("Welcome Adoptee!");
            }

            this.Hide();
        }

    }

}

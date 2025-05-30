﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdoptMe.systemCS;
using AdoptMe.uiAdmin;
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
                FormManager.ShowForm(new AdminDash());
            }
            else
            {
                FormManager.ShowForm(new AdopteeDash());
            }

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var registerForm = new Register())
            {
                if (registerForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Registration successful! Please log in.");
                }
            }
        }
    }

}

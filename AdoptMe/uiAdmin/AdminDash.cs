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

namespace AdoptMe.uiAdmin
{
    public partial class AdminDash : Form
    {
        public AdminDash()
        {
            InitializeComponent();
            this.FormClosed += Closed;
        }
        private void Closed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void LoadFormInPanel(Form form)
        {
            panel3.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel3.Controls.Add(form);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new ManagePets());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new ManageRequest());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new ManageReports());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Session.Logout();
                // Show login form
                var loginForm = new Form1();
                loginForm.Show();
                this.FormClosed -= Closed; // Prevent Application.Exit()
                this.Close();
            }
        }
    }
}

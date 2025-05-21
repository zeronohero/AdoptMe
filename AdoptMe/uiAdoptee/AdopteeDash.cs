using System;
using System.Windows.Forms;
using AdoptMe.systemCS;
using AdoptMe.uiAdoptee;
using AdoptMe.uiAdoptee.Request;

namespace AdoptMe
{
    public partial class AdopteeDash : Form
    {
        public AdopteeDash()
        {
            InitializeComponent();
            this.FormClosed += Closed;
        }

        private void LoadFormInPanel(Form form)
        {
            panel4.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel4.Controls.Add(form);
            form.Show();
        }
        private void Closed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new PetLists());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new RequestLists());
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

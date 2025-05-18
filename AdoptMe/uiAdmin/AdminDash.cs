using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoptMe.uiAdmin
{
    public partial class AdminDash : Form
    {
        public AdminDash()
        {
            InitializeComponent();
            this.FormClosed += AdminDash_FormClosed;
        }
        private void AdminDash_FormClosed(object sender, FormClosedEventArgs e)
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
    }
}

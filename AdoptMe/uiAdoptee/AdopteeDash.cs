using System;
using System.Windows.Forms;
using AdoptMe.uiAdoptee;

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
    }
}

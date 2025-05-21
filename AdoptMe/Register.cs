using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoptMe
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                string password = textBox2.Text;
                string email = textBox3.Text;
                string number = textBox4.Text;
                string address = textBox5.Text;
                new Adoptee(email, password, name, number, address).SaveToDatabase();
                this.DialogResult = DialogResult.OK;
                this.Close();
                MessageBox.Show($"Address: {address}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

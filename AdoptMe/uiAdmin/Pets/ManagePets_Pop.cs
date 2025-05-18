using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AdoptMe.systemCS;

namespace AdoptMe.uiAdmin
{
    public partial class ManagePets_Pop : Form
    {
        public ManagePets_Pop()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var animal = new Animal(
                textBox1.Text, // Name
                comboBox1.SelectedItem.ToString(), // Species
                textBox2.Text, // Color
                int.Parse(textBox3.Text), // Age
                comboBox2.SelectedItem.ToString(), // Gender
                "not_adopted", // Default status
                Session.CurrentUser.Id
            );

            animal.SaveToDatabase();
            MessageBox.Show("Animal added successfully!");
            this.Close();
        }
    }
}

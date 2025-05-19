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
using System.IO;

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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                {
                    open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bpm; *.png;)|*.jpg; *.jpeg; *.gif; *.bpm; *.png;";
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        textBox4.Text = open.FileName;
                        pictureBox1.Image = Image.FromFile(open.FileName);
                    }
                }

            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(textBox4.Text, Path.Combine(textBox3.Text, @"C:\Users\zerxt\source\repos\AdoptMe\AdoptMe\image", Path.GetFileName(textBox4.Text)));
                MessageBox.Show("Image have beed posted");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}

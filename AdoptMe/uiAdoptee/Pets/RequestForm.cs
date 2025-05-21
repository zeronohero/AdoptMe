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

namespace AdoptMe
{
    public partial class RequestForm : Form
    {
        public RequestForm()
        {
            InitializeComponent();
        }
        private Animal animal;

        public RequestForm(Animal animal)
        {
            InitializeComponent();
            this.animal = animal;

            label1.Text = $"Name: {animal.Name}";
            label2.Text = $"Color: {animal.Color}";
            label3.Text = $"Gender: {animal.Gender}";
            label4.Text = $"Species: {animal.Species}";
            label5.Text = $"Date Added: {animal.Date_added:yyyy-MM-dd}";

            if (!string.IsNullOrEmpty(animal.Image) && System.IO.File.Exists(animal.Image))
            {
                pictureBox1.Image = Image.FromFile(animal.Image);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox1.Image = null; 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string information = richTextBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(information))
            {
                MessageBox.Show("Please enter your inquiry or information before submitting.");
                return;
            }

            try
            {
                int userId = Session.CurrentUser.Id;
                int animalId = animal.Animal_id;
                var request = new AdoptionRequest(
                    userId,
                    animalId,
                    information,
                    "Pending"
                );
                request.SaveToDatabase();
                MessageBox.Show("Adoption request sent!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send adoption request: {ex.Message}");
            }
        }
    }
}

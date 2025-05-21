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

namespace AdoptMe.uiAdoptee.Pets
{
    public partial class PetPanel : UserControl
    {
        public PetPanel()
        {
            InitializeComponent();
        }
        public void SetAnimal(Animal animal)
        {
            label1.Text = animal.Name;
            label2.Text = animal.Color;
            label3.Text = animal.Gender;
            label4.Text = animal.Species;
            label5.Text = "" + animal.Date_added;
            pictureBox1.Image = Image.FromFile(animal.Image);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            // Store the animal object for use in the button click
            this.Tag = animal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Tag is Animal animal)
            {
                try
                {
                    int userId = Session.CurrentUser.Id;
                    int animalId = animal.Animal_id;
                    string information = $"Request to adopt {animal.Name} ({animal.Species})";
                    var request = new AdoptMe.AdoptionRequest(
                        userId,
                        animalId,
                        information,
                        "Pending"
                    );
                    request.SaveToDatabase();
                    MessageBox.Show($"Adoption request sent for {animal.Name}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to send adoption request: {ex.Message}");
                }
            }
        }

    }
}

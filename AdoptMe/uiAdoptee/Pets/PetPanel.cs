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
            label5.Text = $"{animal.Date_added:yyyy-MM-dd}";
            pictureBox1.Image = Image.FromFile(animal.Image);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            // Store the animal object for use in the button click
            this.Tag = animal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Tag is Animal animal)
            {
                using (var requestForm = new AdoptMe.RequestForm(animal))
                {
                    if (requestForm.ShowDialog() == DialogResult.OK)
                    {
                        // Optionally, refresh the UI or show a confirmation message
                    }
                }
            }
        }


    }
}

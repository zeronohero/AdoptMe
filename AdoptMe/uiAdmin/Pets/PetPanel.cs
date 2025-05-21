using System.Drawing;
using System.Windows.Forms;

namespace AdoptMe.uiAdmin.Pets
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
            pictureBox1.Image = Image.FromFile(animal.Image);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            if(animal.Status == "adopted")
            {
                label5.Text = "adopted";
            }
            else if (animal.Status == "not_adopted")
            {
                label5.Text = "not adopted";
            }
            label6.Text = "" + animal.Date_added;
        }

        private void PetPanel_Load(object sender, System.EventArgs e)
        {

        }
    }

}


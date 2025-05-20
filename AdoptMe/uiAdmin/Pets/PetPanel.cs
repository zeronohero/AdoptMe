using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AdoptMe.uiAdmin.Pets
{
    public partial class PetPanel : UserControl
    {
        static string default_profile = @"C:\Users\zerxt\source\repos\AdoptMe\AdoptMe\image\Default.png";
        public PetPanel()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(default_profile);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void SetAnimal(Animal animal)
        {
            label1.Text = animal.Name;
            label2.Text = animal.Color;
            label3.Text = animal.Gender;
            label4.Text = animal.Species;
            label9.Text = animal.Image;
        }

    }

}


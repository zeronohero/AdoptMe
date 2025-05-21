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

namespace AdoptMe.uiAdoptee
{
    public partial class PetLists : Form
    {
        List<Animal> animals = new List<Animal>();
        public PetLists()
        {
            InitializeComponent();
            loadAnimals();
            PopulateAnimalPanels();
        }
        private void loadAnimals()
        {
            animals = Animal.GetAllAnimals();
            animals.Where(a => a.Status.Contains("not_adopted"));
        }

        private void PopulateAnimalPanels()
        {
            flowLayoutPanel2.Controls.Clear();

            foreach (var animal in animals)
            {
                var petPanel = new AdoptMe.uiAdoptee.Pets.PetPanel();
                petPanel.SetAnimal(animal);
                flowLayoutPanel2.Controls.Add(petPanel);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

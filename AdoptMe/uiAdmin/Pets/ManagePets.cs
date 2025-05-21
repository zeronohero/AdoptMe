using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdoptMe.uiAdmin;

namespace AdoptMe
{
    public partial class ManagePets : Form
    {
        List<Animal> animals;
        public ManagePets()
        {
            InitializeComponent();
            comboBox2.SelectedIndexChanged += (s, e) => FilterAndDisplayAnimals();
            textBox1.TextChanged += (s, e) => FilterAndDisplayAnimals();
            loadAnimals();
            FilterAndDisplayAnimals();
        }

        private void loadAnimals()
        {
            animals = Animal.GetAllAnimals();
        }

        private void PopulateAnimalPanels()
        {
            flowLayoutPanel2.Controls.Clear();

            foreach (var animal in animals)
            {
                var petPane = new uiAdmin.Pets.PetPanel();
                petPane.SetAnimal(animal);
                flowLayoutPanel2.Controls.Add(petPane);
            }
        }

        private void FilterAndDisplayAnimals()
        {
            IEnumerable<Animal> filtered = animals;

            string selectedSpecies = comboBox2.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedSpecies) && selectedSpecies != "All")
            {
                filtered = filtered.Where(a => a.Species.Equals(selectedSpecies, StringComparison.OrdinalIgnoreCase));
            }

            string searchText = textBox1.Text?.Trim();
            if (!string.IsNullOrEmpty(searchText) && searchText != "search")
            {
                filtered = filtered.Where(a => a.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            flowLayoutPanel2.Controls.Clear();
            foreach (var animal in filtered)
            {
                var petPane = new uiAdmin.Pets.PetPanel();
                petPane.SetAnimal(animal);
                flowLayoutPanel2.Controls.Add(petPane);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadAnimals();
            FilterAndDisplayAnimals();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addAnimalForm = new ManagePets_Pop();
            if (addAnimalForm.ShowDialog() == DialogResult.OK)
            {
                loadAnimals();
                FilterAndDisplayAnimals();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

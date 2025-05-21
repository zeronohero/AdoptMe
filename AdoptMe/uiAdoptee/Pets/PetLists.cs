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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AdoptMe.uiAdoptee
{
    public partial class PetLists : Form
    {
        List<Animal> animals = new List<Animal>();
        public PetLists()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += (s, e) => FilterAndDisplayAnimals();
            textBox1.TextChanged += (s, e) => FilterAndDisplayAnimals();
            loadAnimals();
            FilterAndDisplayAnimals();
        }
        private void loadAnimals()
        {
            animals = Animal.GetAllAnimals()
                            .Where(a => a.Status.Contains("not_adopted"))
                            .ToList();
        }

        private void FilterAndDisplayAnimals()
        {
            IEnumerable<Animal> filtered = animals;

            string selectedSpecies = comboBox1.SelectedItem?.ToString();
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
                var petPane = new uiAdoptee.Pets.PetPanel();
                petPane.SetAnimal(animal);
                flowLayoutPanel2.Controls.Add(petPane);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadAnimals();
            FilterAndDisplayAnimals();
        }
    }
}

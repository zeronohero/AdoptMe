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
            loadAnimals();
            PopulateAnimalPanels();
        }

        private void loadAnimals()
        {
            animals = Animal.GetAllAnimals();
        }


        private void PopulateAnimalPanels()
        {
            // Clear existing controls in the FlowLayoutPanel
            flowLayoutPanel2.Controls.Clear();

            // Loop through the animals list and create panels
            foreach (var animal in animals)
            {
                var animalPanel = new Panel
                {
                    Width = 150,
                    Height = 200,
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Add labels to display animal details
                var nameLabel = new Label { Text = $"Name: {animal.Name}", AutoSize = true };
                var speciesLabel = new Label { Text = $"Species: {animal.Species}", AutoSize = true };
                var ageLabel = new Label { Text = $"Age: {animal.Age}", AutoSize = true };
                var colorLabel = new Label { Text = $"Color: {animal.Color}", AutoSize = true };
                var statusLabel = new Label { Text = $"Status: {animal.Status}", AutoSize = true };



                // Arrange labels in the panel
                animalPanel.Controls.Add(nameLabel);
                animalPanel.Controls.Add(speciesLabel);
                animalPanel.Controls.Add(ageLabel);
                animalPanel.Controls.Add(colorLabel);
                animalPanel.Controls.Add(statusLabel);


                // Adjust label positions
                nameLabel.Location = new System.Drawing.Point(10, 10);
                speciesLabel.Location = new System.Drawing.Point(10, 30);
                ageLabel.Location = new System.Drawing.Point(10, 50);
                colorLabel.Location = new System.Drawing.Point(10, 70);
                statusLabel.Location = new System.Drawing.Point(10, 90);

                // Add the panel to the FlowLayoutPanel
                flowLayoutPanel2.Controls.Add(animalPanel);
            }
        }


        private void ManagePets_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadAnimals();
            PopulateAnimalPanels();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addAnimalForm = new ManagePets_Pop(); // Assuming ManageAnimalView is the form for managing animals
            addAnimalForm.ShowDialog(); // Open as a modal dialog
        }
    }
}

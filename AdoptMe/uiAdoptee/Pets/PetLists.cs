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
            // Clear existing controls in the FlowLayoutPanel
            flowLayoutPanel1.Controls.Clear();

            // Loop through the animals list and create panels
            foreach (var animal in animals)
            {
                var animalPanel = new Panel
                {
                    Width = 150,
                    Height = 240, // Increased height for the button
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

                // Add "Request for Adoption" button
                var requestButton = new Button
                {
                    Text = "Request for Adoption",
                    Width = 130,
                    Height = 30,
                    Location = new System.Drawing.Point(10, 130)
                };

                requestButton.Click += (s, e) =>
                {
                    try
                    {
                        int userId = Session.CurrentUser.Id;
                        int animalId = animal.Animal_id;
                        string information = $"Request to adopt {animal.Name} ({animal.Species})"; // You can customize or prompt for this

                        var request = new AdoptMe.AdoptionRequest(
                            userId,
                            animalId,
                            information,
                            "Pending" // request_status
                        );
                        request.SaveToDatabase();

                        MessageBox.Show($"Adoption request sent for {animal.Name}!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to send adoption request: {ex.Message}");
                    }
                };

                animalPanel.Controls.Add(requestButton);

                // Add the panel to the FlowLayoutPanel
                flowLayoutPanel1.Controls.Add(animalPanel);
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

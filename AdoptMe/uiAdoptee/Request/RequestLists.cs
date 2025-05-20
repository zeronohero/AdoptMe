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

namespace AdoptMe.uiAdoptee.Request
{
    public partial class RequestLists : Form
    {
        List<AdoptionRequest> requests;
        public RequestLists()
        {
            InitializeComponent();
            loadAnimals();
            PopulateRequestPanels();
        }
        private void loadAnimals()
        {
            requests = AdoptionRequest.GetCurrentUserRequests(Session.CurrentUser.Id);
        }
        private void PopulateRequestPanels()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var req in requests)
            {
                var reqPanel = new Panel
                {
                    Width = 300,
                    Height = 120,
                    BorderStyle = BorderStyle.FixedSingle
                };

                var infoLabel = new Label { Text = $"Info: {req.Information}", AutoSize = true };
                var animalLabel = new Label { Text = $"Animal ID: {req.AnimalId}", AutoSize = true };
                var statusLabel = new Label { Text = $"Status: {req.RequestStatus}", AutoSize = true };
                var dateLabel = new Label { Text = $"Date: {req.CreatedAt}", AutoSize = true };

                infoLabel.Location = new Point(10, 10);
                animalLabel.Location = new Point(10, 35);
                statusLabel.Location = new Point(10, 60);
                dateLabel.Location = new Point(10, 85);

                reqPanel.Controls.Add(infoLabel);
                reqPanel.Controls.Add(animalLabel);
                reqPanel.Controls.Add(statusLabel);
                reqPanel.Controls.Add(dateLabel);

                flowLayoutPanel1.Controls.Add(reqPanel);
            }
        }

    }
}

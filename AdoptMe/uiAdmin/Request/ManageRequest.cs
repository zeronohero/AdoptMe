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

namespace AdoptMe.uiAdmin
{
    public partial class ManageRequest : Form
    {
        List<AdoptionRequest> requests;
        public ManageRequest()
        {
            InitializeComponent();
            LoadRequests();
            PopulateRequestPanels();
        }
        private void LoadRequests()
        {
            requests = AdoptionRequest.GetAllRequests();
        }
        private void PopulateRequestPanels()
        {
            flowLayoutPanel2.Controls.Clear();

            foreach (var req in requests)
            {
                var reqPanel = new Panel
                {
                    Width = 300,
                    Height = 180,
                    BorderStyle = BorderStyle.FixedSingle
                };

                var infoLabel = new Label { Text = $"Info: {req.Information}", AutoSize = true };
                var userLabel = new Label { Text = $"User ID: {req.UserId}", AutoSize = true };
                var animalLabel = new Label { Text = $"Animal ID: {req.AnimalId}", AutoSize = true };
                var statusLabel = new Label { Text = $"Status: {req.RequestStatus}", AutoSize = true };
                var dateLabel = new Label { Text = $"Date: {req.CreatedAt}", AutoSize = true };

                // Arrange labels
                infoLabel.Location = new Point(10, 10);
                userLabel.Location = new Point(10, 35);
                animalLabel.Location = new Point(10, 60);
                statusLabel.Location = new Point(10, 85);
                dateLabel.Location = new Point(10, 110);

                reqPanel.Controls.Add(infoLabel);
                reqPanel.Controls.Add(userLabel);
                reqPanel.Controls.Add(animalLabel);
                reqPanel.Controls.Add(statusLabel);
                reqPanel.Controls.Add(dateLabel);

                // Optionally, add Approve/Deny buttons here
                var approveButton = new Button
                {
                    Text = "Approve",
                    Width = 80,
                    Height = 30,
                    Location = new Point(10, 140)
                };
                approveButton.Click += (s, e) =>
                {
                    try
                    {
                        int adminId = Session.CurrentUser.Id; // Replace with your admin session logic
                        AdoptionRequest.ApproveRequest(req.RequestId, adminId);
                        MessageBox.Show("Request approved.");
                        LoadRequests();
                        PopulateRequestPanels();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to approve: {ex.Message}");
                    }
                };

                // Deny button
                var denyButton = new Button
                {
                    Text = "Deny",
                    Width = 80,
                    Height = 30,
                    Location = new Point(100, 140)
                };
                denyButton.Click += (s, e) =>
                {
                    try
                    {
                        int adminId = Session.CurrentUser.Id; // Replace with your admin session logic
                        AdoptionRequest.DenyRequest(req.RequestId, adminId);
                        MessageBox.Show("Request denied.");
                        LoadRequests();
                        PopulateRequestPanels();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to deny: {ex.Message}");
                    }
                };

                reqPanel.Controls.Add(approveButton);
                reqPanel.Controls.Add(denyButton);

                flowLayoutPanel2.Controls.Add(reqPanel);
            }
        }
    }
}

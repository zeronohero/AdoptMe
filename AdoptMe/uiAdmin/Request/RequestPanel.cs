using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoptMe.uiAdmin.Request
{
    public partial class RequestPanel : UserControl
    {
        public RequestPanel()
        {
            InitializeComponent();
        }
        private AdoptionRequest request;

        // Event to notify parent to process this request
        public event EventHandler<AdoptionRequest> ProcessRequested;

        public void SetRequest(AdoptionRequest request)
        {
            this.request = request;
            string animalName = AdoptMe.Animal.GetUserNameById(request.AnimalId) ?? $"Animal ID: {request.AnimalId}";
            label2.Text = $"Animal: {animalName}";
            label3.Text = $"Status: {request.RequestStatus}";
            label4.Text = $"Date: {request.CreatedAt}";
            label5.Text = request.ProcessedAt.HasValue ? $"Processed: {request.ProcessedAt}" : "Not processed";
            if (request.RequestStatus == "Deny" || request.RequestStatus == "Approved")
            {
                label6.Text = $"Proccessed By: {Admin.GetUserNameById((int)request.ProcessedBy)}";
            }
            else
            {
                label6.Text = "";
            }
            pictureBox1.Image = Image.FromFile(Animal.GetImageById(request.AnimalId));
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (request == null)
            {
                MessageBox.Show("No request selected.");
                return;
            }

            using (var processForm = new ProcessRequest(request))
            {
                var result = processForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Optionally refresh or update UI after processing
                    SetRequest(request); // Refresh the panel with updated info
                }
            }
        }
    }
}

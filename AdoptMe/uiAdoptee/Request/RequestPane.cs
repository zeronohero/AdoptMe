using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoptMe.uiAdoptee.Request
{
    public partial class RequestPane : UserControl
    {
        public RequestPane()
        {
            InitializeComponent();
            button1.Hide();
        }
        AdoptionRequest request;

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

            if (request.RequestStatus == "Approved")
            {
                button1.Show();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (request != null)
            {
                // Assuming you have a UserId stored somewhere; replace 0 with actual user ID if available
                int userId = request.UserId; // Ensure your AdoptionRequest class has this property
                int requestId = request.RequestId;

                ReportDoc reportForm = new ReportDoc();
                reportForm.LoadReport(userId, requestId);
                reportForm.Show();
            }
            else
            {
                MessageBox.Show("No request selected to generate report.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

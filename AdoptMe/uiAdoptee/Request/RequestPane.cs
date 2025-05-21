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
        }

        public void SetRequest(AdoptionRequest request)
        {
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

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoptMe.uiAdmin.Request
{
    public partial class ProcessRequest : Form
    {
        public ProcessRequest()
        {
            InitializeComponent();
        }
        public ProcessRequest(AdoptionRequest req)
        {
            InitializeComponent();

            // Get animal details
            Animal animal = Animal.GetAnimalById(req.AnimalId);
            if (animal != null)
            {
                label1.Text = $"Animal: {animal.Name}";
                label2.Text = $"Species: {animal.Species}";
                label3.Text = $"Color: {animal.Color}";
                label4.Text = $"Gender: {animal.Gender}";
                label5.Text = $"Status: {animal.Status}";
                if (!string.IsNullOrEmpty(animal.Image) && System.IO.File.Exists(animal.Image))
                {
                    pictureBox1.Image = Image.FromFile(animal.Image);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }

            // Get Adoptee name
            string adopteeName = Adoptee.GetUserNameById(req.UserId) ?? $"User ID: {req.UserId}";
            label6.Text = $"Requested by: {adopteeName}";
            label8.Text = req.Information;

            // Optionally, show processed by admin
            if (req.ProcessedBy.HasValue)
            {
                string adminName = Admin.GetUserNameById(req.ProcessedBy.Value) ?? $"Admin ID: {req.ProcessedBy.Value}";
                label7.Text = $"Processed by: {adminName}";
            }
            else
            {
                label7.Text = "Not processed yet";
            }
        }

    }
}

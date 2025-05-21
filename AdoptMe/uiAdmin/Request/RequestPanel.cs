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
            button1.Click += button1_Click;

        }
        private AdoptionRequest request;

        // Event to notify parent to process this request
        public event EventHandler<AdoptionRequest> ProcessRequested;

        public void SetRequest(AdoptionRequest req)
        {
            this.request = req; // <-- This line ensures the correct request is used

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
            else
            {
                label1.Text = "Animal: [Unknown]";
            }

            // Get Adoptee name
            string adopteeName = Adoptee.GetUserNameById(req.UserId) ?? $"User ID: {req.UserId}";
            label6.Text = $"Requested by: {adopteeName}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessRequested?.Invoke(this, request);
        }
    }
}

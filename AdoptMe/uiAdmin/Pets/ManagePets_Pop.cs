using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AdoptMe.systemCS;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AdoptMe.uiAdmin
{
    public partial class ManagePets_Pop : Form
    {
        static string imagesDir = Directory.GetParent(Application.StartupPath).Parent.FullName;
        static string default_profile = Path.Combine(imagesDir, "image", "Default.png");

        string profile_picture = string.Empty;
        public ManagePets_Pop()
        {
            InitializeComponent();
            Directory.CreateDirectory(Path.Combine(Application.StartupPath, "image"));

            pictureBox1.Image = Image.FromFile(default_profile);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private string GenerateUniqueImageName(int userId, string originalFilePath)
        {
            string extension = Path.GetExtension(originalFilePath);
            string date = DateTime.Now.ToString("yyyyMMdd");
            string random = Guid.NewGuid().ToString("N").Substring(0, 8); // 8-char random string
            return $"user{userId}_{date}_{random}{extension}";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var animal = new Animal(
                textBox1.Text, // Name
                comboBox1.SelectedItem.ToString(), // Species
                textBox2.Text, // Color
                int.Parse(textBox3.Text), // Age
                comboBox2.SelectedItem.ToString(), // Gender
                "not_adopted", // Default status
                Session.CurrentUser.Id,
                save_image()
            );

            animal.SaveToDatabase();
            MessageBox.Show("Animal added successfully!");
            this.DialogResult = DialogResult.OK; // <-- Add this line
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                {
                    open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bpm; *.png;)|*.jpg; *.jpeg; *.gif; *.bpm; *.png;";
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        profile_picture = open.FileName;
                        pictureBox1.Image = Image.FromFile(open.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // Corrected this line
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string save_image()
        {
            if (profile_picture == default_profile || string.IsNullOrEmpty(profile_picture))
            {
                return default_profile;
            }
            else
            {
                string uniqueFileName = GenerateUniqueImageName(Session.CurrentUser.Id, profile_picture);
                string destinationPath = Path.Combine(imagesDir, uniqueFileName);

                // Only copy if the file doesn't already exist at the destination
                if (!File.Exists(destinationPath))
                {
                    File.Copy(profile_picture, destinationPath, true);
                }
                return destinationPath; 
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

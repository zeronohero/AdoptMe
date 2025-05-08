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
    public partial class AdminDash : Form
    {
        public AdminDash()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addAnimalForm = new ManageAnimal(); // Assuming ManageAnimalView is the form for managing animals
            addAnimalForm.ShowDialog(); // Open as a modal dialog
        }
    }
}

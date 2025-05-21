using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdoptMe.uiAdmin.Reports;

namespace AdoptMe.uiAdmin
{
    public partial class ManageReports : Form
    {
        public ManageReports()
        {
            InitializeComponent();

            // 1. Create your dataset
            var ds = new DataSet1();

            // 2. Fill the dataset with animal data
            List<Animal> animals = Animal.GetAllAnimals(); // Ensure this returns a List<Animal>
            foreach (var animal in animals)
            {
                var row = ds.Animal.NewRow();
                row["animal_id"] = animal.Animal_id;
                row["name"] = animal.Name;
                row["species"] = animal.Species;
                row["color"] = animal.Color;
                row["gender"] = animal.Gender;
                row["status"] = animal.Status;
                row["image"] = animal.Image ?? (object)DBNull.Value;
                row["admin_id"] = animal.Admin_id;
                row["age"] = animal.Age;
                row["adoptee_id"] = animal.Adoptee_id.HasValue ? (object)animal.Adoptee_id.Value : DBNull.Value;
                row["created_at"] = animal.Date_added;
                row["adopted_at"] = animal.AdoptedAt.HasValue ? (object)animal.AdoptedAt.Value : DBNull.Value;
                ds.Animal.Rows.Add(row);
            }


            // 3. Load the Crystal Report
            var report = new CrystalReport1();
            report.SetDataSource(ds);

            // 4. Assign to viewer
            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.Refresh();
        }
    }
    }


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
    public partial class ReportDoc : Form
    {
        public ReportDoc()
        {
            InitializeComponent();
        }
        public void LoadReport(int userId, int requestId)
        {
            var dt = AdoptionRequest.GetAdoptionRequestsWithNames(userId, requestId);

            var ds = new DataSet();
            ds.Tables.Add(dt.Copy()); // Use Copy to avoid issues if dt is already in a DataSet

            var report = new CrystalReport1(); // Make sure this matches your .rpt class name

            report.SetDataSource(ds.Tables[0]);

            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.Refresh();
        }
    }
}

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
                var reqPanel = new AdoptMe.uiAdmin.Request.RequestPanel();
                reqPanel.SetRequest(req);
                reqPanel.ProcessRequested += ReqPanel_ProcessRequested;
                flowLayoutPanel2.Controls.Add(reqPanel);
            }
        }

        private void ReqPanel_ProcessRequested(object sender, AdoptionRequest req)
        {
            using (var processForm = new AdoptMe.uiAdmin.Request.ProcessRequest(req))
            {
                if (processForm.ShowDialog() == DialogResult.OK)
                {
                    LoadRequests();
                    PopulateRequestPanels();
                }
            }
        }

    }
}

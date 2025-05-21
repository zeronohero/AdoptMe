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

namespace AdoptMe.uiAdoptee.Request
{
    public partial class RequestLists : Form
    {
        List<AdoptionRequest> requests;
        public RequestLists()
        {
            InitializeComponent();
            loadAnimals();
            PopulateRequestPanels();
        }
        private void loadAnimals()
        {
            requests = AdoptionRequest.GetCurrentUserRequests(Session.CurrentUser.Id);
        }
        private void PopulateRequestPanels()
        {
            flowLayoutPanel2.Controls.Clear();
            foreach (var req in requests)
            {
                var reqPane = new AdoptMe.uiAdoptee.Request.RequestPane();
                reqPane.SetRequest(req);
                flowLayoutPanel2.Controls.Add(reqPane);
            }
        }

    }
}

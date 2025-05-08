using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoptMe.systemCS
{
    internal static class FormManager
    {
        private static Form _currentForm;

        public static void ShowForm(Form newForm)
        {
            if (_currentForm != null)
            {
                _currentForm.Hide();
                _currentForm.Dispose();
            }

            _currentForm = newForm;
            _currentForm.Show();
        }

        public static void ExitApp()
        {
            Application.Exit();
        }
    }
}

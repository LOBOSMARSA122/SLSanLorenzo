using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAdditionalExamMant : Form
    {
        private string _serviceId = "";
        public frmAdditionalExamMant(string serviceId)
        {
            _serviceId = serviceId;
            InitializeComponent();
        }

        private void frmAdditionalExamMant_Load(object sender, EventArgs e)
        {

        }
    }
}
